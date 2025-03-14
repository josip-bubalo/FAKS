public abstract class Item : IUsable
{
    public string Naziv { get; set; }

    public Item(string naziv)
    {
        Naziv = naziv;
    }

    public abstract void Use(Character character);
}
