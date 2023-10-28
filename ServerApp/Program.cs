using speed.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IWebSocketHandler, WebSocketHandler>();

builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

var VerSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(120),
};
app.UseWebSockets(VerSocketOptions);
//app.UseMiddleware<WebSocketMiddleware>();
app.MapControllers();

app.UseHttpsRedirection();
app.Run();
