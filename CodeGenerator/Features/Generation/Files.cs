namespace CodeGenerator.Features.Generation
{
    using System.Collections.Generic;
    using System.IO;

    public static class Files
    {
        public static IEnumerable<string> GetFiles(string path, string filter = null)
        {
            var queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                foreach (var subDir in Directory.GetDirectories(path))
                    queue.Enqueue(subDir);

                var files = filter == null
                                ? Directory.GetFiles(path)
                                : Directory.GetFiles(path, filter);

                foreach (var t in files)
                    yield return t;
            }
        }
    }
}