namespace WhishList.WhishList;

public class Whish
{
    private readonly string _leksak;
    private readonly string _namn;

    public Whish(string namn, string leksak)
    {
        _namn = namn;
        _leksak = leksak;
    }
}