# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "Publish" -Recurse -ErrorAction SilentlyContinue

# Base framework
Push-Location "./Reloaded.Universal.Localisation.Framework"
./Publish.ps1 -PublishOutputDir "../Publish/ToUpload/LocalisationFramework"
Remove-Item "Publish/Builds" -Recurse -ErrorAction SilentlyContinue
Pop-Location

# Steam language provider
Push-Location "./Reloaded.Universal.Localisation.Provider.Steam"
./Publish.ps1 -PublishOutputDir "../Publish/ToUpload/SteamProvider"
Remove-Item "Publish/Builds" -Recurse -ErrorAction SilentlyContinue
Pop-Location

# Windows language provider
Push-Location "./Reloaded.Universal.Localisation.Provider.Windows"
./Publish.ps1 -PublishOutputDir "../Publish/ToUpload/WindowsProvider"
Remove-Item "Publish/Builds" -Recurse -ErrorAction SilentlyContinue
Pop-Location
              
# API interfaces
.\PublishInterfaces.ps1