using WebApplication1.Model;

namespace WebApplication1;

public class Program
{
    public static void Main(string[] args)
    {
           var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

           Container container = new Container();
           container.Register<IMyService,MyService>();
           
           IMyService service = container.Resolve<IMyService>();
           service.DoSomething();

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>  
     
     Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                
                webBuilder.UseStartup<Startup>();
            });
    
}