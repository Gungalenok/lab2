namespace lab2;

public class Cat(int fieldSize) : Player(fieldSize)
{
    public override void Collide(Player player)
    {
        if (player is Mouse)
            State = State.Winner;
    }
}
