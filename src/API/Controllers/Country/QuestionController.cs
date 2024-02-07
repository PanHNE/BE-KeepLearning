using Application.Question.Queries.CheckAnswer;
using Application.Question.Queries.GenerateQuestion;

namespace API.Controllers;

[ApiController]
[Route("api/country/[controller]")]
public class QuestionController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly ILogger<QuestionController> _logger;

    public QuestionController(IMediator mediator, ILogger<QuestionController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate(GenerateQuestionQuery generateQuestionQuery)
    {
        var question = await _mediator.Send(generateQuestionQuery);

        return Ok(question);
    }

    [HttpPost("check")]
    public async Task<IActionResult> Check(CheckQuestionQuery checkQuestionQuery)
    {
        var result = await _mediator.Send(checkQuestionQuery);

        return Ok(result);
    }
}
