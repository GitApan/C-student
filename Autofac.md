Autofac 是一个功能强大且流行的 .NET 依赖注入（DI）容器。它提供了灵活的注册和解析机制，能够轻松地实现依赖注入和解耦。
### 使用步骤：
##### 1.安装Autofac
通过 NuGet 包管理器或命令行安装 Autofac。
使用 NuGet 包管理器：在 Visual Studio 中，右键点击项目 -> 选择 "Manage NuGet Packages" -> 搜索 "Autofac" -> 安装。
##### 2.注册服务
  使用 Autofac，可以使用 ContainerBuilder 类来构建容器并注册服务
```   
       ContainerBuilder containerBuilder = new ContainerBuilder();
       //注册服务
       containerBuilder.RegisterType<MyService>().As<IMyService>();
       //构造容器
       var container = containerBuilder.Build();
```
##### 3.解析服务：
一旦你构建了 Autofac 容器，就可以通过调用 Resolve() 方法来解析和获取服务的实例。
 ```
// 解析服务
var myService = container.Resolve<IMyService>();

// 使用解析的服务实例
myService.DoSomething();
```

在上述示例中，我们使用 `container.Resolve()` 方法来解析 `IMyService` 的实例，并调用其中的方法。
### 生命周期管理：
Autofac 提供了不同的生命周期选项，用于控制服务实例的生命周期。常见的生命周期选项包括：

1.Transient（瞬时）：每次解析或注入时都会创建一个新的实例。
```
containerBuilder.RegisterType<MyService>().As<IMyService>().InstancePerDependency();
```
2.Singleton（单例）：在容器中只创建一个实例，并在整个应用程序生命周期内重复使用。
```
containerBuilder.RegisterType<MyService>().As<IMyService>().SingleInstance();
```
3.Scoped（作用域）：在每个作用域（如请求、会话）内创建一个实例，并在作用域内重复使用。
```
containerBuilder.RegisterType<MyService>().As<IMyService>().InstancePerLifetimeScope();
```
### 属性注入：
Autofac 支持属性注入，允许你在类中声明依赖关系的属性，并通过容器自动注入这些属性。
```
public class MyClass
{
    public IMyService MyService { get; set; }
}
```
### 模块化注册：
Autofac 提供了模块化注册的功能，允许你将相关服务的注册逻辑封装到单独的模块中，以提高可维护性和组织结构。
```
public class MyModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // 注册服务到容器
        builder.RegisterType<MyService>().As<IMyService>();
    }
}
```
### AOP（面向切面编程）和拦截器：
Autofac 允许使用拦截器来实现 AOP，通过在方法调用前后插入自定义逻辑，例如日志记录、性能监控等。

你可以通过实现 IInterceptor 接口创建自己的拦截器，并将其注册到 Autofac 容器中。
```
public class LoggingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        // 调用原始方法
        invocation.Proceed();
    }
}
```
