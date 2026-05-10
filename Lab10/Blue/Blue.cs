namespace Lab10.Blue;

public class Blue<T> where T : Lab9.Blue.Blue
{
    private BlueFileManager<T> _manager;
    private T[] _tasks;
    
    public BlueFileManager<T> Manager => _manager;
    public T[] Tasks => _tasks;

    public Blue()
    {
        _tasks = new T[0];
    }
    
    public Blue(T[] tasks)
    {
        if (tasks == null)
        {
            _tasks = new T[0];
        }
        else
        {
            _tasks = new T[tasks.Length];
            Array.Copy(tasks,_tasks, tasks.Length);
        }
    }
    public Blue(BlueFileManager<T> manager, T[] tasks = null)
    {
        _manager = manager;
        
        if (tasks == null)
        {
            _tasks = new T[0];
        }
        else
        {
            _tasks = new T[tasks.Length];
            Array.Copy(tasks,_tasks, tasks.Length);
        }
    }
    public Blue(T[] tasks, BlueFileManager<T> manager)
    {
        _manager = manager;
        
        if (tasks == null)
        {
            _tasks = new T[0];
        }
        else
        {
            _tasks = new T[tasks.Length];
            Array.Copy(tasks,_tasks, tasks.Length);
        }
    }

    public void Add(T task)
    {
        if (task == null) return;
        
        Array.Resize(ref _tasks, _tasks.Length+1);
        _tasks[^1] = task;
    }

    public void Add(T[] tasks)
    {
        if (tasks == null) return;
        for (int i = 0; i < tasks.Length; i++)
        {
            Add(tasks[i]);
        }
    }

    public void Remove(T task)
    {
        if (task == null) return;
        _tasks = _tasks.Where(x => x != task).ToArray();
    }

    public void Clear()
    {
        _tasks = new T[0];

        if (_manager == null) return;
        if (Directory.Exists(_manager.FolderPath))
        {
            Directory.Delete(_manager.FolderPath, true);
        }
    }
    
    public void SaveTasks()
    {
        if (_manager == null) return;

        for (int i = 0; i < _tasks.Length; i++)
        {
            if (_tasks[i] == null) continue;

            _manager.ChangeFileName(i.ToString());
            _manager.Serialize(_tasks[i]);
        }
    }
    
    public void LoadTasks()
    {
        if (_manager == null) return;
        
        for (int i = 0; i < _tasks.Length; i++)
        {
            _manager.ChangeFileName(i.ToString());
            _tasks[i] = _manager.Deserialize();
        }
    }
    
    public void ChangeManager(BlueFileManager<T> manager)
    {
        if (manager == null) return;
        
        _manager = manager;

        if (!Directory.Exists(_manager.Name))
        {
            Directory.CreateDirectory(_manager.Name);
        }
        _manager.SelectFolder(_manager.Name);
    }
}