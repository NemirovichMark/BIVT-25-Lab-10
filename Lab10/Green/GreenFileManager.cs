namespace Lab10.Green
{
    public abstract class GreenFileManager : MyFileManager, ISerializer
    {
        public GreenFileManager(string name) : base(name) { }

        public GreenFileManager(string name, string folder, string fileName, string extension = "txt")
            : base(name, folder, fileName, extension) { }

        public abstract void Serialize<T>(T obj) where T : Lab9.Green.Green;

        public abstract T Deserialize<T>() where T : Lab9.Green.Green;

        public override void EditFile(string input)
        {
            base.EditFile(input);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            base.ChangeFileExtension(newExtension);
        }
    }
}
