namespace Fanex.Affiliate.User.Core.Utilities
{
    public class FileSystem : IFileSystem
    {
        public bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}