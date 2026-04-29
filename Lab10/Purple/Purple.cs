namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    private T[] _tasks;

    public PurpleFileManager<T> Manager {get; private set;}
    public T[] Tasks => _tasks;

    public Purple (T[] tasks) => _tasks = tasks ?? Array.Empty<T>();
    public Purple (PurpleFileManager<T> manager, T[]? tasks)
    {
        Manager = manager;
        _tasks = tasks ?? Array.Empty<T>();
    }
    public Purple (T[] tasks, PurpleFileManager<T> manager)
    {
        Manager = manager;
        _tasks = tasks ?? Array.Empty<T>();
    }

    public void Add (T task)
    {
        if (task == null) return;
        Array.Resize(ref _tasks, Tasks.Length+1);
        _tasks[^1] = task;
    }
    public void Add (T[] tasks)
    {
        foreach (var task in tasks) Add(task);
    }

    public void Remove(T task)
    {
        int ind_task_to_remove = Array.FindIndex(_tasks, t => t.ToString() == task.ToString());
        if (ind_task_to_remove != -1)
        {
            T[] new_tasks = new T[_tasks.Length-1];
            for (int i=0; i<new_tasks.Length; i++)
            {
                if (i < ind_task_to_remove) new_tasks[i] = _tasks[i];
                else new_tasks[i] = _tasks[i+1];
            }
            _tasks = new_tasks;
        }
    }
    public void Clear()
    {
        _tasks = Array.Empty<T>();
        if (Manager != null && Directory.Exists(Manager.FolderPath))
            Directory.Delete(Manager.FolderPath, true);
    }

    public void SaveTasks()
    {
        if (Manager != null)
        {
            for (int i=0; i<_tasks.Length; i++)
            {
                Manager.ChangeFileName($"task{i}");
                Manager.Serialize(_tasks[i]);
            }
        }
    }
    public void LoadTasks()
    {
        if (Manager != null)
        {
            for (int i=0; i<_tasks.Length; i++)
            {
                Manager.ChangeFileName($"task{i}");
                _tasks[i] = Manager.Deserialize();
            }
        }
    }
    
    public void ChangeManager(PurpleFileManager<T> manager)
    {
        string parent_folder = string.IsNullOrEmpty(Manager.FolderPath) 
            ? Directory.GetCurrentDirectory() 
            : Manager.FolderPath;

        string folder_path = Path.Combine(parent_folder, manager.Name);
        Directory.CreateDirectory(folder_path);
        manager.SelectFolder(folder_path);
        Manager = manager;     
    }
}