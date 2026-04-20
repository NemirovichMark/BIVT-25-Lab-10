using System.Text;

namespace Lab9.Purple;

public class Task3 : Purple
{
    public Task3(string input) : base(input) {}
    public string Output {get;private set;}
    public override string ToString() => Output;
    public override void Review()
    {
        string encrypted = Input;
        foreach (var code in Codes)
        {
            encrypted = encrypted.Replace(code.Item1, code.Item2.ToString());
        }
        Output = encrypted;
    }
    public (string,char)[] Codes
    {
        get
        {
            int index=0;
            var sortedPairs = GetSortedPairs();
            (string,char)[] codes = new (string,char)[0];
            for (int i=32; i<=126 && index<sortedPairs.Length; i++)
            {
                if (!Input.Contains((char)i))
                {
                    Array.Resize(ref codes, codes.Length+1);
                    codes[^1] = (sortedPairs[index],(char)i);
                    index++;
                }
            }

            return codes;   
        }
    }
    private string[] GetSortedPairs()
    {
        var pairs = GetUnsortedPairs();
        string[] sortedPairs = pairs.OrderByDescending(p => p.Count)
                                .ThenBy(p => p.FirstIndex)
                                .Take(5)
                                .Select(p => p.Symbols)
                                .ToArray();

        return sortedPairs;
    }

    private Pair[] GetUnsortedPairs()
    {
        Pair[] pairs = new Pair[0];
        for (int i=1; i<Input.Length; i++)
        {
            if (char.IsLetter(Input[i-1]) && char.IsLetter(Input[i]))
            {
                string term = Input[i-1].ToString() + Input[i].ToString();
                var pair = pairs.FirstOrDefault(p => p.Symbols == term);
                if (pair != null) pair.RaiseCount();
                else
                {
                    Pair termP = new Pair(term,i-1);
                    Array.Resize(ref pairs, pairs.Length+1);
                    pairs[^1] = termP;
                }
            }
        }
        return pairs;
    }

    private class Pair
    {
        public string Symbols {get; private set;}
        public int Count {get; private set;}
        public int FirstIndex {get; private set;}
        public Pair (string symbols, int firstIndex)
        {
            Symbols = symbols;
            Count = 1;
            FirstIndex = firstIndex;
        }
        public void RaiseCount() => Count++;
    }
}
