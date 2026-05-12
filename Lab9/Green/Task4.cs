namespace Lab9.Green;

public class Task4 : Green
{
    private string[] _output;
    public string[] Output => _output;


    public Task4(string input) : base(input)
    {
        _output = new string[Names().Length];
    }

    public string[] Names()
    {
        string[] names = Input.Split(',');
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = names[i].Trim();
        }
        return names;
    }

    //найти каждое имя и записать отдельно в массив стрингов 
    //потом по алфавиту который мы создали приравнивать каждую первую букву и если она равна то записываем в новый массив
    //дальше приравнивае вторую букву и тд
    public override void Review()
    {
        string alfavit = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz";

        int n = Names().Length;
        string[] names = Names();
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                int h = 0;
                int min = int.Min(names[j].Length, names[j + 1].Length);
                while (h < min && alfavit.IndexOf(char.ToLower(names[j][h])) ==
                       alfavit.IndexOf(char.ToLower(names[j + 1][h])))
                {
                    h++;
                }

                if (h == min)
                {
                    if (names[j].Length == names[j + 1].Length) continue;
                    if (names[j].Length < names[j + 1].Length) continue;
                    if (names[j].Length > names[j + 1].Length)
                    {
                        (names[j], names[j + 1]) = (names[j + 1], names[j]);
                        continue;
                    }
                }

                if (alfavit.IndexOf(char.ToLower(names[j][h])) > alfavit.IndexOf(char.ToLower(names[j + 1][h])))
                {
                    (names[j], names[j + 1]) = (names[j + 1], names[j]);
                }
            }
        }

        _output = names;

    }

    public override string ToString()
    {
        string res = "";
        for (int i = 0; i < _output.Length; i++)
        {
            res += _output[i] + '\n' ;

        }
        
        return res.TrimEnd();
    }
}