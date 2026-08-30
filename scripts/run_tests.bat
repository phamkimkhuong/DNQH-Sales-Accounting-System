@echo off
chcp 65001 > nul
echo ================================================================================

if "%DNQH_TEST_CONNECTION_STRING%"=="" (
    echo [DA DUNG] Chua cau hinh DNQH_TEST_CONNECTION_STRING.
    echo Bien nay phai tro toi database rieng co ten ket thuc bang _Test.
    exit /b 2
)

if "%DNQH_TEST_DEMO_PASSWORD%"=="" (
    echo [DA DUNG] Chua cau hinh DNQH_TEST_DEMO_PASSWORD cho tai khoan fixture.
    exit /b 2
)
echo   CHAY TOAN BO SUITE KIEM THU TU PHASE 4 DEN PHASE 8 - DNQH KE TOAN BAN HANG
echo ================================================================================

set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%.."

echo [1/3] Dang bien dich solution va project MSTest V2 (MSBuild)...
set "TEST_OUTPUT=%CD%\artifacts\Tests"
"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" DNQH_KeToanBanHang.sln /nologo /v:m /p:Configuration=Debug /p:OutDir=%TEST_OUTPUT%\

if %ERRORLEVEL% NEQ 0 (
    echo [LOI] Bien dich that bai! Vui long kiem tra loi tren.
    popd
    exit /b %ERRORLEVEL%
)

echo [2/3] Dang chay MSTest V2 unit tests...
call "%SCRIPT_DIR%run_unit_tests.bat" --no-build
set MSTEST_EXIT=%ERRORLEVEL%
if not "%MSTEST_EXIT%"=="0" goto :mstest_failure

echo [3/3] Dang chay MSTest V2 integration tests Phase 4-8...
call "%SCRIPT_DIR%run_mstest_integration.bat" --no-build
set MSTEST_INTEGRATION_EXIT=%ERRORLEVEL%
if not "%MSTEST_INTEGRATION_EXIT%"=="0" goto :mstest_integration_failure
goto :success

:success
echo ================================================================================
echo [HOAN TAT] MSTEST UNIT VA MSTEST INTEGRATION PHASE 4-8 DEU DAT
echo ================================================================================
popd
exit /b 0

:mstest_failure
echo ================================================================================
echo [THAT BAI] MSTEST V2 KHONG DAT - KIEM TRA artifacts\TestResults
echo ================================================================================
popd
exit /b %MSTEST_EXIT%

:mstest_integration_failure
echo ================================================================================
echo [THAT BAI] MSTEST INTEGRATION KHONG DAT - KIEM TRA artifacts\TestResults
echo ================================================================================
popd
exit /b %MSTEST_INTEGRATION_EXIT%
