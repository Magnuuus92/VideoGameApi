using CsvHelper;
using System.Globalization;
using VideoGameApi.Models;

public interface IGameContext
{
    List<Game> GetAll();
    Game? GetById(int id);
    void Add(Game game);
    bool Update(int id, Game updatedGame);
    bool Delete(int id);

}