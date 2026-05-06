namespace Lab9.Green
{
    abstract public class Green : Object
    {
        private string _input;
        public string Input => _input;

        protected Green(string input) {
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