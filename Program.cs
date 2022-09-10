using SentryTemplate.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseSentry("https://87853bc1473e41babe572c64fbb91d9e@o1381534.ingest.sentry.io/6737093");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();