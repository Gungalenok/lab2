namespace lab2;

public class Mouse(int fieldSize) : Player(fieldSize)
{
    public override void Collide(Player player)
    {
        if (player is Cat)
            State = State.Looser;
    }
}
