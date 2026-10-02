using Xunit;

namespace lab2.Tests;

public class GameTests
{
    [Fact]
    public void Start_writes_status_and_stops_when_mouse_is_caught()
    {
        WithTempDirectory(directory =>
        {
            string input = Path.Combine(directory, "ChaseData.txt");
            string output = Path.Combine(directory, "PursuitLog.txt");
            File.WriteAllText(input, "10\nM 3\nC 8\nP\nC -5\nM 4\nP\n");

            new Game(input, output).Start();

            string result = File.ReadAllText(output);
            Assert.Contains("  8     3         5", result);
            Assert.Contains("Mouse caught at:  3", result);
            Assert.DoesNotContain("Mouse evaded Cat", result);
            Assert.DoesNotContain("  7     7", result);
        });
    }

    [Fact]
    public void Start_prints_unknown_locations_until_both_players_enter()
    {
        WithTempDirectory(directory =>
        {
            string input = Path.Combine(directory, "ChaseData.txt");
            string output = Path.Combine(directory, "PursuitLog.txt");
            File.WriteAllText(input, "20\nM 15\nP\nC 3\nP\n");

            new Game(input, output).Start();

            string[] rows = File.ReadAllLines(output);
            Assert.Contains(" ??    15", rows);
			Assert.Contains("  3    15        12", rows);
            Assert.Contains("Mouse evaded Cat", rows);
        });
    }

    private static void WithTempDirectory(Action<string> action)
    {
        string directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try { action(directory); }
        finally { Directory.Delete(directory, recursive: true); }
    }
}
