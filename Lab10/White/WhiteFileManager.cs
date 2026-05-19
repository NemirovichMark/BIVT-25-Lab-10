using Lab9.White;

namespace Lab10.White
{
    public abstract class WhiteFileManager : MyFileManager
    {
        protected WhiteFileManager(string name) : base(name) { }
        protected WhiteFileManager(string name, string folderPath, string fileName, string fileExtension)
            : base(name, folderPath, fileName, fileExtension) { }

        // Методы для одиночной сериализации (нужны для GeneralTest)
        public abstract void Serialize(White obj);
        public abstract White Deserialize();

        // Методы для массовой сериализации (нужны для WhiteManagerTest)
        // По умолчанию сохраняют/загружают только первый объект.
        // В наследниках (Txt, Json) их следует переопределить для работы с массивом.
        public virtual void SaveTasks(White[] tasks)
        {
            if (tasks == null || tasks.Length == 0) return;
            Serialize(tasks[0]);
        }

        public virtual White[] LoadTasks()
        {
            var task = Deserialize();
            return task != null ? new[] { task } : Array.Empty<White>();
        }
    }
}
