namespace green
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }


    }
    public class Green
    {
        protected GreenFileManager _manager;
        protected Lab9.Green.Green[] _tasks;


        public GreenFileManager Manager => _manager;
        public Lab9.Green.Green[] Tasks => _tasks.Clone() as Lab9.Green.Green[];


        public Green(Lab9.Green.Green[] tasks)
        {
            if (tasks != null)
            {
                _tasks = (Lab9.Green.Green[])tasks.Clone();
            }
            else
            {
                _tasks = new Lab9.Green.Green[0];
            }

        }
        public Green(GreenFileManager manager, Lab9.Green.Green[] tasks = new Lab9.Green.Green[0])
        {
            _manager = manager;
            if (tasks != null)
            {
                _tasks = (Lab9.Green.Green[])tasks.Clone();
            }
            else
            {
                _tasks = new Lab9.Green.Green[0];
            }

        }
        public Green(Lab9.Green.Green[] tasks, GreenFileManager manager)
        {
            _manager = manager;
            if(tasks != null)
            {
                _tasks = (Lab9.Green.Green[])tasks.Clone();
            }
            else
            {
                _tasks = new Lab9.Green.Green[0];
            }
        }
        


    }
}