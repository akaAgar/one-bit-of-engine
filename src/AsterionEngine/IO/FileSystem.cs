using System.IO;
using System.Reflection;

namespace Asterion.IO
{
    public sealed class FileSystem
    {
        public FileSystemSourceType SourceType { get { return Source.SourceType; } }
        public string SourcePath { get { return Source.SourcePath; } }

        private FileSource Source = null;

        internal FileSystem()
        {
            SetSource(FileSystemSourceType.Folder, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        public bool SetSource(FileSystemSourceType sourceType, string path, string password = "")
        {
            switch (sourceType)
            {
                case FileSystemSourceType.Archive:
                    if (!File.Exists(path)) return false;
                    CloseSource();
                    Source = new FileSourceArchive(path, password);
                    return true;

                case FileSystemSourceType.Folder:
                    if (!Directory.Exists(path)) return false;
                    CloseSource();
                    Source = new FileSourceFolder(path);
                    return true;
            }

            return false;
        }

        private void CloseSource()
        {
            if (Source == null) return;

            Source.Dispose();
            Source = null;
        }

        internal byte[] GetFile(string file)
        {
            if (!Source.FileExists(file)) return null;
            return Source.GetFile(file);
        }

        public Stream GetFileAsStream(string file)
        {
            if (!Source.FileExists(file)) return null;
            return new MemoryStream(Source.GetFile(file));
        }

        public bool FileExists(string file)
        {
            return Source.FileExists(file);
        }


        internal void Dispose()
        {
            CloseSource();
        }
    }
}
