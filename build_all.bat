ECHO OFF
:: Prepare
MKDIR output
:: Build
C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe /v:minimal build.proj
:: Organize
COPY RouteCalculator.exe output
COPY sample_data.txt output
:: Clean
DEL RouteCalculator.exe