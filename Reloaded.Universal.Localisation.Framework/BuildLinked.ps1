# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/Reloaded.Universal.Localisation.Framework/*" -Force -Recurse
dotnet publish "./Reloaded.Universal.Localisation.Framework.csproj" -c Release -o "$env:RELOADEDIIMODS/Reloaded.Universal.Localisation.Framework" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location