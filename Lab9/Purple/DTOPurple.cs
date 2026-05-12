using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class DTOPurple
    {
        public string TypeObj { get; set; }
        public string Input { get; set; }
        public DTOCode[] Codes { get; set; }

        public DTOPurple()
        {

        }

        public DTOPurple(Lab9.Purple.Purple purple)
        {
            TypeObj = purple.GetType().Name.ToString() ;
            Input = purple.Input;

            if (purple is Lab9.Purple.Task4 task4)
                Codes = task4.Codes.Select(cortege => new DTOCode(cortege.Item1, cortege.Item2)).ToArray();
        }
       
        public Lab9.Purple.Purple GetResultObj()
        {
            switch (TypeObj)
            {
                case "Task1":
                    var obj1 = new Lab9.Purple.Task1(Input);
                    return obj1;
                case "Task2":
                    var obj2 = new Lab9.Purple.Task2(Input);
                    return obj2;
                case "Task3":
                    var obj3 = new Lab9.Purple.Task3(Input);
                    return obj3;
                case "Task4":
                    (string, char)[] codes = Codes.Select(cortege => (cortege.Letters, cortege.Code)).ToArray();
                    var obj4 = new Lab9.Purple.Task4(Input, codes);
                    return obj4;
            }
            return null;
            
        }
        
    }

    public class DTOCode
    {
        public string Letters { get; set; }
        public char Code { get; set; }
        public (string, char) GetCode => (Letters, Code);

        public DTOCode() { }
        public DTOCode(string letters, char code)
        {
            Letters = letters;
            Code = code;
        }
    }
}
