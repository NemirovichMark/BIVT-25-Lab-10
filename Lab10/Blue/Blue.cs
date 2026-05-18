namespace Lab10.Blue
{
    public class Blue<T> where T : Lab9.Blue.Blue
    {
        private T[] _tasks;
        private BlueFileManager<T> _manager;

        public BlueFileManager<T> Manager => _manager;
        public T[] Tasks => _tasks != null ? (T[])_tasks.Clone() : Array.Empty<T>();

        public Blue() { _tasks = Array.Empty<T>(); }

        public Blue(T[] tasks) { _tasks = tasks != null ? (T[])tasks.Clone() : Array.Empty<T>(); }

        public Blue(BlueFileManager<T> manager, T[] tasks = null)
        {
            _manager = manager;
            _tasks = tasks != null ? (T[])tasks.Clone() : Array.Empty<T>();
        }
        public Blue(T[] tasks, BlueFileManager<T> manager) : this(manager, tasks) { }

        public void Add(T obj)
        {
            if (obj == null) return;
            Array.Resize(ref _tasks, (_tasks?.Length ?? 0) + 1);
            _tasks[^1] = obj;
        }

        public void Add(T[] objects) { if (objects != null) foreach (var o in objects) Add(o); }

        public void Remove(T obj)
        {
            if (_tasks == null || obj == null) return;
            int index = Array.IndexOf(_tasks, obj);
            if (index == -1) return;
            T[] temp = new T[_tasks.Length - 1];
            Array.Copy(_tasks, 0, temp, 0, index);
            Array.Copy(_tasks, index + 1, temp, index, _tasks.Length - index - 1);
            _tasks = temp;
        }

        public void Clear()
        {
            _tasks = Array.Empty<T>();
            if (_manager != null && Directory.Exists(_manager.FolderPath))
                Directory.Delete(_manager.FolderPath, true);
        }

        public void SaveTasks()
        {
            if (_manager == null || _tasks == null) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == null) continue;
                _manager.ChangeFileName($"Task{i + 1}");
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null || _tasks == null) return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"Task{i + 1}");

                if (File.Exists(_manager.FullPath))
                {
                    _tasks[i] = _manager.Deserialize();
                }
                else
                {
                    _tasks[i] = null;
                }
            }
        }

        public void ChangeManager(BlueFileManager<T> newManager)
        {
            if (newManager == null || _manager == null) return;

            string newPath = Path.Combine(_manager.FolderPath, newManager.Name);

            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            newManager.SelectFolder(newPath);
            _manager = newManager;
        }
    }
}