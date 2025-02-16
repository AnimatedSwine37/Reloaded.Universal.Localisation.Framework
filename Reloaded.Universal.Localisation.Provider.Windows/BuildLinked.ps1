# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/Reloaded.Universal.Localisation.Provider.Windows/*" -Force -Recurse
dotnet publish "./Reloaded.Universal.Localisation.Provider.Windows.csproj" -c Release -o "$env:RELOADEDIIMODS/Reloaded.Universal.Localisation.Provider.Windows" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location