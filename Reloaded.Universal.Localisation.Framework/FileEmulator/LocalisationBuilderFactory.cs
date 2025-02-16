using System.Text.Json;
using FileEmulationFramework.Lib;
using FileEmulationFramework.Lib.IO;
using Reloaded.Universal.Localisation.Framework.Interfaces;

namespace Reloaded.Universal.Localisation.Framework.FileEmulator;

public class LocalisationBuilderFactory
{
    /// <summary>
    /// A dictionary mapping base directories to route group tuples
    /// </summary>
    internal Dictionary<string, List<RouteGroupTuple>> RouteFileTuples = new();

    /// <summary>
    /// Adds all available routes from folders.
    /// </summary>
    /// <param name="redirectorFolder">Folder containing the redirector's files.</param>
    /// <param name="baseFolder">The folder that files should be redirected from</param>
    public void AddFromFolders(string redirectorFolder, string baseFolder)
    {
        // Get contents.
        WindowsDirectorySearcher.GetDirectoryContentsRecursiveGrouped(redirectorFolder, out var groups);

        // Find matching folders.
        foreach (var group in groups)
        {
            foreach (var file in group.Files)
            {
                var filePath = $@"{group.Directory.FullPath}\{file}";
                var fullRoute = Route.GetRoute(redirectorFolder, filePath);
                var langIndex = fullRoute.IndexOfAny(Constants.DirectorySeparators);
                var language = fullRoute.Substring(0, langIndex);
                var route = fullRoute.Substring(langIndex+1);
                AddFile(filePath, route, language, baseFolder);
            }
        }
    }
    
    /// <summary>
    /// Adds all available routes from a folder for a particular language.
    /// </summary>
    /// <param name="redirectorFolder">Folder containing the redirector's files.</param>
    /// <param name="baseFolder">The folder that files should be redirected from</param>
    /// <param name="language">The language that the files in this folder are in</param>
    public void AddFromFolders(string redirectorFolder, string baseFolder, Language language)
    {
        // Get contents.
        WindowsDirectorySearcher.GetDirectoryContentsRecursiveGrouped(redirectorFolder, out var groups);

        // Find matching folders.
        foreach (var group in groups)
        {
            foreach (var file in group.Files)
            {
                var filePath = $@"{group.Directory.FullPath}\{file}";
                var route = Route.GetRoute(redirectorFolder, filePath);
                AddFile(filePath, route, language.Id, baseFolder);
            }
        }
    }

    /// <summary>
    /// Adds a file to the builder factory
    /// </summary>
    /// <param name="filePath">The path to the file</param>
    /// <param name="route">The route to the file</param>
    /// <param name="languageId">The ID of the language that the file is in</param>
    /// <param name="baseFolder">The path to the base folder that the file can be used for redirections in (the owner mod's base folder)</param>
    public void AddFile(string filePath, string route, string languageId, string baseFolder)
    {
        if (!RouteFileTuples.ContainsKey(baseFolder))
            RouteFileTuples.Add(baseFolder, new List<RouteGroupTuple>());

        var modRouteFiles = RouteFileTuples[baseFolder];

        modRouteFiles.Add(new RouteGroupTuple()
        {
            Route = new Route(route),
            File = filePath,
            LanguageId = languageId
        });
    }

    /// <summary>
    /// Tries to create a localised file from a given route.
    /// </summary>
    /// <param name="path">The file path/route to create Localisation Builder for.</param>
    /// <param name="builder">The created builder.</param>
    /// <returns>True if a builder could be made, else false (if there are no files to modify this).</returns>
    public bool TryCreateFromPath(string path, out LocalisationBuilder? builder)
    {
        builder = null;

        // Add files
        var route = new Route(path);
        foreach (var (baseDir, routeGroupTuples) in RouteFileTuples)
        {
            if (!route.FullPath.Contains(baseDir))
                continue;

            foreach (var group in routeGroupTuples)
            {
                // Only add matches and prevent files from trying to emulate themselves
                if (!route.Matches(group.Route.FullPath) || route.FullPath.Equals(group.File))
                    continue;

                // Make builder if not made.
                builder ??= new LocalisationBuilder();

                // Add file to builder.
                builder.AddFile(group.File, group.LanguageId);
            }
        }

        return builder != null;
    }

    /// <summary>
    /// Checks if a localised version of a file can be created.
    /// This does not actually create anything.
    /// </summary>
    /// <param name="path">The full path to the file</param>
    /// <param name="language">The language the localised file should be in</param>
    /// <returns>True if a localised version of the file in the specified language can be created, false otherwise.</returns>
    public bool CanCreateFromPath(string path, Language language)
    {
        var route = new Route(path);
        foreach (var (baseDir, routeGroupTuples) in RouteFileTuples)
        {
            if (!route.FullPath.Contains(baseDir))
                continue;

            foreach (var group in routeGroupTuples)
            {
                if (!route.Matches(group.Route.FullPath) || route.FullPath.Equals(group.File))
                    continue;

                if (group.LanguageId == language.Id)
                    return true;
            }
        }

        return false;
    }
}

internal struct RouteGroupTuple
{
    /// <summary>
    /// Route associated with this tuple.
    /// </summary>
    public Route Route;

    /// <summary>
    /// File bound by this route.
    /// </summary>
    public string File;

    /// <summary>
    /// The language id of the file
    /// </summary>
    public string LanguageId;
}