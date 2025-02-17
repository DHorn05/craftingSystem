using System;
using System.Collections.Generic;
using System.Linq;

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

    public void RemoveItemFromInventory(string itemName, int amount)
    {
        int itemIndex = CollectionUtilities.SearchCollectionByNameIndex(Inventory, itemName);
        if (itemIndex != -1)
        {
            var item = Inventory[itemIndex];
            if (item.Amount >= amount)
            {
                item.Amount -= amount;
                if (item.Amount == 0)
                {
                    CollectionUtilities.RemoveFromCollectionByIndexNumber(Inventory, itemIndex);
                }
            }
            else
            {
                Console.WriteLine("Not enough of the item in inventory to remove.");
            }
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

public static class CollectionUtilities
{
    public static bool SearchCollectionByName(List<Item> list, string itemName)
    {
        return list.Any(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
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

public static class InventoryUtilities
{
    public static bool CheckInventoryForItem(List<Item> inventory, string itemName)
    {
        return inventory.Any(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    public static int GetItemAmount(List<Item> inventory, string itemName)
    {
        var item = inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        return item?.Amount ?? 0;
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

        // Sample items to player inventory
        Item wood = new Item("Wood", 200, 5.0m);
        Item iron = new Item("Iron", 100, 15.0m);
        Item leather = new Item("Leather", 150, 10.0m);
        Item gemstone = new Item("Gemstone", 50, 50.0m);
        Item cloth = new Item("Cloth", 250, 2.0m);

        player.AddItemToInventory(wood);
        player.AddItemToInventory(iron);
        player.AddItemToInventory(leather);
        player.AddItemToInventory(gemstone);
        player.AddItemToInventory(cloth);

        // Display player inventory
        player.ListInventory();

        // Create recipes
        Recipe ironSword = new Recipe("Iron Sword", 1, 50.0m);
        ironSword.AddRequiredItem(wood, 2);
        ironSword.AddRequiredItem(iron, 1);

        Recipe leatherArmor = new Recipe("Leather Armor", 1, 100.0m);
        leatherArmor.AddRequiredItem(leather, 5);
        leatherArmor.AddRequiredItem(cloth, 3);

        Recipe gemstoneAmulet = new Recipe("Gemstone Amulet", 1, 200.0m);
        gemstoneAmulet.AddRequiredItem(gemstone, 2);
        gemstoneAmulet.AddRequiredItem(iron, 1);

        Recipe woodenShield = new Recipe("Wooden Shield", 1, 30.0m);
        woodenShield.AddRequiredItem(wood, 4);
        woodenShield.AddRequiredItem(leather, 2);

        Workshop workshop = new Workshop();
        workshop.Recipes.Add(ironSword);
        workshop.Recipes.Add(leatherArmor);
        workshop.Recipes.Add(gemstoneAmulet);
        workshop.Recipes.Add(woodenShield);

        // Display all recipes
        workshop.ListAllRecipes();

        // Crafting menu
        while (true)
        {
            Console.WriteLine("\nCrafting Menu:");
            Console.WriteLine("1. Iron Sword");
            Console.WriteLine("2. Leather Armor");
            Console.WriteLine("3. Gemstone Amulet");
            Console.WriteLine("4. Wooden Shield");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string choice = Library.GetUserInput();

            if (choice == "1")
            {
                CraftItem(player, ironSword);
            }
            else if (choice == "2")
            {
                CraftItem(player, leatherArmor);
            }
            else if (choice == "3")
            {
                CraftItem(player, gemstoneAmulet);
            }
            else if (choice == "4")
            {
                CraftItem(player, woodenShield);
            }
            else if (choice == "5")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }

        // Display updated player inventory
        Console.WriteLine("\nUpdated Inventory:");
        player.ListInventory();
    }

    public static void CraftItem(Player player, Recipe recipe)
    {
        bool canCraft = true;
        Dictionary<string, int> missingMaterials = new Dictionary<string, int>();

        foreach (var item in recipe.RequiredItems)
        {
            int playerItemAmount = InventoryUtilities.GetItemAmount(player.Inventory, item.Key.Name);
            if (playerItemAmount < item.Value)
            {
                canCraft = false;
                missingMaterials[item.Key.Name] = item.Value - playerItemAmount;
            }
        }

        if (canCraft)
        {
            foreach (var item in recipe.RequiredItems)
            {
                player.RemoveItemFromInventory(item.Key.Name, item.Value);
            }
            Console.WriteLine($"Crafted {recipe.Name}!");
        }
        else
        {
            Console.WriteLine($"Not enough materials to craft {recipe.Name}. Missing materials:");
            foreach (var material in missingMaterials)
            {
                Console.WriteLine($"{material.Key}: {material.Value}");
            }
        }

    }
}

/*
 * Craft System
 * Davion Horn
 * Application created in PROG 201 Programming
 * With code demos from instructor
 * Spring 2025
 */


