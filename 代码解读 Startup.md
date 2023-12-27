Startup类是应用程序的配置类，用于配置应用程序的服务和请求处理管道。
Startup类包含两个核心方法：ConfigureServices和Configure。
ConfigureServices方法用于配置应用程序的服务容器（IServiceCollection），注册应用程序所需的服务。
Configure方法用于配置应用程序的请求处理管道（IApplicationBuilder），通过添加中间件和定义请求处理的顺序来处理传入的请求。
### 整段代码  
公共方法，用于配置应用程序的服务。它接受一个IServiceCollection参数services，该参数用于注册和配置应用程序的服务。
```
public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();  
        services.AddControllers(); 
    }
```
使用AddMvc方法将MVC服务添加到服务集合中
```
 services.AddMvc();
```
将控制器服务添加到服务集合中
```
services.AddControllers(); 
```

### 整段代码
```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
       if (env.IsDevelopment()) 
        {
            app.UseDeveloperExceptionPage();  
        }
        app.UseRouting(); 
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });  
     
    }
```
env参数是用于获取当前托管环境的对象。  用于检查当前环境是否为开发环境。这里使用条件语句判断当前环境是否为开发环境。
中间件，用于在开发环境下显示详细的开发者异常页面。当应用程序出现异常时，
可以显示详细的错误信息，帮助开发者进行调试。这个中间件只在开发环境中启用。
```
if (env.IsDevelopment()) 
        {
            app.UseDeveloperExceptionPage(); 
        }
```
这是一个中间件，用于配置请求的路由。它负责将传入的请求与相应的处理程序进行匹配。
这是一个中间件，用于配置应用程序的终结点。MapControllers()方法将配置应用程序的控制器终结点，以便请求能够被相应的控制器进行处理。
```
 app.UseRouting();  
 app.UseEndpoints(endpoints => { endpoints.MapControllers(); });  
```
