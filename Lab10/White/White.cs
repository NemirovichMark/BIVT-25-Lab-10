using System.Linq;

namespace Lab10.White
{
    public class White
    {
        public WhiteFileManager Manager { get; private set; }
        public Lab9.White.White[] Tasks { get; private set; }

        public White() : this(new WhiteTxtFileManager("txt"), System.Array.Empty<Lab9.White.White>()) { }

        public White(WhiteFileManager manager, Lab9.White.White[] tasks)
        {
            Manager = manager;
            Tasks = tasks ?? System.Array.Empty<Lab9.White.White>();
        }

        public void Add(Lab9.White.White task)
        {
            if (task == null) return;
            var list = Tasks.ToList();
            list.Add(task);
            Tasks = list.ToArray();
        }

        public void Add(Lab9.White.White[] newTasks)
        {
            if (newTasks == null) return;
            var list = Tasks.ToList();
            list.AddRange(newTasks);
            Tasks = list.ToArray();
        }

        public void Remove(Lab9.White.White task)
        {
            if (task == null) return;
            var list = Tasks.ToList();
            list.Remove(task);
            Tasks = list.ToArray();
        }

        public void Clear()
        {
            Tasks = System.Array.Empty<Lab9.White.White>();
            Manager?.DeleteFile();
        }

        public void SaveTasks()
        {
            Manager?.SaveTasks(Tasks);
        }

        public void LoadTasks()
        {
            var loaded = Manager?.LoadTasks();
            Tasks = loaded ?? System.Array.Empty<Lab9.White.White>();
        }

        public void ChangeManager(WhiteFileManager newManager)
        {
            Manager = newManager;
        }
    }
}
