regsvr32 "%~dp0SignalGen.dll"
regsvr32 "%~dp0SignalGenPS.dll"
"%~dp0OPCServer.exe" /RegServer
regsvr32 "%~dp0OPCServerPS.dll"
pause