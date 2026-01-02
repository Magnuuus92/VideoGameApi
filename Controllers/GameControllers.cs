using Microsoft.AspNetCore.Mvc;
using VideoGameApi.Models;

[ApiController]
[Route("/[Controller]/[Action]")]
public class GameController : ControllerBase
{
    private readonly IGameContext _context;
    private readonly ILogger<GameController> _logger;

    public GameController(IGameContext context, ILogger<GameController> logger)
    {
        _context = context;
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Get([FromQuery] string? name)
    {
        try
        {
            var games = _context.GetAll();

            if (!string.IsNullOrEmpty(name))
                games = games.Where(g => g.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(games);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get failed");
            return StatusCode(500);

        }
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var game = _context.GetById(id);
        if (game == null) return NotFound();
        return Ok(game);
    }
    [HttpPost]
    public IActionResult Create([FromBody] Game game)
    {
        try
        {
            _context.Add(game);
            return CreatedAtAction(nameof(GetById), new { id = game.GameID }, game);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "create failed");
            return StatusCode(500);
        }
    }
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, [FromBody] Game game)
    {
        if (!_context.Update(id, game))
            return NotFound();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_context.Delete(id))
            return NotFound();
        return NoContent();
    }
}