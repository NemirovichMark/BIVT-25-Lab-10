namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    private PurpleFileManager<T>? _manager;
    private T?[] _tasks;

    public Purple() : this((T[]?)null)
    {
    }

    public Purple(T[]? tasks)
    {
        _manager = null;
        _tasks = CopyTasks(tasks);
    }

    public Purple(PurpleFileManager<T>? manager, T[]? tasks = null)
    {
        _manager = manager;
        _tasks = CopyTasks(tasks);
    }

    public Purple(T[]? tasks, PurpleFileManager<T>? manager) : this(manager, tasks)
    {
    }

    public PurpleFileManager<T>? Manager => _manager;

    public T?[] Tasks => _tasks.ToArray();

    public void Add(T task)
    {
        if (task == null)
        {
            return;
        }

        Array.Resize(ref _tasks, _tasks.Length + 1);
        _tasks[^1] = task;
    }

    public void Add(T[]? tasks)
    {
        if (tasks == null)
        {
            return;
        }

        foreach (T? task in tasks)
        {
            if (task != null)
            {
                Add(task);
            }
        }
    }

    public void Remove(T task)
    {
        if (task == null || _tasks.Length == 0)
        {
            return;
        }

        int index = -1;

        for (int i = 0; i < _tasks.Length; i++)
        {
            if (ReferenceEquals(_tasks[i], task) || EqualityComparer<T?>.Default.Equals(_tasks[i], task))
            {
                index = i;
                break;
            }
        }

        if (index < 0)
        {
            return;
        }

        T?[] result = new T?[_tasks.Length - 1];

        for (int i = 0, j = 0; i < _tasks.Length; i++)
        {
            if (i == index)
            {
                continue;
            }

            result[j++] = _tasks[i];
        }

        _tasks = result;
    }

    public void Clear()
    {
        string folderPath = _manager?.FolderPath ?? string.Empty;
        _tasks = [];

        if (!string.IsNullOrWhiteSpace(folderPath) && Directory.Exists(folderPath))
        {
            Directory.Delete(folderPath, true);
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
            if (_tasks[i] == null)
            {
                continue;
            }

            _manager.ChangeFileName($"task{i}");
            _manager.Serialize(_tasks[i]!);
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
            _manager.ChangeFileName($"task{i}");
            _tasks[i] = _manager.Deserialize();
        }
    }

    public void ChangeManager(PurpleFileManager<T>? manager)
    {
        if (manager == null)
        {
            return;
        }

        string rootFolder = Directory.GetCurrentDirectory();
        string currentFolder = _manager?.FolderPath ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(currentFolder))
        {
            string? parentFolder = Directory.GetParent(currentFolder)?.FullName;

            if (!string.IsNullOrWhiteSpace(parentFolder))
            {
                rootFolder = parentFolder;
            }
        }

        string managerName = manager.Name ?? string.Empty;
        string folder = Path.Combine(rootFolder, managerName);
        Directory.CreateDirectory(folder);
        manager.SelectFolder(folder);
        _manager = manager;
    }

    private static T?[] CopyTasks(T[]? tasks)
    {
        if (tasks == null)
        {
            return [];
        }

        T?[] result = new T?[tasks.Length];

        for (int i = 0; i < tasks.Length; i++)
        {
            result[i] = tasks[i];
        }

        return result;
    }
}
