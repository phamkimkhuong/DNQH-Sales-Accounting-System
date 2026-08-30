@echo off
chcp 65001 > nul
setlocal
pushd "%~dp0.."

set "TEST_OUTPUT=%CD%\artifacts\Tests"
set "RESULT_OUTPUT=%CD%\artifacts\TestResults"
set "VSTEST=%CD%\packages\Microsoft.TestPlatform.18.9.0\tools\net462\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"
set "ADAPTER=%CD%\packages\MSTest.TestAdapter.4.4.0\buildTransitive\net462"
set "TEST_ASSEMBLY=%TEST_OUTPUT%\DNQH_KeToanBanHang.MSTest.dll"

if not exist "%VSTEST%" (
    echo [DA DUNG] Chua co Microsoft.TestPlatform 18.9.0.
    echo Hay chay scripts\restore_test_packages.bat truoc.
    popd
    exit /b 2
)

if /I "%~1"=="--no-build" goto :run

echo [1/2] Dang bien dich MSTest V2 project...
"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" "..\DNQH_KeToanBanHangTest\MSTest\DNQH_KeToanBanHang.MSTest.csproj" /nologo /v:m /p:Configuration=Debug /p:OutDir=%TEST_OUTPUT%\
if %ERRORLEVEL% NEQ 0 (
    set "ERR=%ERRORLEVEL%"
    popd
    exit /b %ERR%
)

:run
if not exist "%TEST_ASSEMBLY%" (
    echo [DA DUNG] Khong tim thay %TEST_ASSEMBLY%.
    popd
    exit /b 2
)

if not exist "%RESULT_OUTPUT%" mkdir "%RESULT_OUTPUT%"
echo [2/2] Dang thuc thi MSTest V2 bang VSTest...
"%VSTEST%" "%TEST_ASSEMBLY%" /TestAdapterPath:"%ADAPTER%" /Platform:x64 /TestCaseFilter:"TestCategory!=Integration" /Logger:"trx;LogFileName=mstest-results.trx" /ResultsDirectory:"%RESULT_OUTPUT%"
set "ERR=%ERRORLEVEL%"
popd
exit /b %ERR%
