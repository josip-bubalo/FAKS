public class Ratnik : Character
{
    public int Snaga { get; set; }

    public Ratnik(string ime, int health, int level, int snaga)
        : base(ime, health, level)
    {
        Snaga = snaga;
    }

    public void Napadi()
    {
        Console.WriteLine($"{Ime} napada sa snagom {Snaga}!");
    }
}

public class Carobnjak : Character
{
    public int Mana { get; set; }

    public Carobnjak(string ime, int health, int level, int mana)
        : base(ime, health, level)
    {
        Mana = mana;
    }

    public void BacajCini()
    {
        Console.WriteLine($"{Ime} baca čini!");
    }
}
