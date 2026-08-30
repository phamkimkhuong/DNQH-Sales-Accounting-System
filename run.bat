@echo off
chcp 65001 > nul
echo ========================================================
echo   HE THONG THONG TIN KE TOAN - KE TOAN BAN HANG (DNQH)
echo   Dang kiem tra build va khoi chay ung dung...
echo ========================================================

:: Tat app cu neu dang mo de giai phong khoa file .exe
taskkill /F /IM DNQH_KeToanBanHang.exe 2>nul
timeout /t 1 /nobreak >nul

"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" DNQH_KeToanBanHang.sln /nologo /v:m

if %ERRORLEVEL% EQU 0 (
    echo [OK] Build thanh cong! Dang mo ung dung...
    start "" "bin\Debug\DNQH_KeToanBanHang.exe"
) else (
    echo [LOI] Build that bai! Vui long kiem tra loi tren man hinh.
    pause
)
