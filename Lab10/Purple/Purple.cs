namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        private T[] tasks;

        public PurpleFileManager<T>? Manager { get; private set; }
        public T[] Tasks => this.tasks;

        public Purple()
        {
            this.tasks = [];
            Manager = null;
        }

        public Purple(T[] tasks)
        {
            this.tasks = tasks == null ? [] : (T[])tasks.Clone();
            Manager = null;
        }

        public Purple(PurpleFileManager<T> manager, T[]? tasks = null)
        {
            Manager = manager;
            this.tasks = tasks == null ? [] : (T[])tasks.Clone();
        }

        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            this.tasks = tasks == null ? [] : (T[])tasks.Clone();
            Manager = manager;
        }

        public void Add(T task)
        {
            if (task == null)
            {
                return;
            }
            Array.Resize(ref this.tasks, this.tasks.Length + 1);
            this.tasks[^1] = task;
        }

        public void Add(T[] tasks)
        {
            if (tasks == null)
            {
                return;
            }
            foreach (T task in tasks)
            {
                this.Add(task);
            }
        }

        public void Remove(T task)
        {
            if (task == null || this.tasks.Length == 0)
            {
                return;
            }
            int index = Array.IndexOf(this.tasks, task);
            if (index == -1)
            {
                return;
            }
            T[] new_tasks = new T[this.tasks.Length - 1];
            Array.Copy(this.tasks, 0, new_tasks, 0, index);
            Array.Copy(this.tasks, index + 1, new_tasks, index, this.tasks.Length - index - 1);
            this.tasks = new_tasks;
        }

        public void Clear()
        {
            this.tasks = [];
            if (Manager != null && !string.IsNullOrEmpty(Manager.FolderPath) && Directory.Exists(Manager.FolderPath))
            {
                Directory.Delete(Manager.FolderPath, true);
            }
        }

        public void SaveTasks()
        {
            if (Manager == null)
            {
                return;
            }
            for (int i = 0; i < this.tasks.Length; i++)
            {
                Manager.ChangeFileName($"task{i}");
                Manager.Serialize(this.tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (Manager == null)
            {
                return;
            }
            for (int i = 0; i < this.tasks.Length; i++)
            {
                Manager.ChangeFileName($"task{i}");
                this.tasks[i] = Manager.Deserialize();
            }
        }

        public void ChangeManager(PurpleFileManager<T> manager)
        {
            if (manager == null)
            {
                return;
            }
            string folder_path = Manager != null && !string.IsNullOrEmpty(Manager.FolderPath)
                ? Manager.FolderPath
                : Path.Combine(Directory.GetCurrentDirectory(), manager.Name);
            Directory.CreateDirectory(folder_path);
            manager.SelectFolder(folder_path);
            Manager = manager;
        }
    }
}