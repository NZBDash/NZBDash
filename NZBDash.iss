; NZBDash Installer

[Setup]
AppName=NZBDash
AppVersion=1.0
DefaultDirName={pf}\NZBDash
DefaultGroupName=NZBDash
UninstallDisplayIcon={app}\NZBDash.exe
Compression=lzma2
SolidCompression=yes
OutputDir=Installer
PrivilegesRequired=admin
UninstallRestartComputer=false

; Copy over all the files
[Files]
Source: "NZBDash.UI\bin\*"; DestDir: "{app}\UI\bin"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "NZBDash.UI\Content\*"; DestDir: "{app}\UI\Content"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "NZBDash.UI\fonts\*"; DestDir: "{app}\UI\fonts"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "NZBDash.UI\Scripts\*"; DestDir: "{app}\UI\Scripts"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "NZBDash.UI\Styles\*"; DestDir: "{app}\UI\Styles"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "NZBDash.UI\Views\*"; DestDir: "{app}\UI\Views"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "NZBDash.UI\favicon.ico"; DestDir: "{app}\UI"; Flags: ignoreversion  
Source: "NZBDash.UI\NLog.config"; DestDir: "{app}\UI"; Flags: ignoreversion  
Source: "NZBDash.UI\Web.config"; DestDir: "{app}\UI"; Flags: ignoreversion  
Source: "NZBDash.UI\Global.asax"; DestDir: "{app}\UI"; Flags: ignoreversion
Source: "NZBDash.Services.HardwareMonitor\bin\*"; DestDir: "{app}\Monitoring"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "Install\*"; DestDir: "{app}\Install"; Flags: ignoreversion recursesubdirs createallsubdirs

[Run]
; Add a IIS website
Filename: C:\Windows\System32\inetsrv\appcmd.exe; Parameters: "add site /name:NZBDash /bindings:http/*:7500 /physicalPath:{app}/UI/"; Flags: runascurrentuser

; Install the monitoring service
Filename: {app}/Monitoring/Debug/NZBDash.Services.HardwareMonitor.exe; Parameters: "install"; Flags: runascurrentuser

[UninstallRun]
; Uninstall the monitoring service
Filename: {app}/Monitoring/Debug/NZBDash.Services.HardwareMonitor.exe; Parameters: "uninstall"; Flags: runascurrentuser

