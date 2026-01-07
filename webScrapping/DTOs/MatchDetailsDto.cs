namespace webScrapping.DTOs;

public class MatchDetailsDto
{
    public int NumberOfSets { get; set; }
    public List<int> PointsP1 { get; set; } = new();
    public List<int> PointsP2 { get; set; } = new();
    public string MatchLink { get; set; } = string.Empty;
}
