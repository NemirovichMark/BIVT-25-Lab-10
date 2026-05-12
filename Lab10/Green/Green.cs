namespace Lab10.Green;

public class Green
{
    private GreenFileManager _manager;
    private Lab9.Green.Green[] _green;

    public GreenFileManager Manager => _manager;
    public Lab9.Green.Green[] Tasks => _green;

    public Green(Lab9.Green.Green[] green = null)
    {
        if (green == null)
        {
            _green = new Lab9.Green.Green[0];
        }
        else
        {
            _green = green.ToArray();
        }
    }

    public Green(GreenFileManager manager, Lab9.Green.Green[] green = null)
    {
        _manager = manager;
        if (green == null)
        {
            _green = new Lab9.Green.Green[0];
        }
        else
        {
            _green = green.ToArray();
        }
    }

    public Green(Lab9.Green.Green[] green, GreenFileManager manager)
    {
        _manager = manager;
        if (green == null)
        {
            _green = new Lab9.Green.Green[0];
        }
        else
        {
            _green = green.ToArray();
        }
    }

    public void Add(Lab9.Green.Green green)
    {
        Lab9.Green.Green[] NewGreen = new Lab9.Green.Green[_green.Length + 1];
        Array.Copy(_green, NewGreen, _green.Length);
        NewGreen[_green.Length] = green;
        _green = NewGreen;
    }
    
    public void Add(Lab9.Green.Green[] green)
    {
        Lab9.Green.Green[] NewGreen = new Lab9.Green.Green[_green.Length + green.Length];
        Array.Copy(_green, NewGreen, _green.Length);
        Array.Copy(green, 0, NewGreen, _green.Length, green.Length);
        _green = NewGreen;
    }

    public void Remove(Lab9.Green.Green green)
    {
        int index = Array.IndexOf(_green, green);
        Lab9.Green.Green[] NewGreen = new Lab9.Green.Green[_green.Length - 1];
        Array.Copy(_green, NewGreen, index);
        Array.Copy(_green, index + 1, NewGreen, index, NewGreen.Length - index);
        _green = NewGreen;
    }

    public void Clear()
    {
        _green = new Lab9.Green.Green[0];
        //if (_manager != null && Directory.Exists(_manager.FolderPath))
        {
            Directory.Delete(_manager.FolderPath, true); 
        }
    }

    public void SaveTasks()
    {
        for (int i = 0; i < _green.Length; i++)
        {
            _manager.ChangeFileName("Task_" + i);
            _manager.Serialize(_green[i]);
        }
    }

    public void LoadTasks()
    {
        for (int i = 0; i < _green.Length; i++)
        {
            _manager.ChangeFileName("Task_" + i);
            _green[i] = _manager.Deserialize<Lab9.Green.Green>();
        }
    }

    public void ChangeManager(GreenFileManager manager)
    {
        _manager = manager;
        Directory.CreateDirectory(_manager.Name);
        _manager.SelectFolder(_manager.Name);
    }
}