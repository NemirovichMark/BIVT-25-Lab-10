using System.IO;

namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    private T[] _tasks;
    private PurpleFileManager<T> _manager;

    public T[] Tasks => _tasks.ToArray();
    public PurpleFileManager<T> Manager => _manager;
    
    public Purple(T[] tasks)
    {
        _tasks = (tasks != null) ? (T[])tasks.Clone() : new T[0];
    }

    public Purple(PurpleFileManager<T> manager, T[] tasks = null)
    {
        _manager = manager;
        _tasks = (tasks != null) ? (T[])tasks.Clone() : new T[0];
    }
    
    public Purple(T[] tasks, PurpleFileManager<T> manager)
    {
        _manager = manager;
        _tasks = (tasks != null) ? (T[])tasks.Clone() : new T[0];
    }

    public Purple() : base()
    {
        
    }

    public void Add(T obj)
    {
        if (obj == null) return;
        
        Array.Resize(ref _tasks, _tasks.Length+1);
        _tasks[_tasks.Length - 1] = obj;
    }

    public void Add(T[] obj)
    {
        if (obj == null) return;
        
        foreach(T i in obj)
            Add(i);
    }

    public void Remove(T obj)
    {
        if (obj == null) return;
        int index = Array.IndexOf(_tasks, obj);
        if (index == -1) return;

        T[] newTasks = new T[_tasks.Length - 1];
        int j = 0;
        for (int i = 0; i < newTasks.Length; i++)
        {
            if (i != index) newTasks[j++] = _tasks[i];
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
        if (_manager == null) return;
        for (int i = 0; i < _tasks.Length; i++)
        {
            _manager.ChangeFileName($"task_{i+1}");
            _manager.Serialize(_tasks[i]);
        }
    }

    public void LoadTasks()
    {
        if (_manager == null) return;
        for (int i = 0; i < _tasks.Length; i++)
        {
            _manager.ChangeFileName($"task_{i+1}");
            _tasks[i] = _manager.Deserialize();
        }
    }

    public void ChangeManager(PurpleFileManager<T> newManager)
    {
        if (newManager == null) return;
        _manager = newManager;

        if (!Directory.Exists(_manager.Name))
        {
            Directory.CreateDirectory(_manager.Name);
        }
        
        _manager.SelectFolder(_manager.Name);
    }
}