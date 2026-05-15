namespace Lab10.Blue
{
    public class Blue<T> where T : Lab9.Blue.Blue
    {
        private BlueFileManager<T>? _manager;
        private T[] _tasks;

        public Blue(T[]? tasks = null)
        {
            _manager = null;
            _tasks = CopyTasks(tasks);
        }

        public Blue(BlueFileManager<T>? manager, T[]? tasks = null)
        {
            _manager = manager;
            _tasks = CopyTasks(tasks);
        }

        public Blue(T[]? tasks, BlueFileManager<T>? manager)
        {
            _manager = manager;
            _tasks = CopyTasks(tasks);
        }

        public BlueFileManager<T>? Manager => _manager;
        public T[] Tasks => _tasks ?? Array.Empty<T>();

        public void Add(T task)
        {
            if (task is null)
            {
                return;
            }

            T[] result = new T[_tasks.Length + 1];
            Array.Copy(_tasks, result, _tasks.Length);
            result[^1] = task;
            _tasks = result;
        }

        public void Add(T[]? tasks)
        {
            if (tasks is null)
            {
                return;
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                Add(tasks[i]);
            }
        }

        public void Remove(T task)
        {
            if (task is null || _tasks.Length == 0)
            {
                return;
            }

            int index = -1;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(_tasks[i], task))
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                return;
            }

            T[] result = new T[_tasks.Length - 1];
            if (index > 0)
            {
                Array.Copy(_tasks, 0, result, 0, index);
            }

            if (index < _tasks.Length - 1)
            {
                Array.Copy(_tasks, index + 1, result, index, _tasks.Length - index - 1);
            }

            _tasks = result;
        }

        public void Clear()
        {
            _tasks = Array.Empty<T>();

            if (_manager is null)
            {
                return;
            }

            string folderPath = _manager.FolderPath;
            if (!string.IsNullOrWhiteSpace(folderPath) && Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }

        public void SaveTasks()
        {
            if (_manager is null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)
            {
                T task = _tasks[i];
                if (task is null)
                {
                    continue;
                }

                _manager.ChangeFileName(GetTaskFileName(i));
                _manager.Serialize(task);
            }
        }

        public void LoadTasks()
        {
            if (_manager is null)
            {
                return;
            }

            if (_tasks.Length > 0)
            {
                T[] loaded = new T[_tasks.Length];
                for (int i = 0; i < loaded.Length; i++)
                {
                    _manager.ChangeFileName(GetTaskFileName(i));
                    loaded[i] = _manager.Deserialize()!;
                }

                _tasks = loaded;
                return;
            }

            string folderPath = _manager.FolderPath;
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                _tasks = Array.Empty<T>();
                return;
            }

            string extension = _manager.FileExtension;
            string searchPattern = string.IsNullOrWhiteSpace(extension) ? "*" : $"*.{extension}";
            string[] files = Directory.GetFiles(folderPath, searchPattern);
            Array.Sort(files, StringComparer.Ordinal);

            List<T> loadedTasks = new List<T>();
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(files[i]);
                _manager.ChangeFileName(fileName);
                T? task = _manager.Deserialize();
                if (task is not null)
                {
                    loadedTasks.Add(task);
                }
            }

            _tasks = loadedTasks.ToArray();
        }

        public void ChangeManager(BlueFileManager<T>? manager)
        {
            if (manager is null)
            {
                return;
            }

            string parentFolder = manager.FolderPath;
            if (string.IsNullOrWhiteSpace(parentFolder) && _manager is not null)
            {
                parentFolder = _manager.FolderPath;
            }

            if (string.IsNullOrWhiteSpace(parentFolder))
            {
                parentFolder = Directory.GetCurrentDirectory();
            }

            string managerFolder = string.IsNullOrWhiteSpace(manager.Name)
                ? parentFolder
                : Path.Combine(parentFolder, manager.Name);

            Directory.CreateDirectory(managerFolder);
            manager.SelectFolder(managerFolder);
            _manager = manager;
        }

        private static T[] CopyTasks(T[]? tasks)
        {
            if (tasks is null || tasks.Length == 0)
            {
                return Array.Empty<T>();
            }

            T[] copy = new T[tasks.Length];
            Array.Copy(tasks, copy, tasks.Length);
            return copy;
        }

        private static string GetTaskFileName(int index)
        {
            return $"task{index}";
        }
    }
}
