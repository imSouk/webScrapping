namespace webScrapping.Models;

public class Match
{
    public int MatchId { get; set; }
    public string Date { get; set; } = string.Empty;
    public string Player1 { get; set; } = string.Empty;
    public string Player2 { get; set; } = string.Empty;
    public string LinkP1 { get; set; } = string.Empty;
    public string LinkP2 { get; set; } = string.Empty;
    public string LinkMatch { get; set; } = string.Empty;
    public int SetScoreP1 { get; set; }
    public int SetScoreP2 { get; set; }
    public int P1Id { get; set; }
    public int P2Id { get; set; }
    public string P1Name { get; set; } = string.Empty;
    public string P2Name { get; set; } = string.Empty;

    public Match()
    {
    }

    public Match(string date, string player1, string player2, int scoreP1, int scoreP2, 
        string linkP1, string linkP2, string linkMatch, int p1Id, int p2Id, string p1Name, string p2Name)
    {
        Date = date;
        Player1 = player1;
        Player2 = player2;
        P1Id = p1Id;
        P2Id = p2Id;
        P1Name = p1Name;
        P2Name = p2Name;
        LinkP1 = linkP1;
        LinkP2 = linkP2;
        LinkMatch = linkMatch;
        SetScoreP1 = scoreP1;
        SetScoreP2 = scoreP2;
    }
}
