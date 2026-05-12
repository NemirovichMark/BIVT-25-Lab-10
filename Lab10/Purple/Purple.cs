namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        private PurpleFileManager<T> _manager;
        private T[] _tasks;

        public PurpleFileManager<T> Manager => _manager;
        public T[] Tasks => _tasks.ToArray();

        public Purple(T[] tasks = null)
        {
            _manager = null;
            _tasks = CopyTasks(tasks);
        }

        public Purple(PurpleFileManager<T> manager, T[] tasks = null)
        {
            _manager = manager;
            _tasks = CopyTasks(tasks);
        }

        public void Add(T obj)
        {
            if (obj == null)
                return;

            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = obj;
        }

        public void Add(T[] tasks)
        {
            if (tasks == null)
                return;

            foreach (T task in tasks)
                Add(task);
        }

        public void Remove(T obj)
        {
            if (obj == null || _tasks == null)
                return;

            List<T> result = new List<T>();

            foreach (T task in _tasks)
            {
                if (ReferenceEquals(task, obj))
                {
                    continue;
                }

                result.Add(task);
            }

            _tasks = result.ToArray();
        }

        public void Clear()
        {
            _tasks = Array.Empty<T>();

            if (Directory.Exists(_manager.FolderPath))
                Directory.Delete(_manager.FolderPath, true);
        }

        public void SaveTasks()
        {
            if (_manager == null || _tasks == null)
                return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null || _tasks == null)
                return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                _tasks[i] = _manager.Deserialize();
            }
        }

        public void ChangeManager(PurpleFileManager<T> manager)
        {
            if (manager == null)
                return;

            string baseFolder = Directory.GetCurrentDirectory();
            string folder = Path.Combine(baseFolder, manager.Name);
            Directory.CreateDirectory(folder);

            manager.SelectFolder(folder);
            _manager = manager;
        }

        private static T[] CopyTasks(T[] tasks)
        {
            if (tasks == null)
                return Array.Empty<T>();

            T[] copy = new T[tasks.Length];
            Array.Copy(tasks, copy, tasks.Length);
            return copy;
        }
    }
}
