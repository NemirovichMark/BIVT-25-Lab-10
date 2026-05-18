namespace Lab10
{
    public interface IWhiteSerializer
    {
        void Serialize(Lab9.White.White obj);//сериализация- превращение объекта в формат который можно передать в строку в jsn txt xml
        Lab9.White.White Deserialize();//обратный процесс 
    }
}
