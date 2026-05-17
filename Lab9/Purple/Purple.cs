namespace Lab9.Purple
{
    public abstract class Purple
    {
        //fields
        protected string _text;

        //parameters
        public string Input => _text;
        public string Output {get; protected set;}

        //methods
        public abstract void Review();
        public virtual void ChangeText(string text) {_text = text; Review();}
        public override string ToString() {return Output;}

        //constructor
        protected Purple (string text) {_text = text;}
    }
}