Program类是应用程序的入口点，包含Main方法，它是应用程序启动的起点。
Main方法负责创建主机（Host）并启动应用程序的执行。
在Main方法中，通常会调用CreateHostBuilder方法创建主机构建器，并调用Build方法构建主机，最后调用Run方法启动应用程序的执行。
Program类还可以在需要进行一些初始化操作的时候提供额外的方法或逻辑。
#### 整段代码
```
 public static void Main(string[] args)
    {
           var configuration = new ConfigurationBuilder() 
            .AddJsonFile("appsettings.json")  
            .AddEnvironmentVariables() 
            .Build();          
            CreateHostBuilder(args).Build().Run();
    }
```
创建ConfigurationBuilder对象，用于构建应用程序配置
```
  var configuration = new ConfigurationBuilder() 
```
从指定的JSON文件中加载配置值
```
.AddJsonFile("appsettings.json")
```
从环境变量中加载配置值
```
.AddEnvironmentVariables() 
```
构造配置对象，转换为IConfiguration实例
 ```
 .Build();  
```
创建一个主机构建器，使用传入的命令行参数args配置主机，然后构建主机并启动应用程序的执行。
```
CreateHostBuilder(args).Build().Run();
```

### 整段代码
```
   public static IHostBuilder CreateHostBuilder(string[] args) =>  
   Host.CreateDefaultBuilder(args)
 .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });    
}
```
静态方法，用于创建一个默认的主机构建器（IHostBuilder）。它会自动配置一些常见的设置和服务，例如日志、配置等。
```
Host.CreateDefaultBuilder(args)
```
指定了启动类为Startup。CreateHostBuilder方法返回这个主机构建器，以便在Main方法中进行构建和运行。
```
.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
```
