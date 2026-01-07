namespace webScrapping.DTOs;

public class ScrapedMatchDto
{
    public string Date { get; set; } = string.Empty;
    public string PlayerHome { get; set; } = string.Empty;
    public string PlayerAway { get; set; } = string.Empty;
    public int P1Id { get; set; }
    public int P2Id { get; set; }
    public string P1Name { get; set; } = string.Empty;
    public string P2Name { get; set; } = string.Empty;
    public string LinkP1 { get; set; } = string.Empty;
    public string LinkP2 { get; set; } = string.Empty;
    public string LinkMatch { get; set; } = string.Empty;
    public int SetScoreP1 { get; set; }
    public int SetScoreP2 { get; set; }
}
