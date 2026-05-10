namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    public PurpleFileManager<T> Manager { get; private set; }
    public T[] Tasks { get; private set; }

    public Purple()
    {
        Tasks = Array.Empty<T>();
    }
    public Purple (T[] tasks)
    {
        Tasks = tasks ?? Array.Empty<T>();
    }
    public Purple(PurpleFileManager<T> manager, T[] tasks = null)
    {
        Manager = manager;
        Tasks = tasks ?? Array.Empty<T>();
    }
    public Purple(T[] tasks, PurpleFileManager<T> manager)
    {
        Tasks = tasks ?? Array.Empty<T>();
        Manager = manager;
    }

    public void Add(T item)
    {
        if (Tasks == null || item == null) return;
        T[] newArray = new T[Tasks.Length + 1];
        for (int i = 0; i < Tasks.Length; i++)
            newArray[i] = Tasks[i];
        newArray[^1] = item;
        Tasks = newArray;
    }
    public void Add(T[] items)
    {
        if (items == null) return;
        foreach(T item in items)
            Add(item);
    }

    public void Remove(T item)
    {
        if (Tasks == null || Tasks.Length == 0) return;
        int idx = Array.FindIndex(Tasks, t => t == item);
        if (idx < 0) return;
        T[] newArray = new T[Tasks.Length-1];
        for (int i = 0, j = 0; i < Tasks.Length; i++)
        {
            if (i == idx) continue;
            newArray[j++] = Tasks[i];
        }
        Tasks = newArray;
    }

    public void Clear()
    {
        Tasks = Array.Empty<T>();
        if (Manager != null)
        {
            string folder = Manager.FolderPath;
            if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
                Directory.Delete(folder, true);
        }
    }

    public void SaveTasks()
    {
        if (Manager == null) return;
        for (int i = 0; i < Tasks.Length; i++)
        {
            if (Tasks[i] == null) continue;
            string fileName = $"{Tasks[i].GetType().Name}_{i}";
            Manager.ChangeFileName(fileName);
            Manager.CreateFile();
            Manager.Serialize(Tasks[i]);
        }
    }

    public void LoadTasks()
    {
        if (Manager == null) return;
        Tasks = Array.Empty<T>();
        string folder = Manager.FolderPath;
        if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder)) return;
        var files = Directory.GetFiles(folder);
        foreach (var file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            Manager.ChangeFileName(fileName);
            T loaded = Manager.Deserialize();
            if (loaded != null)
            {
                Add(loaded);
            }
        }
    }

    public void ChangeManager(PurpleFileManager<T> newManager)
    {
        if (newManager == null) return;
        Manager = newManager;
        string folderName = newManager.Name;
        if (!string.IsNullOrEmpty(folderName) && !Directory.Exists(folderName))
            Directory.CreateDirectory(folderName);
        Manager.SelectFolder(folderName);
    }
}