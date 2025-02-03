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

public class Program
{
    public static void Main()
    {
        Player player = new Player("Default Name");
        player.UpdateName("New Player Name");

        Item item1 = new Item("Wood", 10, 5.0m);
        Item item2 = new Item("Iron", 5, 15.0m);

        Recipe recipe = new Recipe("Iron Sword", 1, 50.0m);
        recipe.AddRequiredItem(item1, 2);
        recipe.AddRequiredItem(item2, 1);

        Workshop workshop = new Workshop();
        workshop.Recipes.Add(recipe);

        workshop.ListAllRecipes();
        workshop.ShowRecipeDetails("Iron Sword");

        Console.WriteLine("Enter a number:");
        string userInput = Library.GetUserInput();
        if (Library.IsNumber(userInput))
        {
            Console.WriteLine("You entered a valid number.");
        }
        else
        {
            Console.WriteLine("Invalid number.");
        }
    }
}


/*
 * Craft System
 * Davion Horn
 * Application created in PROG 201 Programming I
 * With code demos from instructor
 * Spring 2025
 */


