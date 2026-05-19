using Lab9.White;

namespace Lab10.White
{
    public abstract class WhiteFileManager : MyFileManager
    {
        protected WhiteFileManager(string name) : base(name) { }
        protected WhiteFileManager(string name, string folderPath, string fileName, string fileExtension)
            : base(name, folderPath, fileName, fileExtension) { }

        // Методы для массовой сериализации (для WhiteManagerTest)
        public abstract void SaveTasks(White[] tasks);
        public abstract White[] LoadTasks();

        // Методы для одиночной сериализации (для GeneralTest)
        public abstract void Serialize(White obj);
        public abstract White Deserialize();
    }
}
