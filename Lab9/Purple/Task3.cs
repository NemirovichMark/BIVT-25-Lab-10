using System.Text;

namespace Lab9.Purple;

public class Task3: Purple
{
    private string? _output;

    private (string, char)[] _codes;
    public string Output => _output ?? default; //при null default 

    public (string, char)[] Codes => _codes;
    
    public Task3(string input) : base(input)
    {
    }

    public override void Review()
    {
        string[] pairsSymbols = new string[Input.Length - 1]; //закидываем сами пары, -1 тк последняя буква без пары
        int[] frequencies = new int[Input.Length - 1]; //кол-во повторений

        int pairCount = 0; //разные уникальные пары
        
        for (int i = 0; i < Input.Length - 1; i++)
        {
            if (!char.IsLetter(Input[i]) || !char.IsLetter(Input[i + 1]))
                continue;
            string pair = Input.Substring(i, 2);
            int index = IndexOf(pairsSymbols, pair, pairCount);
            if (index >= 0) //если такой варик уже есть
            {
                frequencies[index]++;
            }
            
            else
            {
                pairsSymbols[pairCount] = pair;
                frequencies[pairCount] = 1;
                pairCount++;
            }
        }
        
        int[] topFiveFrequencies = Enumerable.Range(0, pairCount) 
            .OrderByDescending(i => frequencies[i])
            .ThenBy(i => Input.IndexOf(pairsSymbols[i])) //те, которые идут раньше
            .Take(5)
            .ToArray();

        char[] unusedSymbols = Enumerable.Range(32, 95)
            .Select(i => (char)i) //из инта в аски
            .Where(s => Input.IndexOf(s) == -1) //первые символы(не встреч.)
            .Take(5)
            .ToArray();
        
        _codes = new (string, char)[5]; 
        
        _output = new string(Input); 
        //новая ссылка
        
        for (int i = 0; i < topFiveFrequencies.Length; i++)
        {
            int index = topFiveFrequencies[i]; //индекс повторений чтобы потом найти пару
            _codes[i] = (pairsSymbols[index], unusedSymbols[i]); 
            _output = _output.Replace(pairsSymbols[index], unusedSymbols[i].ToString());
        }
    }

    private int IndexOf(string[] pairs, string pair, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (pair == pairs[i])
                return i;
        }
        return -1;
    }
    
    public override string ToString() => Output;
}