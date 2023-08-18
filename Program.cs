using Microsoft.Win32.SafeHandles;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Rose",
        LightNeeds = 3,
        AskingPrice = 15.50M,
        City = "Somerset",
        ZIP = 42503,
        Sold = false
    },
    new Plant()
    {
        Species = "Tulip",
        LightNeeds = 4,
        AskingPrice = 12.50M,
        City = "Monticello",
        ZIP = 42633,
        Sold = false
    },
    new Plant()
    {
        Species = "Daisy",
        LightNeeds = 2,
        AskingPrice = 5.50M,
        City = "Crossville",
        ZIP = 38555,
        Sold = false
    },
    new Plant()
    {
        Species = "Blackberry Bush",
        LightNeeds = 5,
        AskingPrice = 20.50M,
        City = "Somerset",
        ZIP = 42503,
        Sold = false
    },
    new Plant()
    {
        Species = "Fescue",
        LightNeeds = 5,
        AskingPrice = 1.50M,
        City = "Jamestown",
        ZIP = 39568,
        Sold = false
    }
};

string greeting = "Welcome to ExtraVert! The Worlds best plant database.";

Console.WriteLine(greeting);

Random random = new Random();

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. Display all Plants
                        2. Post a Plant for Adoption
                        3. Adopt a Plant
                        4. Delist a plant
                        5. Pick a Random Plant
                        6. Plants by Sunlight Needs
                        7. Plant Statistics");
    choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        DisplayAvailablePlants(plants);
    }
    else if (choice == "2")
    {
        Console.WriteLine("Enter plant species: ");
        string species = Console.ReadLine();

        Console.WriteLine("Enter light needs: (1(shade)-5(full sun)) ");
        int lightNeeds = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter asking price: ($$.cc)");
        decimal askingPrice = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter city: ");
        string city = Console.ReadLine();

        Console.WriteLine("Enter zip code: ");
        int zipCode = int.Parse(Console.ReadLine());

        try
        {
            Console.WriteLine("Enter year of availability until: ");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter month of availability until: ");
            int month = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter day of availability until: ");
            int day = int.Parse(Console.ReadLine());

            DateTime availableUntil = new DateTime(year, month, day);

            // Create a new Plant object with default Sold value (false)
            Plant newPlant = new()
            {
                Species = species,
                LightNeeds = lightNeeds,
                AskingPrice = askingPrice,
                City = city,
                ZIP = zipCode,
                Sold = false,
                AvailableUntil = availableUntil
            };

            // Add the new Plant object to the plants list
            plants.Add(newPlant);

            Console.WriteLine("Plant posted for adoption.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Invalid date. Please enter a valid date.");
        }
    }
    else if (choice == "3")
    {
      
        DisplayAvailablePlants(plants);

        Console.Write("Enter the index of the plant you want to adopt: ");
        int plantIndex = int.Parse(Console.ReadLine()) - 1; // Subtract 1 to get the correct index

        if (plantIndex >= 0 && plantIndex < plants.Count && !plants[plantIndex].Sold)
        {
            plants[plantIndex].Sold = true;
            Console.WriteLine($"Congratulations! You adopted the {plants[plantIndex].Species} plant.");
        }
        else
        {
            Console.WriteLine("Invalid selection or plant is not available for adoption.");
        }
    }
    else if (choice == "4")
    {
        Console.WriteLine("Current List of Plants:");

        // Display all plants
        for (int i = 0; i < plants.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : "is available")} for {plants[i].AskingPrice}");
        }

        Console.Write("Enter the index of the plant you want to delist: ");
        int plantIndex = int.Parse(Console.ReadLine()) - 1; // Subtract 1 to get the correct index

        if (plantIndex >= 0 && plantIndex < plants.Count)
        {
            plants.RemoveAt(plantIndex); // Remove the selected plant from the list
            Console.WriteLine("Plant delisted.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }
    else if (choice == "5")
    {
        int randomIndex;

        // Find a random index of an available plant
        while (true)
        {
            randomIndex = random.Next(plants.Count); // Generate a random index
            if (!plants[randomIndex].Sold)
            {
                break; // Exit the loop if an available plant is found
            }
        }

        Console.WriteLine("Randomly Selected Plant:");
        Console.WriteLine($"Species: {plants[randomIndex].Species}");
        Console.WriteLine($"Location: {plants[randomIndex].City}");
        Console.WriteLine($"Light Needs: {plants[randomIndex].LightNeeds}");
        Console.WriteLine($"Price: {plants[randomIndex].AskingPrice:C}");
    }
    else if (choice == "6")
    {
        SearchPlantsByLightNeeds(plants);
    }
    else if (choice == "7")
    {
        ViewAppStatistics(plants);
    }
}


Console.WriteLine("Please enter a product number: ");

static void SearchPlantsByLightNeeds(List<Plant> plants)
{
    Console.Write("Enter a maximum light needs number (1 to 5): ");
    int maxLightNeeds = int.Parse(Console.ReadLine());

    List<Plant> matchingPlants = new List<Plant>();

    foreach (var plant in plants)
    {
        if (plant.LightNeeds <= maxLightNeeds)
        {
            matchingPlants.Add(plant);
        }
    }

    Console.WriteLine("Plants with Maximum Light Needs of " + maxLightNeeds + " or Lower:");
    foreach (var plant in matchingPlants)
    {
        Console.WriteLine($"Species: {plant.Species}");
        Console.WriteLine($"Location: {plant.City}");
        Console.WriteLine($"Light Needs: {plant.LightNeeds}");
        Console.WriteLine($"Price: {plant.AskingPrice:C}");
        Console.WriteLine();
    }
}

static void DisplayAvailablePlants(List<Plant> plants)
{
    Console.WriteLine("Available Plants for Adoption:");

    foreach (var plant in plants)
    {
        if (!plant.Sold && plant.AvailableUntil >= DateTime.Now)
        {
            Console.WriteLine(PlantDetails(plant));
        }
    }
}

static void ViewAppStatistics(List<Plant> plants)
{
    Console.WriteLine("App Statistics:");

    // Lowest price plant name
    decimal lowestPrice = decimal.MaxValue;
    string lowestPricePlantName = "";
    foreach (var plant in plants)
    {
        if (plant.AskingPrice < lowestPrice)
        {
            lowestPrice = plant.AskingPrice;
            lowestPricePlantName = plant.Species;
        }
    }
    Console.WriteLine($"Lowest price plant name: {lowestPricePlantName}");

    // Number of Plants Available (not sold, and still available)
    int availablePlantsCount = 0;
    foreach (var plant in plants)
    {
        if (!plant.Sold && plant.AvailableUntil >= DateTime.Now)
        {
            availablePlantsCount++;
        }
    }
    Console.WriteLine($"Number of Plants Available: {availablePlantsCount}");

    // Name of plant with highest light needs
    int highestLightNeeds = 0;
    string highestLightNeedsPlantName = "";
    foreach (var plant in plants)
    {
        if (plant.LightNeeds > highestLightNeeds)
        {
            highestLightNeeds = plant.LightNeeds;
            highestLightNeedsPlantName = plant.Species;
        }
    }
    Console.WriteLine($"Name of plant with highest light needs: {highestLightNeedsPlantName}");

    // Average light needs
    double totalLightNeeds = 0;
    int totalPlants = 0;
    foreach (var plant in plants)
    {
        totalLightNeeds += plant.LightNeeds;
        totalPlants++;
    }
    double averageLightNeeds = totalLightNeeds / totalPlants;
    Console.WriteLine($"Average light needs: {averageLightNeeds:F}");

    // Percentage of plants adopted
    int adoptedPlantsCount = 0;
    foreach (var plant in plants)
    {
        if (plant.Sold)
        {
            adoptedPlantsCount++;
        }
    }
    double percentageAdopted = (double)adoptedPlantsCount / plants.Count * 100;
    Console.WriteLine($"Percentage of plants adopted: {percentageAdopted:F}%");
}

static string PlantDetails(Plant plant)
{
    string plantString =  $"Species: {plant.Species}, Light Needs: {plant.LightNeeds}, " +
                             $"Asking Price: {plant.AskingPrice:C}, City: {plant.City}, ZIP: {plant.ZIP}";

    return plantString;
}