using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        public PurpleFileManager<T> Manager { get; private set; }
        public T[] Tasks { get; private set; }

        public Purple() { }
        
        public Purple(T[] tasks)
        {
            if (tasks == null)
            {
                Tasks = [];
            }
            else
            {
                Tasks = tasks;
            }
        }
        public Purple(PurpleFileManager<T> manager, T[]? tasks)
        {
            Manager = manager;
            if (tasks != null)
            {
                Tasks = tasks;
            } else
            {
                Tasks = [];
            }
        }
        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            if (tasks == null)
            {
                Tasks = [];
            }
            else
            {
                Tasks = tasks;
            }
            Manager = manager;
        }

        public void Add(T task)
        {
            T[] newTasks = new T[Tasks.Length+1];
            for (int i = 0; i< Tasks.Length; i++)
            {
                newTasks[i] = Tasks[i];
            }
            newTasks[^1] = task;
            for (int i = 0; i < newTasks.Length; i++)
            {
                Tasks[i] = newTasks[i];
            }
        }

        public void Add(T[] tasks)
        {
            T[] newTasks = new T[Tasks.Length + tasks.Length];
            for (int i = 0; i < Tasks.Length; i++)
            {
                newTasks[i] = Tasks[i];
            }
            int index = 0;
            for (int i = Tasks.Length-1; i < newTasks.Length; i++)
            {
                newTasks[i] = tasks[index++];
            }
            for (int i = 0; i < newTasks.Length; i++)
            {
                Tasks[i] = newTasks[i];
            }
        }

        public void Remove(T task)
        {
            if (Tasks.Length > 0)
            {
                T[] newTasks = new T[Tasks.Length - 1];
                int index = 0;
                for (int i = 0; i<Tasks.Length; i++)
                {
                    if (Tasks[i] != task)
                    {
                        newTasks[index++] = Tasks[i];
                    }
                }

                Tasks = new T[newTasks.Length];
                for (int i = 0; i < newTasks.Length; i++)
                {
                    Tasks[i] = newTasks[i];
                }
            }
        }

        public void Clear()
        {
            Tasks = new T[0];
            Directory.Delete(Manager.FolderPath, true);
        }


        public void SaveTasks()
        {
            int index = 0;
            foreach (var task in Tasks)
            {
                this.Manager.ChangeFileName(index.ToString());
                this.Manager.Serialize(task);
                index++;
            }
        }

        public void LoadTasks()
        {
            Clear();

            int index = 0;

            while (true)
            {
                Manager.ChangeFileName(index.ToString());

                if (!File.Exists(Manager.FileName))
                    break;

                var task = Manager.Deserialize();
                Add(task);

                index++;
            }
        }

        public void ChangeManager(PurpleFileManager<T> manager)
        {
            Manager = manager;
            if (!Directory.Exists(Manager.Name))
            {
                Directory.CreateDirectory(Manager.Name);
                Manager.SelectFolder(Manager.Name);
            }
        }
    }
}
