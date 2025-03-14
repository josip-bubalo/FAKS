public class Lijek : Item
{
    public int KolicinaLijecenja { get; set; }

    public Lijek(string naziv, int kolicinaLijecenja)
        : base(naziv)
    {
        KolicinaLijecenja = kolicinaLijecenja;
    }

    public override void Use(Character character)
    {
        character.LijeciSe(KolicinaLijecenja);
        Console.WriteLine($"Korišten {Naziv} za liječenje {character.Ime}.");
    }
}
