namespace CodeGenerator
{
    using System;
    using System.IO;
    using System.Linq;
    using FubuCsProjFile;

    public class ProjectUpdater
    {
        public void Update(string[] files, string[] csprojPaths)
        {
            foreach (var csprojPath in csprojPaths)
            {
                var directory = Path.GetDirectoryName(csprojPath) + "\\";
                var addedFiles = files.Where(x => x.Contains(directory,StringComparison.OrdinalIgnoreCase)).ToArray();
                var addedCsFiles      = addedFiles.Where(x => x.EndsWith(".cs")).ToArray();
                var addedSqlFiles     = addedFiles.Where(x => x.EndsWith(".sql")).ToArray();
                var addedContentFiles = addedFiles.Where(x => !addedCsFiles.Contains(x) && !addedSqlFiles.Contains(x)).ToArray();

                var csproj = CsProjFile.LoadFrom(csprojPath);

                //existing
                var codeFiles        = csproj.All<CodeFile>().ToArray();
                var contentFiles     = csproj.All<Content>().ToArray();
                var embeddedResource = csproj.All<EmbeddedResource>().ToArray();

                foreach (var addedCsFile in addedCsFiles)
                {
                    var relativeFile = addedCsFile.Substring(directory.Length);
                    if(codeFiles.All(x => !x.Include.Equals(relativeFile, StringComparison.OrdinalIgnoreCase)))
                        csproj.Add(new CodeFile(relativeFile));
                }

                foreach (var addedSqlFile in addedSqlFiles)
                {
                    var relativeFile = addedSqlFile.Substring(directory.Length);
                    if(embeddedResource.All(x => !x.Include.Equals(relativeFile, StringComparison.OrdinalIgnoreCase)))
                        csproj.Add(new EmbeddedResource(relativeFile));
                }

                foreach (var addedContentFile in addedContentFiles)
                {
                    var relativeFile = addedContentFile.Substring(directory.Length);
                    if(contentFiles.All(x => !x.Include.Equals(relativeFile, StringComparison.OrdinalIgnoreCase)))
                        csproj.Add(new Content(relativeFile));
                }
                csproj.Save();
            }
        }
    }
}