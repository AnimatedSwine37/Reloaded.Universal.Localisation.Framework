# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/Reloaded.Universal.Localisation.Provider.Steam/*" -Force -Recurse
dotnet publish "./Reloaded.Universal.Localisation.Provider.Steam.csproj" -c Release -o "$env:RELOADEDIIMODS/Reloaded.Universal.Localisation.Provider.Steam" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location