public class Character
{
    public string Ime { get; set; }
    public int Health { get; set; }
    public int Level { get; set; }
    public Inventory Inventar {  get; set; }

    public Character(string ime, int health, int level)
    {
        Ime = ime;
        Health = health;
        Level = level;
        Inventar = new Inventory();
    }

    public void PrimajStetu(int steta)
    {
        Health -= steta;
        if (Health < 0) Health = 0;
    }

    public void LijeciSe(int kolicina)
    {
        Health += kolicina;
        if (Health > 100) Health = 100;
    }

    public void PovecajLevel()
    {
        Level++;
    }

}
