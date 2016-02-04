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
Source: "Install\*"; DestDir: "{app}\Install"; Flags: ignoreversion

[Run]
; Add a IIS website
Filename: {cmd}; Parameters: "/C ""{app}\Install\IISSetup.bat {app}"""

; Install the monitoring service
Filename: {cmd}; Parameters: "/C ""{app}\Install\MonitoringInstall.bat"""
