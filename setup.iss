; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Super Roberto Breakout"
#define MyAppVersion "1.0"
#define MyAppPublisher "Hutopi"
#define MyAppURL "https://github.com/hutopi/breakout"
#define MyAppExeName "Super Roberto Breakout.exe"
#define XGAGSLocation "C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0"
#define XNARedist "xnafx40_redist.msi"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{4BD01E15-9A00-48BA-BEA4-F1BDEDF94313}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
LicenseFile=.\LICENSE
OutputDir=.\setup
OutputBaseFilename=setup
SetupIconFile=.\breakout\breakout\icon.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: {#XGAGSLocation}\Redist\XNA FX Redist\{#XNARedist}; DestDir: {tmp}
Source: ".\breakout\breakout\bin\x86\Release\Super Roberto Breakout.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\breakout\breakout\bin\x86\Release\Content\*"; DestDir: "{app}\Content"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: msiexec.exe; StatusMsg: "Installing required component: XNA Framework Redistributable 4.0 Refresh."; Parameters: "/qb /i ""{tmp}\{#XNARedist}"; Check: CheckXNAFramework
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
function IsXNAFrameworkDetected: Boolean;
var
    key: string;
    install: cardinal;
    success: boolean;
 
begin
    WizardForm.StatusLabel.Caption := 'Checking for XNA Framework Redistributable 4.0 Refresh.';
    if IsWin64 then begin
        key := 'SOFTWARE\Wow6432Node\Microsoft\XNA\Framework\v4.0';
    end else begin
        key := 'SOFTWARE\Microsoft\XNA\Framework\v4.0';
    end;
    success := RegQueryDWordValue(HKLM, key, 'Installed', install);
    result := success and (install = 1);
end;
 
function CheckXNAFramework: boolean;
begin
    if IsXNAFrameworkDetected then begin
        WizardForm.StatusLabel.Caption := 'XNA Framework Redistributable 4.0 Refresh detected.';
    end;
    result := not IsXNAFrameworkDetected;
end;