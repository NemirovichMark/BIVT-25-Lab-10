namespace Lab10.White
{
    public abstract class WhiteFileManager : MyFileManager
    {
        protected WhiteFileManager(string name) : base(name) { }
        protected WhiteFileManager(string name, string folderPath, string fileName, string fileExtension)
            : base(name, folderPath, fileName, fileExtension) { }

        public abstract void SaveTasks(Lab9.White.White[] tasks);
        public abstract Lab9.White.White[] LoadTasks();
    }
}
