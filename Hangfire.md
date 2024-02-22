Hangfire 是一个用于在应用程序中管理后台作业的库，它确保作业至少执行一次，并提供持久性存储以在应用程序重新启动后继续存在。Hangfire 使用持久性存储来存储作业、队列和统计数据，并让它们在应用程序重新启动后继续存在。存储子系统足够抽象，可以支持经典的 SQL Server 和快速的 Redis。

- Hangfire的具有如下有点：

1.持久化保存任务、队列、统计信息：默认使用SQL Server，也可以配合消息队列来降低队列处理延 迟，或配置使用Redis来获得更好的性能表现
2.内置自动重试机制：可以设定重试次数，还可以手动在控制台重启任务
  除了调用静态方法外还支持实例方法能够捕获多语言状态：即可以把调用者的Thread.CurrentCulture和 
  Thread.CurrentUICulture信息同任务持久保存在一起，以便任务执行的时候多语言信息是一致的
3.支持任务取消：使用CancellationToken这样的机制来处理任务取消逻辑
4.支持IoC容器：目前支持Ninject和Autofac比较常用的开源IoC容器
5.支持Web集群：可以在一台或多台机器上运行多个Hangfire实例以便实现冗余备份
6.支持多队列：同一个Hangfire实例可以支持多个队列，以便更好的控制任务的执行方式
7.并发级别的控制：默认是处理器数量的5倍工作行程，当然也可以自己设定
8.具备很好的扩展性：有很多扩展点来控制持久存储方式、IoC容器支持等

- 与传统的 Timer 相比，Hangfire 具有以下几个区别：

持久性存储：Hangfire 使用持久性存储来保存任务和队列的状态，通常是使用外部存储（如 Redis、SQL Server）来存储任务信息。这使得即使应用程序重启或崩溃，任务的状态和调度信息仍然可靠，不会丢失。

高可靠性：Hangfire 提供了故障恢复机制，确保即使在应用程序异常终止或崩溃的情况下，任务仍然能够被执行。它能够自动重试失败的任务，并提供失败任务的管理和监控。

分布式架构：Hangfire 支持分布式架构，可以在多个服务器或节点上运行后台工作进程。这使得它能够处理大量的任务并具备高可伸缩性，适用于处理高负载和大规模任务的场景。

灵活的任务调度：Hangfire 提供了多种灵活的任务调度方式，可以基于时间间隔、特定时间点、延迟时间等来调度任务。它还支持任务的优先级设置、任务依赖关系的定义等。

可视化监控和管理：Hangfire 提供了一个直观的仪表板界面，用于监控和管理后台任务。你可以查看任务的状态、执行历史、错误日志等信息，并对任务进行管理，如手动触发任务、取消任务等操作。
- Hangfire 中的主要流程：
![image.png](https://upload-images.jianshu.io/upload_images/29491970-3c94de6261c40f70.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- Hangfire 支持使用 SQL Server 和 Redis 作为后端存储来管理后台任务。
- 1.SQL Server：Hangfire提供了对SQL Server的支持，可以将SQL Server作为后端存储来存储和管理后台任务的信息。通过配置Hangfire连接字符串和相关设置，可以将任务数据存储在SQL Server数据库中。

- 2.Redis：Hangfire还支持使用Redis作为后端存储。Redis是一个高性能的内存数据库，可以有效地管理后台任务的信息。通过配置Hangfire连接到Redis服务器，可以使用Redis来存储和管理任务数据。

# 使用 SQL Server
- NuGet 包

    Hangfire.SqlServer
    Microsoft.Data.SqlClient

- 需要配置 Hangfire 连接字符串和相关设置，将任务数据存储在 SQL Server 数据库中
```
GlobalConfiguration.Configuration
    .UseSqlServerStorage("connection_string", new SqlServerStorageOptions
    {
        SqlClientFactory = System.Data.SqlClient.SqlClientFactory
        // or
        SqlClientFactory = Microsoft.Data.SqlClient.SqlClientFactory
    });
```
- 选择使用连接字符串名称或直接使用自定义连接字符串来配置 SQL Server

```
GlobalConfiguration.Configuration
    // Use connection string name defined in `web.config` or `app.config`
    .UseSqlServerStorage("db_connection")
    // Use custom connection string
    .UseSqlServerStorage(@"Server=.\sqlexpress; Database=Hangfire; Integrated Security=SSPI;");
```
- 对于 SQL Server 存储，可以配置轮询间隔。SQL Server 作业存储实现的一个主要缺点是它使用轮询技术来获取新作业。你可以通过设置 QueuePollInterval 来调整轮询间隔。
```
var options = new SqlServerStorageOptions
{
    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
    QueuePollInterval = TimeSpan.Zero
};

GlobalConfiguration.Configuration.UseSqlServerStorage("<name or connection string>", options);
```

# 使用 Redis
使用 Redis 作业存储实现的 Hangfire 处理作业的速度比使用 SQL Server 存储快得多。
- NuGet包
Hangfire.Pro.Redis

- 连接字符串
```
GlobalConfiguration.Configuration.UseRedisStorage();
```
- 对于 ASP.NET Core 项目，UseRedisStorage从AddHangfire方法委托调用该方法：
````
services.AddHangfire(configuration => configuration.UseRedisStorage());
````

- Redis 集群支持
于Hangfire需要事务，而Redis不支持跨越多个哈希槽的事务，因此需要配置前缀以将其分配给相同的哈希标签
```
GlobalConfiguration.Configuration.UseRedisStorage(
    "localhost:6379,localhost:6380,localhost:6381",
    new RedisStorageOptions { Prefix = "{hangfire-1}:" });
```
- 传递选项
可以传递特定于 Hangfire 的 Redis 存储选项，使用 RedisStorageOptions 类实例进行配置
```
var options = new RedisStorageOptions
{
    Prefix = "hangfire:app1:"
};

GlobalConfiguration.Configuration.UseRedisStorage("localhost", options);
```
- 使用键前缀
```
var options = new RedisStorageOptions
{
    Prefix = "hangfire:"; // default value
};

GlobalConfiguration.Configuration.UseRedisStorage("localhost", 0, options);
```
# 后台作业的类型
1.Enqueue 方法：用于将一个方法调用添加到 Hangfire 的队列中，以便在后台执行。返回的 jobId 是作业的唯一标识符，可用于跟踪作业的状态和进度。

2.Schedule 方法：用于在指定的延迟时间后执行方法

3.ContinueWith 方法：在一个作业完成后继续执行另一个作业

4.Delay 方法：将方法调用延迟指定的时间后执行

5.ContinueJobWith 方法：用于在指定作业完成后执行另一个作业。jobId 是要延续的作业的标识符。

6.Delete 方法：删除指定的作业。jobId 是要删除的作业的标识符。

- #### 即发即忘
（用于卸载方法调用）
```
var jobId = BackgroundJob .Enqueue(
    () => Console .WriteLine( "一劳永逸！" ));
```
- #### 延迟
（在一段时间后执行调用）
```
var jobId = BackgroundJob .Schedule(
    () => Console .WriteLine( "延迟！" ),
     TimeSpan .FromDays(7));
```
- #### 重复
（每小时、每天执行方法等）
```
RecurringJob .AddOrUpdate(
     "myrecurringjob" ,
    () => Console .WriteLine( "重复执行！" ),
     Cron .Daily);
```
- #### 延续
    当父作业完成时执行延续。
```
后台作业.ContinueJobWith(
    职位编号,
    () => Console .WriteLine( "继续！" ));
```
- #### 批次
  批处理是一组以原子方式创建的后台作业。
```
var batchId = Batch.StartNew(x =>
{
    x.Enqueue(() => Console.WriteLine("Job 1"));
    x.Enqueue(() => Console.WriteLine("Job 2"));
});
```
- #### 批次延续
父批处理中的所有后台作业完成后， 将触发批处理延续。
```
Batch.ContinueBatchWith(batchId, x =>
{
    x.Enqueue(() => Console.WriteLine("Last Job"));
});
```
- #### 后台进程
  需要在应用程序的整个生命周期中连续运行后台进程时
```
public class CleanTempDirectoryProcess : IBackgroundProcess
{
    public void Execute(BackgroundProcessContext context)
    {
        Directory.CleanUp(Directory.GetTempDirectory());
        context.Wait(TimeSpan.FromHours(1));
    }
}
```
# hangfire任务排队机制：
任务入队：当使用 Hangfire 的 Enqueue、Schedule 或其他类似的方法创建一个任务时，任务会被添加到一个队列中。队列可以是基于内存的，默认是基于内存的队列，也可以选择使用持久性存储队列，如 Redis 或 SQL Server。

后台工作进程：Hangfire 需要一个或多个后台工作进程来处理队列中的任务。这些后台工作进程可以是独立的进程，也可以是与应用程序主进程一起运行的线程。后台工作进程会从队列中获取任务，并执行任务的方法。

任务执行：当后台工作进程获取到任务时，它会执行任务所关联的方法。执行过程发生在后台，不会阻塞应用程序的主线程。任务可以是简单的方法调用，也可以是复杂的操作，取决于你的代码逻辑。

任务状态和持久性：Hangfire 跟踪任务的状态并提供持久性存储，以确保即使应用程序重启，任务状态和队列仍然可用。任务可以具有不同的状态，如等待执行、执行中、已完成等。可以查询任务状态并监视任务的执行情况。

任务重试和失败处理：如果任务执行失败，Hangfire 提供机制来处理失败的任务。可以配置任务的重试次数和重试间隔，以及定义任务失败时的处理逻辑（如记录错误、发送通知等）。
# Cron
计划任务，是任务在约定的时间执行已经计划好的工作，这是表面的意思。在Linux中，他们经常用到 cron 服务器来完成这项工作。
#### 文件的格式：M H D m d
M: 分钟（0-59）。
H：小时（0-23）。
D：天（1-31）。
m: 月（1-12）。
d: 一星期内的天（0~7，0,7为星期天，6为星期六)

#### 特殊符号
*代表所有的取值范围内的数字

"/"代表每的意思

"*/5"表示每5个单位

"-"代表从某个数字到某个数字

","分开几个离散的数字



# 考核项目配置
### NuGet 包
![image.png](https://upload-images.jianshu.io/upload_images/29491970-adb665b80fa5c999.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

### 配置设置
appsettings.json文件
```
{
  "ConnectionStrings": {
    "HangfireConnection": "Server=.\\sqlexpress;Database=HangfireTest;Integrated Security=SSPI;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Hangfire": "Information"
    }
  }
}
```
```
public static class HangFireExtension
{
    public static void AddHangFireService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)//设置兼容性级别，指定与Hangfire版本1.7.0兼容。
            .UseSimpleAssemblyNameTypeSerializer()//使用简单的程序集名称类型序列化器。
            .UseRecommendedSerializerSettings()//使用建议的序列化设置。
            .UseStorage(//配置Hangfire使用的存储后端。在这里，是用来Mysql数据库作为存储，通过MySqlStorage类进行配置。
                new MySqlStorage(
                    configuration.GetConnectionString("Default"),//获取连接字符串
                    new MySqlStorageOptions
                    {
                        QueuePollInterval = TimeSpan.FromSeconds(10),//检查队列的时间间隔。
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),//作业到期检查的时间间隔。
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),//计数器聚合的时间间隔。
                        DashboardJobListLimit = 25000,//仪表板上作业列表的限制。
                        TransactionTimeout = TimeSpan.FromMilliseconds(1),//事务超过时间
                        TablesPrefix = "Hangfire"//表的前缀
                    }
                )
            ));
        
        services.AddHangfireServer(options => options.WorkerCount = 1);//启用Hangfire服务器，它处理后台任务。options.WorkerCount设置了同时处理任务的工作线程数量。
    }
}
```
在Configure方法中启用hangfire中间件:
```
app.UseHangfireServer();  //启动hangfire服务 
app.UseHangfireDashboard();  //启动hangfire面板
```
生成cron：
[https://bradymholt.github.io/cron-expression-descriptor/?locale=zh-CN&expression=0%2023%20?%20*%20MON-FRI](https://bradymholt.github.io/cron-expression-descriptor/?locale=zh-CN&expression=0%2023%20?%20*%20MON-FRI)
Hangfire学习文档：
https://docs.hangfire.io/en/latest/
