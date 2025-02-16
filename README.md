# Localisation Framework
A [Reloaded](https://reloaded-project.github.io/Reloaded-II/) mod that helps you make other mods support multiple languages.

## Usage - Users
As a user you should not need to do anything special. If mods are setup correctly, Localisation Framework will automatically detect the language of the game and load up the correct files for that language.

## Usage - Mod Authors
Localisation Framework works by redirecting files in your mod to different versions depending on the language that the game is running in. This relies on your mod already having appropriate files for the base language, which is currently assumed to always be English.

This system is completely generic and can replace any type of files in your mod's folder. Whether that's a text file, an image, a model, audio, or a binary file and whether it's loaded by [Unreal Essentials](https://github.com/AnimatedSwine37/UnrealEssentials), [Persona Essentials](https://github.com/Sewer56/p5rpc.modloader), [File Emulation Framework](https://github.com/Sewer56/FileEmulationFramework), or any other kind of system!

### Dependencies
The first thing you'll need to do is make your mod depend on the Localisation Framework mod and all of the [Language Providers](#language-providers) that the game you are modding needs.
These dependencies will work transitively so if the game you are modding has something like an "Essentials" mod that all mods already depend on, the Localisation Framework and Language Provider dependencies can be in that mod instead.

If there are multiple potential sources for the game's language you can depend on multiple providers and they should be able to determine the correct one.
For example, [Persona Essentials](https://github.com/Sewer56/p5rpc.modloader) is setup to support Localisation Framework by depending on it, the Steam Language Provider and the Windows Language Provider. The Steam provider will be used if the user is playing the Steam version of one of the games and the Windows one will act as a backup, in which case they are playing the Microsoft store version.

In this example your mod can just depend on Persona Essentials instead of directly depending on Localisation Framework and the language providers.

### Formatting Files
To add localised versions of files to your mod you just need to put them in a folder called `FEmulator\L10N\{languageId}`, where `{languageId}` is the id for the language that the files are for (see [Supported Languages](#supported-languages)).

For example, if your mod has an English file in `FEmulator\BF\FHIT_003_002_00.msg` and you wanted to also support Japanese, you would put a Japanese version of that file at `FEmulator\L10N\ja\FHIT_003_002_00.msg`. 

The paths that you use inside of `FEmulator\L10N\{languageId}` follow the normal routing rules of File Emulation Framework (see its [docs](https://sewer56.dev/FileEmulationFramework/routing.html)) so you can be more specific if you need.
For example, if you have two files in your mod:
- `Redirector\enemy\names.txt`
- `Redirector\friend\names.txt`

You'd make your routes more specific so both `names.txt` don't get replaced with the same file. Like:
- `FEmulator\L10N\ja\enemy\names.txt`
- `FEmulator\L10N\ja\friend\names.txt`

### API
If you're making a code mod you can access Localisation Framework's API to get the game's language in your code, provide additional localised versions of files, or create your own language provider (see [Creating A Language Provider](#creating-a-language-provider)).

First, you'll need to add the `Reloaded.Universal.Localisation.Framework.Interfaces` NuGet package to your mod's project. If you don't know how to add NuGet packages, look it up online. Most IDEs will have built-in support.

Next, add a dependency on `Reloaded.Universal.Localisation.Framework` to the `ModDependencies` section of your `ModConfig.json` file.

Then get access to the API by using the following code (or something like it):

```csharp
// An instance variable in the Mod class (you don't need to keep it as an instance variable if you don't want to)
private ILocalisationFramework _localisationFramework;

public Mod(ModContext context) 
{
    // Other code in your constructor...
    
    var localisationFrameworkController = _modLoader.GetController<ILocalisationFramework>();
    if (localisationFrameworkController == null || !localisationFrameworkController.TryGetTarget(out _localisationFramework))
    {
        _logger.WritLine("[My Mod] Unable to get controller for Localisation Framework, stuff won't work :(", System.Drawing.Color.Red);
        return;
    }
    
    // Do stuff with _localisationFramework...
}

```

Then you can use the `ILocalisationFramework` instance that you got to do stuff. Currently this includes:
- `SetLanguage` to set the language (what a Language Provider does)
- `TryGetLanguage` to try and get the game's current language
- `IsFileLocalised` to check if a file has a localised version for the current language
- `AddFile` to add a new file to be used for localisation
- `AddDirectory` to add a directory full of new files to be used for localisation

#### Adding New Localised Files
Sometimes it may be useful to dynamically add new files for localisation in your code. You can do this with the `AddFile` and `AddDirectory` functions in the `ILocalisationFramework` API.
These are fairly straight forward, but there is one thing of note: all of these functions require a `modDir` argument. This should be the base path to your mod's directory which you can get with the following code:

```csharp
var modDir = _modLoader.GetDirectoryForModId(_modConfig.ModId);
```

Where `_modLoader` and `_modConfig` are included in the `Mod` constructor in the standard Reloaded mod template.

Note that technically you can use this to localise the files in other mods however I'd advise against it unless you really need to. 
If possible, please try and get the other mod's author to just add the localisation directly to it. I made this framework specifically so people didn't have to download things like compatibility mods for languages (mods for mods).

## Supported Languages
Localisation Framework currently supports the following languages. 

Note that just because Localisation Framework supports the langauge doesn't necessarily mean you can actually use it for a specific game. 
For example, if you are using it on a Steam game that doesn't support French, putting files in `FEmulator\BF\L10N\fr` will do nothing as Steam will never report the game's language as French. 

(It should be possible to add support for a completely new language like this by having a base mod for the language which acts as a [Language Provider](#language-providers)).

| Language                 | Id      | 
|--------------------------|---------|
| Arabic                   | ar      |
| Bulgarian                | bg      |
| Traditional Chineese     | zh-Hant |
| Simplified Chinese       | zh-Hans |
| Czech                    | cs      |
| Danish                   | da      |
| Dutch                    | nl      |
| English                  | en      |
| Finnish                  | fi      |
| French                   | fr      |
| German                   | de      |
| Greek                    | el      |
| Hungarian                | hu      |
| Indonesian               | id      |
| Italian                  | it      |
| Japanese                 | ja      |
| Norweegian               | no      |
| Polish                   | pl      |
| Portugese                | pt      |
| Brazilian                | pt-BR   |
| Romanian                 | ro      |
| Russian                  | ru      |
| Spanish                  | es      |
| Spanish - Latin American | es-419  |
| Swedish                  | sv      |
| Thai                     | th      |
| Turkish                  | tr      |
| Ukranian                 | uk      |
| Vietnamese               | vn      |

## Language Providers
Language Providers are what Localisation Framework uses to determine the language that a game is using.
Below is a list of all the current ones.

| Language Provider                                                                                                                                    | Mod Id  | Description                                                                                                                                                            |
|------------------------------------------------------------------------------------------------------------------------------------------------------|---------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Steam](https://github.com/AnimatedSwine37/Reloaded.Universal.Localisation.Framework/tree/master/Reloaded.Universal.Localisation.Provider.Steam)     | Reloaded.Universal.Localisation.Provider.Steam | Uses Steam's API to determine the language of the game. This is the language that you see when you right click a game in Steam and go to Properties->General->Language |
| [Windows](https://github.com/AnimatedSwine37/Reloaded.Universal.Localisation.Framework/tree/master/Reloaded.Universal.Localisation.Provider.Windows) | Reloaded.Universal.Localisation.Provider.Windows | Uses your Windows display language to determinee the language of the game. Used for Microsoft Store/Game Pass games.                                                   |

### Creating A Language Provider
If you want to get language information for a game that currently does not have a supported Language Provider you can write your own! 
This doesn't necessarily need to be a separate mod, it could be included in something like an "Essentials" mod if it's game specific.

All you need to do is get access to Localisation Framework's API as described in [API](#api) and use the `SetLanguage` function in it. 
The specifics of how you determine what the language should actually be set to are up to you. 

If you've made a language provider, particularly if it's for a generic platform like Steam, please let me know and I'll add it to the list above (or make the pr, adding it yourself!)