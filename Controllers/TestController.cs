using Microsoft.AspNetCore.Mvc;
using SentryTemplate.HttpsException;

namespace SentryTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    public TestController(ILogger<TestController> logger)
    {
    }

    [HttpGet("/ExpectedError")]
    public void GetExpectedError()
    {
        throw new BadRequestException("um erro de badRequest");
    }
    
    [HttpGet("/UnexpectedError")]
    public void GetUnexpectedError()
    {
        throw new ArgumentException("um erro de UnexpectedError");
    }
}