using System.Collections.Concurrent;
using FileEmulationFramework.Interfaces;
using FileEmulationFramework.Interfaces.Reference;
using Reloaded.Universal.Localisation.Framework.Interfaces;

namespace Reloaded.Universal.Localisation.Framework.FileEmulator;

public class LocalisationEmulator : IEmulator
{
    
    // Note: Handle->Stream exists because hashing IntPtr is easier; thus can resolve reads faster.
    private readonly LocalisationBuilderFactory _builderFactory = new();
    private readonly ConcurrentDictionary<string, LocalisedFile?> _pathToLocalised = new(StringComparer.OrdinalIgnoreCase);
    
    private Language? _gameLanguage;

    /// <summary>
    /// Checks whether we could create a localised version of a file in the current language.
    /// </summary>
    /// <param name="path">The path to the file</param>
    /// <returns>True if we could (or already have) created a localised version of the file in the language, false otherwise</returns>
    public bool CanCreateLocalisedFile(string path)
    {
        if (_gameLanguage == null)
            return false;
        
        if (_pathToLocalised.ContainsKey(path))
            return true;
        
        return _builderFactory.CanCreateFromPath(path, _gameLanguage);
    }

    public bool TryCreateFile(IntPtr handle, string filepath, string route, out IEmulatedFile emulated)
    {
        // Check if we already made a localised file.
        emulated = null!;
        if (_pathToLocalised.TryGetValue(filepath, out var localisedFile))
        {
            if (localisedFile == null)
                return false;

            emulated = new EmulatedFile<Stream>(localisedFile.Stream, localisedFile.LastWriteTime);
            return true;
        }
        
        if (!TryCreateEmulatedFile(handle, filepath, filepath, filepath, ref emulated!))
            return false;

        return true;
    }
    
    /// <summary>
    /// Tries to create an emulated file from a given file handle.
    /// </summary>
    /// <param name="handle">Handle of the bf file to use as a base.</param>
    /// <param name="srcDataPath">Path of the file the handle refers to.</param>
    /// <param name="outputPath">Path where the emulated file is stored.</param>
    /// <param name="route">The route of the emulated file, for builder to pick up.</param>
    /// <param name="emulated">The emulated file.</param>
    /// <returns>True if an emulated file could be created, false otherwise</returns>
    public bool TryCreateEmulatedFile(IntPtr handle, string srcDataPath, string outputPath, string route, ref IEmulatedFile? emulated)
    {
        // If we have no language information nothing can be localised
        if (_gameLanguage == null)
            return false;
        
        // Check if there's a known route for this file
        // Put this before actual file check because I/O.
        if (!_builderFactory.TryCreateFromPath(route, out var builder))
            return false;
        
        // Make the emulated file
        _pathToLocalised[outputPath] = null; // Avoid recursion into same file.

        var localised = builder!.Build( _gameLanguage, srcDataPath);
        if (localised == null)
            return false;

        _pathToLocalised[outputPath] = localised;
        emulated = new EmulatedFile<Stream>(localised.Stream, localised.LastWriteTime);
        Utils.Log($"Created Emulated file with Path {outputPath}");
        return true;
    }

    /// <summary>
    /// Called when a mod is being loaded.
    /// </summary>
    /// <param name="modFolder">Folder where the mod is contained.</param>
    public void OnModLoading(string modFolder)
    {
        var redirectorFolder = $"{modFolder}/{Constants.RedirectorFolder}";

        if (Directory.Exists(redirectorFolder))
            _builderFactory.AddFromFolders(redirectorFolder, modFolder);
    }
    
    /// <summary>
    /// Adds a file to be used for localisation
    /// </summary>
    /// <param name="file">The path to the file</param>
    /// <param name="route">The route to the file</param>
    /// <param name="modDir">The base directory of the mod that the file is for</param>
    /// <param name="language">The language that the file is in</param>
    public void AddFile(string file, string route, string modDir, Language language)
    {
        _builderFactory.AddFile(file, route, language.Id, modDir);
    }

    /// <summary>
    /// Adds a directory to Localisation Framework so it's like the files were in FEmulator\L10N.
    /// There must be appropriate language subfolders in the spcified path, such as `{dir}\ja\...` for Japanese files. 
    /// </summary>
    /// <param name="dir">The directory to add the files from</param>
    /// <param name="modDir">The base directory to the mod that the localised files are for</param>
    public void AddDirectory(string dir, string modDir)
    {
        _builderFactory.AddFromFolders(dir, modDir);
    }
    
    /// <summary>
    /// Adds a directory to Localisation Framework so it's like the files were in FEmulator\L10N\{language}
    /// </summary>
    /// <param name="dir">The directory to add the files from</param>
    /// <param name="modDir">The base directory to the mod that the localised files are for</param>
    /// <param name="language">The language that the files in this folder are in</param>
    public void AddDirectory(string dir, string modDir, Language language)
    {
        _builderFactory.AddFromFolders(dir, modDir, language);
    }
    
    /// <summary>
    /// Sets the current language of the emulator
    /// </summary>
    /// <param name="language">The new language for the emulator</param>
    internal void SetLanguage(Language language) => _gameLanguage = language;
    
}