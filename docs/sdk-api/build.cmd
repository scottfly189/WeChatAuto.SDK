@echo off
rem Usage:
rem   set WECHATAUTO_VIP_ROOT=D:\repo\WeChatAuto.SDK\WeChatAuto.SDK\src
rem   build.cmd
rem
rem The public repo does not contain VIP source files, so WECHATAUTO_VIP_ROOT
rem must point to the private WeChatAuto.SDK\src checkout when regenerating
rem the full (VIP) API reference.
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
set "ROOT=%SCRIPT_DIR%..\.."
set "WORK=%TEMP%\wechatauto-sdk-api"
set "SOURCE_ROOT=%ROOT%"
if defined WECHATAUTO_VIP_ROOT set "SOURCE_ROOT=%WECHATAUTO_VIP_ROOT%"
set "CSPROJ=%SOURCE_ROOT%\WeChatAuto4_X\WeChatAuto\WeChatAuto.csproj"

if not exist "%SOURCE_ROOT%\WeChatAuto4_X\WeChatAuto\Components\WeChatClient_Vip.cs" (
  echo [ERROR] WeChatClient_Vip.cs not found under "%SOURCE_ROOT%".
  echo         The public repo does not contain VIP source files.
  echo         Set WECHATAUTO_VIP_ROOT to the private WeChatAuto.SDK\src directory and run again.
  exit /b 1
)

if not exist "%CSPROJ%" (
  echo [ERROR] Cannot find %CSPROJ%
  exit /b 1
)

where dotnet >nul 2>nul
if errorlevel 1 (
  echo [ERROR] dotnet not found in PATH.
  exit /b 1
)

where docfx >nul 2>nul
if errorlevel 1 (
  echo [ERROR] docfx not found in PATH. Install DocFX first.
  exit /b 1
)

echo [1/4] Cleaning work directory...
if exist "%WORK%" rmdir /s /q "%WORK%"
mkdir "%WORK%" || exit /b 1

echo [2/4] Building WeChatAuto (Release, net10.0-windows)...
dotnet build "%CSPROJ%" -c Release -f net10.0-windows -o "%WORK%\bin" --nologo
if errorlevel 1 (
  echo [ERROR] Build failed.
  exit /b 1
)

echo [3/4] Running DocFX...
xcopy "%SCRIPT_DIR%.docfx\*" "%WORK%\" /E /I /Y >nul
pushd "%WORK%"
docfx docfx.json
if errorlevel 1 (
  popd
  echo [ERROR] DocFX failed.
  exit /b 1
)
popd

if not exist "%WORK%\out\index.html" (
  echo [ERROR] DocFX output not found: %WORK%\out\index.html
  exit /b 1
)

echo [4/4] Replacing generated files in %SCRIPT_DIR%...
rem Keep build.cmd and .docfx, remove all other generated files/directories.
for /d %%D in ("%SCRIPT_DIR%*") do (
  if /I not "%%~nxD"==".docfx" rmdir /s /q "%%~fD"
)
for %%F in ("%SCRIPT_DIR%*") do (
  if /I not "%%~nxF"=="build.cmd" if not exist "%%~fF\" del /q "%%~fF"
)
xcopy "%WORK%\out\*" "%SCRIPT_DIR%" /E /I /Y >nul

echo [DONE] API reference updated: %SCRIPT_DIR%index.html
exit /b 0
