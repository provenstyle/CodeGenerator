namespace CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Generator
    {
        private readonly string _templatePath;
        private readonly string _outputPath;
        private readonly Dictionary<string, string> _keys;

        public Generator(string templateDir, string outputDir, Dictionary<string, string> applicationKeys, Dictionary<string, string> entityKeys)
        {
            _templatePath = templateDir;
            _outputPath   = outputDir;
            _keys         = applicationKeys.Concat(entityKeys).ToDictionary(x => x.Key, x => x.Value);
        }

        public string[] Generate()
        {
            var generatedFiles = new List<string>();
            foreach (var file in GetFiles(_templatePath))
                generatedFiles.Add(Replace(file));

            return generatedFiles.ToArray();
        }

        private string Replace(string filePath)
        {
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


        private IEnumerable<string> GetFiles(string path)
        {
            var queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                foreach (var subDir in Directory.GetDirectories(path))
                    queue.Enqueue(subDir);

                var files = Directory.GetFiles(path);
                foreach (var t in files)
                    yield return t;
            }
        }

        private string OutputFile(string path, string templateDirectory)
        {
            var outputFile = path.Substring(templateDirectory.Length + 1);
            var outputPath = Path.Combine(_outputPath, outputFile);
            foreach (var key in _keys)
                outputPath = outputPath.Replace(key.Key, key.Value);

            return outputPath;
        }

        private string WriteFile(string filePath, string[] lines)
        {
            var outputFile = OutputFile(filePath, _templatePath);
            var directoryName = Path.GetDirectoryName(outputFile);
            if(string.IsNullOrEmpty(directoryName)) return string.Empty;

            Directory.CreateDirectory(directoryName);
            File.WriteAllLines(outputFile, lines);

            Console.WriteLine($"Created {outputFile}");

            return outputFile;
        }
    }
}