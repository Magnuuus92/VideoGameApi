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
    public async Task<IActionResult> Get([FromQuery] string? name)
    {
        try
        {
            var games = await _context.GetAllAsync();

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
    public async Task<IActionResult> GetById(int id)
    {
        var game = await _context.GetByIdAsync(id);
        if (game == null) return NotFound();
        return Ok(game);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Game game)
    {
        try
        {
            await _context.AddAsync(game);
            return CreatedAtAction(nameof(GetById), new { id = game.GameID }, game);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "create failed");
            return StatusCode(500);
        }
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] Game game)
    {
        var updated = await _context.UpdateAsync(id, game);
        if (!updated)
            return NotFound();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _context.DeleteAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}