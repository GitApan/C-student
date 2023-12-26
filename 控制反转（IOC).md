#### 控制反转（IoC）的原则：
IoC 的核心思想是将对象的创建和依赖关系的管理交给外部容器或框架，而不是由对象自身来控制。这样做可以实现松耦合、可扩展和可测试的代码架构。

#### IoC 容器的角色：
IoC 容器是负责管理对象的生命周期和依赖关系的组件。它负责创建对象、解析依赖项，并在需要时注入它们。容器通常从配置文件或代码中读取依赖关系的映射，并根据需要实例化和管理对象。

#### 注册和解析依赖项：
在使用 IoC 容器时，需要将依赖项注册到容器中，以便容器能够知道如何创建和提供它们。注册通常包括指定依赖项的接口或抽象类以及其对应的具体实现。一旦依赖项注册完成，可以通过容器的解析机制获取依赖项的实例。

#### 生命周期管理：
IoC 容器通常提供了不同的生命周期管理选项，用于控制对象的创建和销毁。常见的生命周期选项包括：

1.瞬态（Transient）生命周期：每次解析时都会创建一个新的对象实例。
```
   containerBuilder.RegisterType<MyService>().As<IMyClass>().InstancePerDependency();
```
2.单例（Singleton）生命周期：容器中只有一个实例，并在整个应用程序生命周期中重用。
```
          containerBuilder.RegisterType<MyService>().As<IMyClass>().SingleInstance();
```
3.作用域（Scoped）生命周期：在每个作用域（例如 HTTP 请求）内保持相同的实例，并在作用域结束时销毁。
```
          containerBuilder.RegisterType<MyService>().As<IMyClass>().InstancePerLifetimeScope();
```
生命周期管理可以确保对象的正确创建和销毁，避免资源泄露和不必要的开销。

### 使用autofac实现IOC
 ```
public interface IMyClass
{
    public void Send(String Message);
}


public class MyService : IMyClass
{
    public void Send(string Message)
    {
        Console.WriteLine("Myservice"+Message);
    }
}

using Autofac;
public class MyTest
{
     public void test()
     {
          ContainerBuilder containerBuilder = new ContainerBuilder();
          containerBuilder.RegisterType<MyService>().As<IMyClass>();
          var container = containerBuilder.Build();
          var myClass = container.Resolve<IMyClass>();
          myClass.Send("Service");
     }   
}
```
