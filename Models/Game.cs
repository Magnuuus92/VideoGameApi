using CsvHelper;

namespace VideoGameApi.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string Genre { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public double? GlobalSales { get; set; }
}