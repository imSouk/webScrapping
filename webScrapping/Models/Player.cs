namespace webScrapping.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PlayerUrl { get; set; } = string.Empty;
    public int[] GameScore { get; set; } = Array.Empty<int>();
}
