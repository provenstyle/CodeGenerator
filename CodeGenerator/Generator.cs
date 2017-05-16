namespace CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Generator
    {
        private readonly string _templatePath;
        private readonly string _outputPath;
        private readonly Dictionary<string, string> _keys;

        public Generator(string templateDir, string outputDir, Dictionary<string, string> keys)
        {
            _templatePath = templateDir;
            _outputPath   = outputDir;
            _keys         = keys;
        }

        public void Generate()
        {
            foreach (var file in GetFiles(_templatePath))
                Replace(file);
        }

        private void Replace(string filePath)
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
            WriteFile(filePath, updatedLines.ToArray());
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

        private void WriteFile(string filePath, string[] lines)
        {
            var outputFile = OutputFile(filePath, _templatePath);
            var directoryName = Path.GetDirectoryName(outputFile);
            if(string.IsNullOrEmpty(directoryName)) return;

            Directory.CreateDirectory(directoryName);
            File.WriteAllLines(outputFile, lines);

            Console.WriteLine($"Created {outputFile}");
        }
    }
}