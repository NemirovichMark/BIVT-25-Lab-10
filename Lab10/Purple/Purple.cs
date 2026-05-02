namespace Lab10.Purple;

public class Purple<T> where T:Lab9.Purple.Purple
{
    private PurpleFileManager<T> _manager;
    private T[] _tasks;
    public PurpleFileManager<T> Manager => _manager;
    public T[] Tasks => _tasks;

    public Purple(T[] tasks)
    {
        _manager = null;
        T[] array = new T[tasks.Length];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = tasks[i];
        }

        _tasks = array;
    }

    public Purple(PurpleFileManager<T> manager, T[] tasks = null)
    {
        _manager = manager;
        if (tasks == null)
        {
            _tasks = new T[0];
        }
        else
        {
            T[] array = new T[tasks.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = tasks[i];
            }

            _tasks = array;
        }
    }
    public Purple()
    {
        _manager = null;
        _tasks = new T[0];
    }

    public void Add(T obj)
    {
        if (_tasks == null) return;
        if (obj == null) return;
        Array.Resize(ref _tasks,_tasks.Length+1);
        _tasks[_tasks.Length - 1] = obj;
    }

    public void Add(T[] tasks)
    {
        if (tasks == null) return;
        for (int i = 0; i < tasks.Length; i++)
        {
            Add(tasks[i]);
        }
    }

    public void Remove(T obj)
    {
        if (_tasks == null) return;
        if (obj == null) return;
        int index = -1;
        for (int i = 0; i < _tasks.Length; i++)
        {
            if (_tasks[i] == obj)
            {
                index = i;
            }
        }

        if (index == -1) return;
        T[] array = new T[_tasks.Length - 1];
        for (int i = 0; i < array.Length; i++)
        {
            if (i < index)
            {
                array[i] = _tasks[i];
            }
            else
            {
                array[i] = _tasks[i + 1];
            }
        }

        _tasks = array;
    }

    public void Clear()
    {
        if (_tasks == null) return;
        for (int i = 0; i < _tasks.Length; i++)
        {
            Remove(_tasks[i]);
            i--;
        }

        if (!Directory.Exists(_manager.FolderPath)) return;
        Directory.Delete(_manager.FolderPath,true);
    }

    public void SaveTasks()
    {
        if (_tasks == null) return;
        for (int i = 0; i < _tasks.Length; i++)
        {
            if (_tasks[i] is Lab9.Purple.Task1)
            {
                _manager.ChangeFileName("task0");
            }
            else if (_tasks[i] is Lab9.Purple.Task2)
            {
                _manager.ChangeFileName("task1");
            }
            else if (_tasks[i] is Lab9.Purple.Task3)
            {
                _manager.ChangeFileName("task2");
            }
            else if (_tasks[i] is Lab9.Purple.Task4)
            {
                _manager.ChangeFileName("task3");
            }
            _manager.Serialize(_tasks[i]);
        }
    }

    public void LoadTasks()
    {
        if (_tasks == null) return;
        int i1 = 0;
        while (true)
        {
            _manager.ChangeFileName($"task{i1}");
            if (!File.Exists(_manager.FullPath)) break;
            T obj = _manager.Deserialize();
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = obj;
            i1++;
        }
    }

    public void ChangeManager(PurpleFileManager<T> manager)
    {
        if (manager == null) return;
        _manager = manager;
        _tasks = new T[0];
        if (!Directory.Exists(_manager.FolderPath))
        {
            Directory.CreateDirectory(_manager.Name);
            _manager.SelectFolder(_manager.Name);
        }
    }
}
