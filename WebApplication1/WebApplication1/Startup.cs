using WebApplication1.Model;

namespace WebApplication1;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) 
        //这是一个公共方法，用于配置应用程序的服务。它接受一个IServiceCollection参数services，该参数用于注册和配置应用程序的服务。
    {
        services.AddSingleton<IMyService, MyService>(); 
        services.AddMvc();   //使用AddMvc方法将MVC服务添加到服务集合中
        services.AddControllers(); //将控制器服务添加到服务集合中
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //这是一个公共方法，用于配置应用程序的请求处理管道。它接受两个参数：IApplicationBuilder类型的app和IWebHostEnvironment类型的env。
        if (env.IsDevelopment())  //env参数是用于获取当前托管环境的对象。  用于检查当前环境是否为开发环境。这里使用条件语句判断当前环境是否为开发环境。
        {
            app.UseDeveloperExceptionPage();  //中间件，用于在开发环境下显示详细的开发者异常页面。当应用程序出现异常时，
                                              //可以显示详细的错误信息，帮助开发者进行调试。这个中间件只在开发环境中启用。
        }

        app.UseRouting();  //这是一个中间件，用于配置请求的路由。它负责将传入的请求与相应的处理程序进行匹配。
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });  
        //这是一个中间件，用于配置应用程序的终结点。MapControllers()方法将配置应用程序的控制器终结点，以便请求能够被相应的控制器进行处理。
    }
}

//这段代码的含义是在应用程序的请求处理管道中，根据当前环境配置异常页面中间件（仅在开发环境中启用），配置路由中间件，以及配置控制器终结点。
//这样，应用程序就能够根据路由的匹配将请求传递给相应的控制器进行处理。