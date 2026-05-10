using System;

namespace Lab10.Purple
{
    public class Purple<T> where T : global::Lab9.Purple.Purple
    {
        private T[] tasks;
        private PurpleFileManager<T>? manager;

        public PurpleFileManager<T>? Manager => this.manager;
        public T[] Tasks => this.tasks;

        public Purple()
        {
            this.tasks = Array.Empty<T>();
            this.manager = null;
        }

        public Purple(T[] tasks)
        {
            this.tasks = tasks == null ? Array.Empty<T>() : (T[])tasks.Clone();
            this.manager = null;
        }

        public Purple(PurpleFileManager<T> manager, T[]? tasks = null)
        {
            this.manager = manager;
            this.tasks = tasks == null ? Array.Empty<T>() : (T[])tasks.Clone();
        }

        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            this.tasks = tasks == null ? Array.Empty<T>() : (T[])tasks.Clone();
            this.manager = manager;
        }

        public void Add(T task)
        {
            if (task == null)
                return;
            Array.Resize(ref this.tasks, this.tasks.Length + 1);
            this.tasks[this.tasks.Length - 1] = task;
        }

        public void Add(T[] tasks)
        {
            if (tasks == null)
                return;
            foreach (T task in tasks)
                this.Add(task);
        }

        public void Remove(T task)
        {
            if (task == null || this.tasks.Length == 0)
                return;
            int index = Array.IndexOf(this.tasks, task);
            if (index == -1)
                return;

            T[] new_tasks = new T[this.tasks.Length - 1];
            Array.Copy(this.tasks, 0, new_tasks, 0, index);
            Array.Copy(this.tasks, index + 1, new_tasks, index, this.tasks.Length - index - 1);
            this.tasks = new_tasks;
        }

        public void Clear()
        {
            this.tasks = Array.Empty<T>();
            if (this.manager != null && !string.IsNullOrEmpty(this.manager.FolderPath) && Directory.Exists(this.manager.FolderPath))
                Directory.Delete(this.manager.FolderPath, true);
        }

        public void SaveTasks()
        {
            if (this.manager == null)
                return;
            for (int i = 0; i < this.tasks.Length; i++)
            {
                this.manager.ChangeFileName($"task{i}");
                this.manager.Serialize(this.tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (this.manager == null)
                return;
            for (int i = 0; i < this.tasks.Length; i++)
            {
                this.manager.ChangeFileName($"task{i}");
                this.tasks[i] = this.manager.Deserialize();
            }
        }

        public void ChangeManager(PurpleFileManager<T> manager)
        {
            if (manager == null)
                return;
            string folder_path = this.manager != null && !string.IsNullOrEmpty(this.manager.FolderPath)
                ? this.manager.FolderPath
                : Path.Combine(Directory.GetCurrentDirectory(), manager.Name);
            Directory.CreateDirectory(folder_path);
            manager.SelectFolder(folder_path);
            this.manager = manager;
        }
    }
}