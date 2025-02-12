using System;
using System.Collections.Generic;

public class Player
{
    public string Name { get; set; }
    public List<Item> Inventory { get; set; }

    public Player(string name)
    {
        Name = name;
        Inventory = new List<Item>();
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void AddItemToInventory(Item item)
    {
        Inventory.Add(item);
    }

    public void RemoveItemFromInventory(string itemName)
    {
        int itemIndex = CollectionUtilities.SearchCollectionByNameIndex(Inventory, itemName);
        if (itemIndex != -1)
        {
            CollectionUtilities.RemoveFromCollectionByIndexNumber(Inventory, itemIndex);
        }
        else
        {
            Console.WriteLine("Item not found in inventory.");
        }
    }

    public void ListInventory()
    {
        Console.WriteLine($"Inventory of {Name}:");
        foreach (var item in Inventory)
        {
            Console.WriteLine($"Item: {item.Name}, Amount: {item.Amount}, Value: {item.Value}");
        }
    }
}

public class Item
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public decimal Value { get; set; }

    public Item(string name, int amount, decimal value)
    {
        Name = name;
        Amount = amount;
        Value = value;
    }
}

public class Recipe
{
    public string Name { get; set; }
    public int OutputAmount { get; set; }
    public decimal Value { get; set; }
    public Dictionary<Item, int> RequiredItems { get; set; }

    public Recipe(string name, int outputAmount, decimal value)
    {
        Name = name;
        OutputAmount = outputAmount;
        Value = value;
        RequiredItems = new Dictionary<Item, int>();
    }

    public void AddRequiredItem(Item item, int amount)
    {
        RequiredItems[item] = amount;
    }
}

public class Workshop
{
    public List<Recipe> Recipes { get; set; }

    public Workshop()
    {
        Recipes = new List<Recipe>();
    }

    public void ListAllRecipes()
    {
        foreach (var recipe in Recipes)
        {
            Console.WriteLine($"Recipe: {recipe.Name}, Output: {recipe.OutputAmount}, Value: {recipe.Value}");
        }
    }

    public void ShowRecipeDetails(string recipeName)
    {
        var recipe = Recipes.Find(r => r.Name == recipeName);
        if (recipe != null)
        {
            Console.WriteLine($"Recipe: {recipe.Name}");
            Console.WriteLine($"Output Amount: {recipe.OutputAmount}");
            Console.WriteLine($"Value: {recipe.Value}");
            Console.WriteLine("Required Items:");
            foreach (var item in recipe.RequiredItems)
            {
                Console.WriteLine($"Item: {item.Key.Name}, Amount: {item.Value}");
            }
        }
        else
        {
            Console.WriteLine("Recipe not found.");
        }
    }
}

public static class Library
{
    public static string GetUserInput()
    {
        return Console.ReadLine();
    }

    public static bool IsNumber(string input)
    {
        return int.TryParse(input, out _);
    }
}

public class CollectionUtilities
{
   
    public static bool SearchCollectionByName(List<Item> list, string itemName)
    {
        foreach (var item in list)
        {
            if (item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    
    public static int SearchCollectionByNameIndex(List<Item> list, string itemName)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }
        return -1;
    }

    
    public static void AddToCollectionByName(List<Item> list, Item item)
    {
        list.Add(item);
    }

    
    public static void RemoveFromCollectionByIndexNumber(List<Item> list, int itemIndexNumber)
    {
        if (itemIndexNumber >= 0 && itemIndexNumber < list.Count)
        {
            list.RemoveAt(itemIndexNumber);
        }
        else
        {
            Console.WriteLine("Invalid index number.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.Title = "Crafting System By Davion Horn";

        Console.Write("Enter your player name: ");
        string playerName = Library.GetUserInput();

        Player player = new Player(playerName);

        Item item1 = new Item("Wood", 10, 5.0m);
        Item item2 = new Item("Iron", 5, 15.0m);

        
        player.AddItemToInventory(item1);
        player.AddItemToInventory(item2);

        
        player.ListInventory();

        Recipe recipe = new Recipe("Iron Sword", 1, 50.0m);
        recipe.AddRequiredItem(item1, 2);
        recipe.AddRequiredItem(item2, 1);

        Workshop workshop = new Workshop();
        workshop.Recipes.Add(recipe);

        workshop.ListAllRecipes();
        workshop.ShowRecipeDetails("Iron Sword");

        Console.WriteLine("Enter a number:");
        string userInput2 = Library.GetUserInput();
        if (Library.IsNumber(userInput2))
        {
            Console.WriteLine("You entered a valid number.");
        }
        else
        {
            Console.WriteLine("Invalid number.");
        }

        
        List<Item> inventory = new List<Item>
        {
            new Item("Loaf of Bread", 1, 2.5m),
            new Item("Milk", 1, 1.5m)
        };

        
        int itemIndex = CollectionUtilities.SearchCollectionByNameIndex(inventory, "Loaf of Bread");
        CollectionUtilities.RemoveFromCollectionByIndexNumber(inventory, itemIndex);
    }
}


/*
 * Craft System
 * Davion Horn
 * Application created in PROG 201 Programming
 * With code demos from instructor
 * Spring 2025
 */


