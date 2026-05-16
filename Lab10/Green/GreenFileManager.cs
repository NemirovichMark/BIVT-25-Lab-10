using System.IO;

namespace Lab10.Green
{
    public abstract class GreenFileManager : MyFileManager, ISerializer
    {
        public GreenFileManager(string name) : base(name)
        {
            ChangeFileFormat("txt");
        }

        public GreenFileManager(string name, string folder, string fileName, string ext = "txt")
            : base(name, folder, fileName, ext)
        {
            ChangeFileFormat(ext);
        }

        public override void EditFile(string text)
        {
            base.EditFile(text);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            base.ChangeFileExtension(newExtension);
        }

        public abstract T Deserialize<T>() where T : Lab9.Green.Green;
        public abstract void Serialize<T>(T obj) where T : Lab9.Green.Green;
    }
}
