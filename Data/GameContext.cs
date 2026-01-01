using System.Data.Common;
using System.Globalization;
using CsvHelper;
using VideoGameApi.Models;




public class GameContext : IGameContext
{
    private readonly string _filepath = "data/Videoames.csv";
    public List<Game> GetAll()
    {
        using var reader = new StreamReader(_filepath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Game>().ToList();
    }
    public Game? GetById(int id)
    {
        return GetAll().FirstOrDefault(g => g.Id == id);
    }
    public void Add(Game game)
    {
        var games = GetAll();
        game.Id = games.Max(g => g.Id) + 1;
        games.Add(game);
        WriteToFile(games);
    }
    public bool Update(int id, Game updatedGame)
    {
        var games = GetAll();
        var game = games.FirstOrDefault(g => g.Id == id);
        if (game == null) return false;

        game.Name = updatedGame.Name;
        game.Platform = updatedGame.Platform;
        game.Year = updatedGame.Year;
        game.Genre = updatedGame.Genre;
        game.Publisher = updatedGame.Publisher;
        game.GlobalSales = updatedGame.GlobalSales;

        WriteToFile(games);
        return true;

    }
    public bool Delete(int id)
    {
        var games = GetAll();
        var game = games.FirstOrDefault(g => g.Id == id);
        if (game == null) return false;
        games.Remove(game);
        WriteToFile(games);
        return true;
    }
    private void WriteToFile(List<Game> games)
    {
        using var writer = new StreamWriter(_filepath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(games);
    }


}