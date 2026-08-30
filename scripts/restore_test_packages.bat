@echo off
setlocal
pushd "%~dp0.."

set "NUGET=%CD%\.tools\nuget.exe"
set "PACKAGES_CONFIG=%CD%\..\DNQH_KeToanBanHangTest\MSTest\packages.config"

if not exist "%NUGET%" (
    echo [DA DUNG] Khong tim thay .tools\nuget.exe.
    echo Tai NuGet CLI tu https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
    echo va dat vao thu muc .tools, sau do chay lai lenh nay.
    popd
    exit /b 2
)

"%NUGET%" install "%PACKAGES_CONFIG%" -OutputDirectory "%CD%\packages" -NonInteractive -DirectDownload
set "EXIT_CODE=%ERRORLEVEL%"
popd
exit /b %EXIT_CODE%
