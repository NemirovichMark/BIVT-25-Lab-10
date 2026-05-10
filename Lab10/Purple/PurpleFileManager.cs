using System.IO;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : Lab10.MyFileManager, Lab10.ISerializer<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name)
        {
        }

        public PurpleFileManager(string name, string folderName, string fileName, string fileExtension = "txt")
            : base(name, folderName, fileName, fileExtension)
        {
        }

        public abstract void Serialize(T obj);

        public abstract T Deserialize();

        public override void EditFile(string text)
        {
            if (FullPath == null || FullPath == "")
            {
                return;
            }

            Directory.CreateDirectory(FolderPath);

            if (text == null)
            {
                File.WriteAllText(FullPath, "");
            }
            else
            {
                File.WriteAllText(FullPath, text);
            }
        }

        public override void ChangeFileExtension(string text)
        {
            base.ChangeFileExtension(text);
        }
    }
}