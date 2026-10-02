namespace lab2;

public class Game(string inFileName, string outFileName)
{
	public void Start()
	{
		string[] lines = File.ReadAllLines(inFileName);
		int fieldSize = int.Parse(lines[0]);
		Mouse mouse = new Mouse(fieldSize);
		Cat cat = new Cat(fieldSize);

		using StreamWriter writer = new StreamWriter(outFileName);
		writer.WriteLine("Cat and Mouse\n\nCat Mouse  Distance\n-------------------");

		foreach (string line in lines.Skip(1))
		{
			string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 0) continue;

			if (parts[0] == "P")
			{
				string c = cat.State == State.NotInGame ? "??" : cat.Location.ToString();
				string m = mouse.State == State.NotInGame ? "??" : mouse.Location.ToString();
				if (cat.State == State.NotInGame || mouse.State == State.NotInGame)
					writer.WriteLine($"{c,3} {m,5}");
				else
					writer.WriteLine($"{c,3} {m,5} {cat.DistanceTo(mouse),9}");
				continue;
			}

			Player player = parts[0] == "M" ? mouse : cat;
			player.Move(int.Parse(parts[1]));
			CheckCollision(cat, mouse);
			if (cat.State == State.Winner) break;
		}

		writer.WriteLine("-------------------\n\n\nDistance traveled:   Mouse    Cat");
		writer.WriteLine($"{mouse.DistanceTraveled,26}{cat.DistanceTraveled,7}\n");
		writer.WriteLine(mouse.State == State.Looser
			? $"Mouse caught at: {mouse.Location,2}"
			: "Mouse evaded Cat");
	}

	private static void CheckCollision(Player cat, Player mouse)
	{
		if (cat.State == State.Playing && mouse.State == State.Playing && cat.Location == mouse.Location)
		{
			cat.Collide(mouse);
			mouse.Collide(cat);
		}
	}
}
