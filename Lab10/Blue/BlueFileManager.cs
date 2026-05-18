using System.IO;

namespace Lab10.Blue
{
    public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T>
        where T : Lab9.Blue.Blue
    {
        protected BlueFileManager(string name) : base(name)
        {
        }

        protected BlueFileManager(string name, string folder, string file, string ext = "txt")
            : base(name, folder, file, ext)
        {
        }

        public override void EditFile(string content)
        {
            if (File.Exists(FullPath))
            {
                T obj = Deserialize();

                if (obj != null)
                {
                    obj.ChangeText(content);

                    Serialize(obj);
                }
            }
        }

        public override void ChangeFileExtension(string extension)
        {
            if (!string.IsNullOrEmpty(extension))
            {
                ChangeFileFormat(extension);
            }
        }

        public abstract void Serialize(T obj);

        public abstract T Deserialize();
    }
}
