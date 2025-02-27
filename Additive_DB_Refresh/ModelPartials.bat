@echo off
setlocal enabledelayedexpansion
set "projectbasepath=%~1"
set "projectname=%~2"
REM Directories
set "source_dir=%projectbasepath%Models"
set "target_dir=%projectbasepath%ModelPartials"
set newpartialfiles=false
echo Comparing files in !source_dir! to files in !target_dir!

REM Iterate through files in source directory
for %%F in ("%source_dir%\*.cs") do (
    set "filename=%%~nxF"
	set "baseclass=%%~nF"
    set "target_filename=%%~nFPartial%%~xF"
	set "partialclass=%%~nFPartial"
	echo Checking for !partialclass! in !target_dir!
    if not exist "%target_dir%\!target_filename!" (
		REM Create an empty file and write its name into the file
		echo Generating file for !partialclass! in !target_dir!
		set newpartialfiles=true
		echo using System.CodeDom.Compiler; >> "%target_dir%\!target_filename!"
		echo. >>"%target_dir%\!target_filename!"
		echo namespace !projectname!.Models >> "%target_dir%\!target_filename!"
		echo {>> "%target_dir%\!target_filename!"
		echo 	public partial class !baseclass! >> "%target_dir%\!target_filename!"
		echo 	{ >> "%target_dir%\!target_filename!"
		echo			//This file is autogenerated. Construct override functions and remove the NotImplemented variable>>"%target_dir%\!target_filename!"
		echo			private int NotImplemented = "No equals comparison implemented yet";>>"%target_dir%\!target_filename!"
		echo. >>"%target_dir%\!target_filename!"		
		echo			public override bool Equals^(object? obj^) >>"%target_dir%\!target_filename!"
		echo			{>>"%target_dir%\!target_filename!"
		echo				throw new NotImplementedException^("!baseclass!.Equals not implemented."^);>>"%target_dir%\!target_filename!"
		echo				if ^(obj is !baseclass! other^)>>"%target_dir%\!target_filename!"
		echo				{>>"%target_dir%\!target_filename!"
		echo				//	return !baseclass!Key == other.!baseclass!Key;>>"%target_dir%\!target_filename!"
		echo				}>>"%target_dir%\!target_filename!"
		echo				else>>"%target_dir%\!target_filename!"
		echo				{>>"%target_dir%\!target_filename!"
		echo					return false;>>"%target_dir%\!target_filename!"
		echo				}>>"%target_dir%\!target_filename!"
		echo			}>>"%target_dir%\!target_filename!"
		echo			public override int GetHashCode^(^)>>"%target_dir%\!target_filename!"
		echo			{>>"%target_dir%\!target_filename!"
		echo			//	return HashCode.Combine^(!baseclass!Key^);>>"%target_dir%\!target_filename!"
		echo				throw new NotImplementedException^("!baseclass!.GetHasCode not implemented."^);>>"%target_dir%\!target_filename!"
		echo			}>>"%target_dir%\!target_filename!"
		echo 	} >> "%target_dir%\!target_filename!"
		echo } >> "%target_dir%\!target_filename!"
		
	) else (
	
		echo !partialclass! found in !target_dir!
	)
)
REM Throw 1 to stop build if new files created
if %newpartialfiles% equ true (
	echo New partial files created by batch file. Build process terminated and must be rerun.
	exit 1
) else (
	exit 0
)
