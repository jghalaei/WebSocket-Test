internal class Program
{



    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        var app = builder.Build();

        var VerSocketOptions = new WebSocketOptions
        {
            KeepAliveInterval = TimeSpan.FromMinutes(5),
        };
        app.UseWebSockets(VerSocketOptions);
        //app.UseMiddleware<WebSocketMiddleware>();
        app.MapControllers();

        app.Run();
    }
}