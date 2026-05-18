namespace Lab10.Blue
{
    public class Blue<T> where T : Lab9.Blue.Blue
    {
        private T[] _tasks;
        private BlueFileManager<T> _manager;
        public BlueFileManager<T> Manager => _manager;
        public T[] Tasks => (T[])_tasks.Clone();

        public Blue()
        {
            _tasks = Array.Empty<T>();
        }
        public Blue(T[] tasks)
        {
            _tasks = tasks != null ? tasks : Array.Empty<T>();
        }

        public Blue(BlueFileManager<T> manager, T[] tasks = null)
        {
            _manager = manager;
            _tasks = tasks != null ? tasks : Array.Empty<T>();
        }

        public Blue(T[] tasks, BlueFileManager<T> manager)
        {
            _tasks = tasks != null ? tasks : Array.Empty<T>();
            _manager = manager;
        }

        public void Add(T obj)
        {
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[^1] = obj;
        }

        public void Add(T[] objects)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                Add(objects[i]);
            }
        }

        public void Remove(T obj)
        {
            int index = Array.IndexOf(_tasks, obj);
            T[] temp = new T[_tasks.Length - 1];
            Array.Copy(_tasks, 0, temp, 0, index);
            Array.Copy(_tasks, index+1, temp, index, _tasks.Length - index - 1);
            _tasks = temp;
        }

        public void Clear()
        {
            _tasks = Array.Empty<T>();
            Directory.Delete(_manager.FolderPath);
        }

        public void SaveTasks()
        {
            if (_manager == null || _tasks == null || _tasks.Length == 0) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] is Lab9.Blue.Task1) 
                    _manager.ChangeFileName("Task1");
                if (_tasks[i] is Lab9.Blue.Task2)
                    _manager.ChangeFileName("Task2");
                if (_tasks[i] is Lab9.Blue.Task3)
                    _manager.ChangeFileName("Task3");
                if (_tasks[i] is Lab9.Blue.Task4)
                    _manager.ChangeFileName("Task4");
                
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            for (int i = 0; i < _tasks.Length; i++)
            {
                _tasks[i] = _manager.Deserialize();
            }
        }

        public void ChangeManager(BlueFileManager<T> newManager)
        { 
            var folderPath = Path.Combine(_manager.FolderPath ?? Directory.GetCurrentDirectory(), _manager.Name);
            Directory.CreateDirectory(folderPath);
            newManager.SelectFolder(folderPath);
            _manager = newManager;
            
        }
    }
}