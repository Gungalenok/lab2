using lab2;

for (int i = 1; i <= 3; i++)
{
    Game game = new Game($"{i}.ChaseData.txt", $"{i}.PursuitLog.txt");
    game.Start();
}