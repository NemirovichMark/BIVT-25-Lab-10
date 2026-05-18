namespace Lab10.Purple
{
    public class Purple<T> where T: Lab9.Purple.Purple
    {
        private PurpleFileManager<T> _manager;
        private T[] _tasks;

        public PurpleFileManager<T> Manager => _manager;
        public T[] Tasks => _tasks;

        public Purple(T[] tasks)
        {
            _tasks = tasks ?? new T[0];
        }
        public Purple(PurpleFileManager<T> manager,  T[] tasks = null)
        {
            _manager = manager;
            _tasks = tasks ?? new T[0];
        }
        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            _manager = manager;
            _tasks = tasks ?? new T[0];
        }
        public Purple() 
        {
            _tasks = new T[0];
        }
        public void Add(T obj)
        {
            if (obj == null) return;
            
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = obj;
        }
        public void Add(T[] objs)
        {
            if (objs == null) return;
            for(int i = 0; i < objs.Length; i++)
            {
                Add(objs[i]);
            }
        }
        public void Remove(T obj)
        {
            if (obj == null) return;

            int index = -1;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == obj) index = i; break;
            }
            if (index == -1) return;

            T[] newTasks = new T[_tasks.Length - 1];
            for (int i = 0; i < newTasks.Length; i++)
            {
                if (i < index) newTasks[i] = _tasks[i];
                else newTasks[i] = _tasks[i + 1];
            }

            _tasks = newTasks;
        }
        public void Clear()
        {
            _tasks = new T[0];
            if (_manager != null && Directory.Exists(_manager.FolderPath)) 
            {
                Directory.Delete(_manager.FolderPath, true);
            }
        }
        public void SaveTasks()
        {
            if (_manager == null || _tasks == null) return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task{i}");
                _manager.Serialize(_tasks[i]);
            }
        }
        public void LoadTasks()
        {
            if (_manager == null || _tasks == null) return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task{i}");
                _tasks[i] = _manager.Deserialize();
            }   
        }
        public void ChangeManager(PurpleFileManager<T> newManager)
        {
            if (newManager == null) return;
            _manager = newManager;

            string folderPath = _manager.Name;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            _manager.SelectFolder(folderPath);
        }
    }
}
