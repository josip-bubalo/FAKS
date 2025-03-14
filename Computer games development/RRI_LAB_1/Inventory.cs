using System.Collections.Generic;

public class Inventory
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine($"Dodan predmet: {item.Naziv}");
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Console.WriteLine($"Uklonjen predmet: {item.Naziv}");
        }
        else
        {
            Console.WriteLine($"Predmet {item.Naziv} nije pronađen u inventaru.");
        }
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }

    public void PrikaziInventar()
    {
        Console.WriteLine("Inventar:");
        foreach (var item in items)
        {
            Console.WriteLine(item.Naziv);
        }
    }
}
