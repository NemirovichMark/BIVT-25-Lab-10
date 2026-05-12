namespace serialization_28._04;

class Program
{
    static void Main(string[] args)
    {
        var ser = new MySerializer();
        ser.Serialize();
        ser.Deserialize();

        var serX = new MyXMLSerializer();
        serX.Serialize();
        serX.Deserialize();
    }
}