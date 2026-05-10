namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    private PurpleFileManager<T> _manager;
    private List<T> _tasks;
    
    public PurpleFileManager<T> Manager => _manager;
    public T[] Tasks => _tasks.ToArray();

    public Purple()
    {
        _tasks = new List<T>();
    }
    
    public Purple(T[] tasks)
    {
        _tasks = new List<T>(tasks); 
    }
    
    public Purple(PurpleFileManager<T> manager, T[]? tasks = null)
    {
        _manager = manager;
        if (tasks != null)
        {
            _tasks = new List<T>(tasks);
        }

    }
    
    public Purple(T[] tasks, PurpleFileManager<T> manager)
    {
        _manager = manager;
        _tasks = new List<T>(tasks); 
    }

    public void Add(T task)
    {
        _tasks.Add(task);
    }
    
    public void Add(T[] task)
    {
        _tasks.AddRange(task);
    }

    public void Remove(T task)
    {
        _tasks.Remove(task);
    }

    public void Clear()
    {
        _tasks.Clear();
        if (_manager == null)
            return;
        if (Directory.Exists(_manager.FolderPath))
            Directory.Delete(_manager.FolderPath, true);
    }

    public void SaveTasks()
    {
        int number = 0;
        
        foreach (T task in _tasks)
        {
            _manager.ChangeFileName(number.ToString());
            
            _manager.Serialize(task);
            number++;
        }
    }

    public void LoadTasks()
    {
        for(int i = 0; i < _tasks.Count; i++) 
        {
            _manager.ChangeFileName(i.ToString());
            T task = _manager.Deserialize();
            _tasks[i] = task; //обнова для каждого элемента
        }
    }

    public void ChangeManager(PurpleFileManager<T> manager)
    {
        _manager = manager;
        string currentDirectory = Directory.GetCurrentDirectory();
        string path = Path.Combine(currentDirectory, _manager.Name);
        Directory.CreateDirectory(path);
        _manager.SelectFolder(path);
    }
}