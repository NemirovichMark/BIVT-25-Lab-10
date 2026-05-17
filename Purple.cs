namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    private PurpleFileManager<T> _manager;
    private T[] _tasks;
    public PurpleFileManager<T> Manager => _manager;
    public T[] Tasks => _tasks;

    public Purple()
    {
        _manager = null;
        _tasks = Array.Empty<T>();
    }
    public Purple(T[] tasks)
    {
        _manager = null;
        _tasks = tasks.ToArray() ?? Array.Empty<T>();
    }

    public Purple(PurpleFileManager<T> manager, T[] tasks)
    {
        _manager = manager;
        _tasks = tasks.ToArray() ?? Array.Empty<T>();

    }
    public Purple(T[] tasks, PurpleFileManager<T> manager )
    {
        _manager = manager;
        _tasks = tasks.ToArray() ?? Array.Empty<T>();
    }


    public void Add(T obj)
    {
        if (_tasks == null) return;
        if (obj == null) return;
        Array.Resize(ref _tasks, _tasks.Length + 1);
        _tasks[^1] = obj;
    }

    public void Add(T[] tasks)
    {
        if (tasks == null) return;
        foreach(T obj in tasks)
        {
            Add(obj);
        }
    }

    public void Remove(T obj)
    {
        if (_tasks == null) return;
        if (obj == null) return;
        _tasks = _tasks.Where(x => x != obj).ToArray();
    }

    public void Clear()
    {
        _tasks = Array.Empty<T>();
        if (!Directory.Exists(_manager.FolderPath)) return;
        Directory.Delete(_manager.FolderPath, true);
    }

    public void SaveTasks()
    {
        if (_tasks == null) return;
        for (int i = 0; i < _tasks.Length; i++)
        {
            _manager.ChangeFileName($"Task {i}");
            _manager.Serialize(_tasks[i]);
        }
    }

    public void LoadTasks()
    {
        var tasks = new List<T>();

        for (int i = 0; ; i++)
        {
            _manager.ChangeFileName($"Task {i}");
            if (!File.Exists(_manager.FullPath)) break;
            T obj = _manager.Deserialize();
            tasks.Add(obj);
        }

        _tasks = tasks.ToArray();
    }

    public void ChangeManager(PurpleFileManager<T> manager)
    {
        if (manager == null) return;
        _manager = manager;
        string folderPath = Path.GetFullPath(_manager.Name);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        _manager.SelectFolder(folderPath);
    }
}