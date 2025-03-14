class Program
{
    static void Main()
    {
        Ratnik ratnik = new Ratnik("Conan", 100, 1, 10);
        Carobnjak carobnjak = new Carobnjak("Gandalf", 80, 1, 50);

        Lijek lijek = new Lijek("Mali lijek", 20);

        ratnik.PrimajStetu(30);
        Console.WriteLine($"Zdravlje nakon štete: {ratnik.Health}");

        lijek.Use(ratnik);
        Console.WriteLine($"Zdravlje nakon liječenja: {ratnik.Health}");

        carobnjak.PrimajStetu(20);
        Console.WriteLine($"Zdravlje nakon štete: {carobnjak.Health}");

        lijek.Use(carobnjak);
        Console.WriteLine($"Zdravlje nakon liječenja: {carobnjak.Health}");

        Lijek lijek1 = new Lijek("Mali lijek", 20);
        Lijek lijek2 = new Lijek("Veliki lijek", 50);
        Lijek lijek3 = new Lijek("Specijalni lijek", 100);

        ratnik.Inventar.AddItem(lijek1);
        ratnik.Inventar.AddItem(lijek2);
        ratnik.Inventar.AddItem(lijek3);

        ratnik.Inventar.PrikaziInventar();

        ratnik.Inventar.RemoveItem(lijek2);

        Console.WriteLine("Inventar nakon uklanjanja:");
        ratnik.Inventar.PrikaziInventar();

        if (ratnik.Inventar.ContainsItem(lijek1))
        {
            Console.WriteLine("Predmet 'Mali lijek' postoji u inventaru.");
        }
        else
        {
            Console.WriteLine("Predmet 'Mali lijek' ne postoji u inventaru.");
        }

        if (ratnik.Inventar.ContainsItem(lijek2))
        {
            Console.WriteLine("Predmet 'Veliki lijek' postoji u inventaru.");
        }
        else
        {
            Console.WriteLine("Predmet 'Veliki lijek' ne postoji u inventaru.");
        }
    }
}