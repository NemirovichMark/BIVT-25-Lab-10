using System;
using System.IO;
using System.Linq;

namespace Lab10.Green
{
    public class Green
    {
        public GreenFileManager Manager { get; private set; }
        public Lab9.Green.Green[] Tasks { get; private set; }

        public Green()
        {
            Tasks = new Lab9.Green.Green[0];
        }

        public Green(Lab9.Green.Green[] tasks)
        {
            if (tasks == null) Tasks = new Lab9.Green.Green[0];
            else Tasks = tasks.ToArray();
        }

        public Green(GreenFileManager manager, Lab9.Green.Green[] tasks = null)
        {
            Manager = manager;
            if (tasks == null) Tasks = new Lab9.Green.Green[0];
            else Tasks = tasks.ToArray();
        }

        public void Add(Lab9.Green.Green task)
        {
            if (task == null) return;

            var newTasks = new Lab9.Green.Green[Tasks.Length + 1];

            for (int i = 0; i < Tasks.Length; i++)
                newTasks[i] = Tasks[i];

            newTasks[Tasks.Length] = task;
            Tasks = newTasks;
        }

        public void Add(Lab9.Green.Green[] tasks)
        {
            if (tasks == null) return;
            for (int i = 0; i < tasks.Length; i++)
                Add(tasks[i]);
        }

        public void Remove(Lab9.Green.Green task)
        {
            if (task == null || Tasks == null) return;
            Tasks = Tasks.Where(t => t != task).ToArray();
        }

        public void Clear()
        {
            Tasks = new Lab9.Green.Green[0];

            if (Manager == null) return;
            if (string.IsNullOrEmpty(Manager.FolderPath)) return;

            if (Directory.Exists(Manager.FolderPath))
                Directory.Delete(Manager.FolderPath, true);
        }

        public void SaveTasks()
        {
            if (Manager == null || Tasks == null) return;

            for (int i = 0; i < Tasks.Length; i++)
            {
                Manager.ChangeFileName("task_" + i);
                Manager.Serialize(Tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (Manager == null || Tasks == null) return;

            for (int i = 0; i < Tasks.Length; i++)
            {
                Manager.ChangeFileName("task_" + i);
                Tasks[i] = Manager.Deserialize<Lab9.Green.Green>();
            }
        }

        public void ChangeManager(GreenFileManager newManager)
        {
            if (newManager == null) return;
            Manager = newManager;

            if (!Directory.Exists(Manager.Name))
                Directory.CreateDirectory(Manager.Name);

            Manager.SelectFolder(Manager.Name);
        }
    }
}
