using System.Text.Json.Serialization;

namespace Lab9.Blue
{
    public class String
    {
        public static void Add<T>(ref T[] arr, T elem)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = elem;
        }
        public static string[] Split(string t)
        {
            var ans = new string[] { };
            var str = "";
            foreach(var  i in t)
            {
                if(i == ' ')
                {
                    Add(ref ans, str);
                    str = "";
                }
                else
                {
                    str += i;
                }
            }
            Add(ref ans, str);
            return ans;
        }
        public static string[] SplitAll(string t)
        {
            var ans = new string[] { };
            var str = "";
            foreach (var i in t)
            {
                if (i == ' ' || char.IsPunctuation(i))
                {
                    Add(ref ans, str);
                    str = "";
                }
                else
                {
                    str += i;
                }
            }
            Add(ref ans, str);
            return ans;
        }
        public static string Join(string[] strs, string sep, bool flag = false)
        {
            var ans = "";
            for (int i = 0; i < strs.Length; i++)
            {
                if (string.IsNullOrEmpty(strs[i]) && flag) continue;
                ans += strs[i];
                if (i != strs.Length - 1) ans += sep;
            }
            return ans;
        }
        public static string Join(string[] strs, char sep, bool flag = false)
        {
            string t = "";
            t += sep;
            return Join(strs, t, flag);
        }
        public static bool Find(string where, string what)
        {
            return where.Contains(what);
        }
        public static bool Find(string where, char what)
        {
            return where.Contains(what);
        }
        public static string OnlyPunct(string s)
        {
            var ans = "";
            foreach(var i in s)
                if (Find(".,:;'!\"", i)) ans += i;
            return ans;
        }
        public static string OnlyLetters(string s)
        {
            var ans = "";
            foreach (var i in s) if (char.IsNumber(i)) return ""; else if (char.IsLetter(char.ToLower(i))) ans += char.ToLower(i);
            return ans;
        }
        public static int OnlyNumbers(string s)
        {
            var ans = 0;
            foreach (var i in s) if (char.IsNumber(i)) { ans *= 10; ans += i - '0'; }
            return ans;
        }
    }
    [JsonDerivedType(typeof(Task1), typeDiscriminator: "task1")]
    [JsonDerivedType(typeof(Task2), typeDiscriminator: "task2")]
    [JsonDerivedType(typeof(Task3), typeDiscriminator: "task3")]
    [JsonDerivedType(typeof(Task4), typeDiscriminator: "task4")]
    public abstract class Blue
    {
        protected string _input;

        public string Input => _input;

        protected Blue(string Input) => _input = Input;

        public abstract void Review();
        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }
    }
}