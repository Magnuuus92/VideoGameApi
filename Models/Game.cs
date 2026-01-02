using CsvHelper;

namespace VideoGameApi.Models;

public class Game
{
    public int GameID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public int? ReleaseYear { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public double? GlobalSales { get; set; }
    public string? Rating { get; set; }
}