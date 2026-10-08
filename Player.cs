namespace lab2;

public abstract class Player(int fieldSize)
{
    public int Location { get; private set; } = -1;
    public State State { get; protected set; } = State.NotInGame;
    public int DistanceTraveled { get; private set; }

    public void Move(int steps)
    {
        if (State == State.NotInGame)
        {
			Location = Normalize(steps);
            State = State.Playing;
            return;
        }

        if (State != State.Playing) return;
        Location = Normalize(Location + steps);
		DistanceTraveled += Math.Abs(steps);
    }

	private int Normalize(int position) => ((position - 1) % fieldSize + fieldSize) % fieldSize + 1;

    public int DistanceTo(Player player)
    {
		return Math.Abs(player.Location - Location);
    }

    public abstract void Collide(Player player);
}
