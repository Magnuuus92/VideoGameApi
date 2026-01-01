using Microsoft.AspNetCore.Mvc;

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
                games = games.Where(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}