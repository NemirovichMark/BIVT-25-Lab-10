namespace Lab10.White
{
    public class White
    {
        private Lab9.White.White[] _tasks;
        private WhiteFileManager? _manager;

        public WhiteFileManager? Manager => _manager;
        public Lab9.White.White[] Tasks => _tasks;

        public White(Lab9.White.White[]? tasks = null)
        {
            _tasks = CopyOrEmpty(tasks);
            _manager = null;
        }

        public White(WhiteFileManager manager, Lab9.White.White[]? tasks = null)
        {
            _manager = manager;
            _tasks = CopyOrEmpty(tasks);
        }

        public White(Lab9.White.White[]? tasks, WhiteFileManager manager)
        {
            _tasks = CopyOrEmpty(tasks);
            _manager = manager;
        }

        public void Add(Lab9.White.White task)
        {
            if (task == null)
            {
                return;
            }

            var arr = new Lab9.White.White[_tasks.Length + 1];
            for (int i = 0; i < _tasks.Length; i++)
            {
                arr[i] = _tasks[i];
            }
            arr[_tasks.Length] = task;
            _tasks = arr;
        }

        public void Add(Lab9.White.White[] tasks)
        {
            if (tasks == null)
            {
                return;
            }

            foreach (var t in tasks)
            {
                Add(t);
            }
        }

        public void Remove(Lab9.White.White task)
        {
            if (task == null || _tasks.Length == 0)
            {
                return;
            }

            int index = -1;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (ReferenceEquals(_tasks[i], task))
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)
            {
                return;
            }

            var array = new Lab9.White.White[_tasks.Length - 1];
            for (int i = 0, j = 0; i < _tasks.Length; i++)
            {
                if (i == index)
                {
                    continue;
                }
                array[j++] = _tasks[i];
            }
            _tasks = array;
        }

        public void Clear()
        {
            _tasks = new Lab9.White.White[0];

            if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath) && Directory.Exists(_manager.FolderPath))
            {
                Directory.Delete(_manager.FolderPath, true);
            }
        }

        public void SaveTasks()
        {
            if (_manager == null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName("task" + i);
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName("task" + i);
                var loaded = _manager.Deserialize();
                _tasks[i] = loaded!;
            }
        }

        public void ChangeManager(WhiteFileManager manager)
        {
            if (manager == null)
            {
                return;
            }

            _manager = manager;

            var folder = Path.Combine(Directory.GetCurrentDirectory(), manager.Name ?? string.Empty);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            _manager.SelectFolder(folder);
        }

        private static Lab9.White.White[] CopyOrEmpty(Lab9.White.White[]? src)
        {
            if (src == null)
            {
                return new Lab9.White.White[0];
            }

            var copy = new Lab9.White.White[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                copy[i] = src[i];
            }
            return copy;
        }
    }
}
