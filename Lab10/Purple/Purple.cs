using System.IO;
using System.Linq;

namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    private T[] _tasks = Array.Empty<T>();
    private PurpleFileManager<T> _manager;

    public T[] Tasks => _tasks;
    public PurpleFileManager<T> Manager => _manager;
    
    public Purple() 
    {
        _tasks = Array.Empty<T>();
    }

    public Purple(T[] tasks)
    {
        _tasks = tasks ?? Array.Empty<T>();
    }

    public Purple(PurpleFileManager<T> manager, T[] tasks = null)
    {
        _manager = manager;
        _tasks = tasks ?? Array.Empty<T>();
    }
    
    public Purple(T[] tasks, PurpleFileManager<T> manager)
    {
        _manager = manager;
        _tasks = tasks ?? Array.Empty<T>();
    }

    public void Add(T obj)
    {
        if (obj == null) return;
        
        Array.Resize(ref _tasks, _tasks.Length + 1);
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
        if (obj == null || _tasks == null || _tasks.Length == 0) return;
        
        int index = Array.IndexOf(_tasks, obj);
        if (index == -1) return;

        T[] newTasks = new T[_tasks.Length - 1];
        for (int i = 0, j = 0; i < _tasks.Length; i++)
        {
            if (i == index) continue;
            if (j < newTasks.Length) newTasks[j++] = _tasks[i];
        }

        _tasks = newTasks;
    }

    public void Clear()
    {
        _tasks = Array.Empty<T>();
        if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath) && Directory.Exists(_manager.FolderPath))
        {
            Directory.Delete(_manager.FolderPath, true);
        }
    }

    public void SaveTasks()
    {
        if (_manager == null || _tasks == null) return;
        for (int i = 0; i < _tasks.Length; i++)
        {
            _manager.ChangeFileName($"task_{i+1}");
            _manager.Serialize(_tasks[i]);
        }
    }

    public void LoadTasks()
    {
        if (_manager == null || _tasks == null) return;
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
