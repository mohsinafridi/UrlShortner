using UrlShortner;
using UrlShortner.Models;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Services;
using UrlShortner.Entities;
using UrlShortner.Extensions;
using UrlShortner.Handler;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("Database")));


builder.Services.AddScoped<UrlShorteningService>();


builder.Services.AddScoped<IMyService,MyService>();
builder.Services.AddTransient<LoggingHandler>();
builder.Services.AddHttpClient<IBoredApiClient, BoredApiClient>(client =>
{
    client.BaseAddress = new Uri("https://www.boredapi.com/api");
}).AddHttpMessageHandler<LoggingHandler>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}


app.MapPost("api/shorten", async (
    ShortenUrlRequest request,
    UrlShorteningService urlShorteningService,
    ApplicationContext _dbContext,
    HttpContext _httpContext
    
    ) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
    {
        return Results.BadRequest("The specified URL is not valid.");
    }

    var code = await urlShorteningService.GenerateUniqueCode();

    var shorUrlObj = new ShortenedUrl
    {
        Id = Guid.NewGuid(),
        LongUrl = request.Url,
        ShortUrl = $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}/api/{code}",
        Code  = code,
        CreateAtUtc = DateTime.UtcNow,
    };

    _dbContext.ShortenedUrl.Add(shorUrlObj);

    await _dbContext.SaveChangesAsync();

    return Results.Ok(shorUrlObj.ShortUrl);
});

// Get 

app.MapGet("api/{code}",async (string code,ApplicationContext dbContext) => {
    
    var shortUrl = await dbContext.ShortenedUrl.FirstOrDefaultAsync(x => x.Code == code);

    if (shortUrl is null)
    {
        return Results.NotFound();
    }

    return Results.Redirect(shortUrl.LongUrl);
});


app.MapGet("api/activity", async (CancellationToken cancellationToken,IMyService service) => {
    
    ActivityModel modelResponse = await service.GetActivityAsyn(cancellationToken);

    return Results.Ok(modelResponse);
});

app.UseHttpsRedirection();

app.Run();

