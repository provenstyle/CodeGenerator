namespace CodeGenerator.Features.Generation
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TemplateProcessor
    {
        private readonly Dictionary<string, string> _keys;
        private readonly ApplicationConfig _config;

        public TemplateProcessor(ApplicationConfig config, Dictionary<string, string> keys)
        {
            _config       = config;
            _keys         = keys;
        }

        public string[] Run()
        {
            var generatedFiles = new List<string>();
            foreach (var file in Files.GetFiles(_config.TemplateFolder))
            {
                var replaced = Replace(file);
                if(!string.IsNullOrEmpty(replaced))
                    generatedFiles.Add(replaced);
            }

            return generatedFiles.ToArray();
        }

        private string Replace(string filePath)
        {
            if (_config.Replace!= true && File.Exists(OutputFile(filePath, _config.TemplateFolder)))
                return null;

            var lines = File.ReadAllLines(filePath);
            var updatedLines = new List<string>();

            foreach (var line in lines)
            {
                var updated = line;
                foreach (var key in _keys)
                    updated = updated.Replace(key.Key, key.Value);

                updatedLines.Add(updated);
            }
            return WriteFile(filePath, updatedLines.ToArray());
        }

        private string OutputFile(string path, string templateDirectory)
        {
            var outputFile = path.Substring(templateDirectory.Length + 1);
            var outputDirectory = new DirectoryInfo(_config.SolutionFolder).Parent;
            var outputPath = Path.Combine(outputDirectory.FullName, outputFile);
            foreach (var key in _keys)
                outputPath = outputPath.Replace(key.Key, key.Value);

            return outputPath;
        }

        private string WriteFile(string filePath, string[] lines)
        {
            var outputFile = OutputFile(filePath, _config.TemplateFolder);
            var directoryName = Path.GetDirectoryName(outputFile);
            if(string.IsNullOrEmpty(directoryName)) return string.Empty;

            Directory.CreateDirectory(directoryName);
            File.WriteAllLines(outputFile, lines);

            Console.WriteLine($"Created {outputFile}");

            return outputFile;
        }
    }
}