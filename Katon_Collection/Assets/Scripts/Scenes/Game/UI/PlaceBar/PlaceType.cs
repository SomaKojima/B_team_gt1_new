
//すべての場所のデータ
public enum Type
{
    none = -1,
    market,         //市場
    fountain,       //噴水

    forest,         //森
    cave,           //洞窟
    factory,        //工場
    farm,           //農場
    cotton,         //綿

    Max,
}

public class PlaceType
{
    static public bool IsCollectPlace(Type _type)
    {
        if (_type == Type.market || _type == Type.fountain)
        {
            return false;
        }
        return true;
    }
}