using System.Data.Common;
using System.Globalization;
using CsvHelper;
using VideoGameApi.Models;




public class GameContext : IGameContext
{
    private readonly string _filepath = "Data/VideoGames.csv";
    public List<Game> GetAll()
    {
        using var reader = new StreamReader(_filepath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Game>().ToList();
    }
    public Game? GetById(int id)
    {
        return GetAll().FirstOrDefault(g => g.GameID == id);
    }
    public void Add(Game game)
    {
        var games = GetAll();
        game.GameID = games.Any() ? games.Max(g => g.GameID) + 1 : 1;
        games.Add(game);
        WriteToFile(games);
    }
    public bool Update(int id, Game updatedGame)
    {
        var games = GetAll();
        var game = games.FirstOrDefault(g => g.GameID == id);
        if (game == null) return false;

        game.Title = updatedGame.Title;
        game.Platform = updatedGame.Platform;
        game.ReleaseYear = updatedGame.ReleaseYear;
        game.Genre = updatedGame.Genre;
        game.Publisher = updatedGame.Publisher;
        game.GlobalSales = updatedGame.GlobalSales;

        WriteToFile(games);
        return true;

    }
    public bool Delete(int id)
    {
        var games = GetAll();
        var game = games.FirstOrDefault(g => g.GameID == id);
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

    //ASYNC METODER
    public Task<List<Game>> GetAllAsync()
    {
        return Task.FromResult(GetAll());
    }
    public Task<Game?> GetByIdAsync(int id)
    {
        return Task.FromResult(GetById(id));
    }
    public Task AddAsync(Game game)
    {
        Add(game);
        return Task.CompletedTask;
    }
    public Task<bool> UpdateAsync(int id, Game updatedGame)
    {
        var result = Update(id, updatedGame);
        return Task.FromResult(result);
    }
    public Task<bool> DeleteAsync(int id)
    {
        var result = Delete(id);
        return Task.FromResult(result);
    }


}