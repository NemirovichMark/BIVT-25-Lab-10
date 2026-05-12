namespace Lab9.Blue
{
    public class Edit
    {
        public static void Add(ref string[] array, string text)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = text;
        }
        
        public static string Join(string[] array, string separator)
        {
            var result = "";
            bool first = true;
            foreach (var s in array)
            {
                if (string.IsNullOrEmpty(s)) continue;
                if (!first) result += separator;
                result += s;
                first = false;
            }
            return result;
        }

        public static string Letters(string text)
        {
            var res = "";
            foreach (var i in text) if (char.IsNumber(i)) return ""; else if (char.IsLetter(i)) res += char.ToLower(i);
            return res;
        }

        public static int Numbers(string text)
        {
            var res = 0;
            foreach (var i in text)
            {
                if (char.IsNumber(i)) { res *= 10; res += i - '0'; }
            }
            return res;
        }
    } 
    
    public abstract class Blue
    {
        protected string _input;
        public string Input => _input;

        protected Blue(string input)
        {
            _input = input;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }
    }
}
