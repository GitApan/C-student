DBUp是一个.Net库，可以帮助将更改部署到SQL Server数据库。跟踪已运行的SQL脚本，并运行使数据库保存最新状态所需的更改脚本。
#  DBUP和EF CORE的区别
 -  功能和目的：
     - DbUp: DbUp 是一个数据库迁移和升级的库，它提供了一种简化和自动化数据库升级的方式。用于执行数据库脚本的部署和升级操作，通过执行脚本来创建、修改或删除数据库对象。
    - Entity Framework Core 是一个对象关系映射 (ORM) 框架，用于将数据库和应用程序中的对象进行映射和交互。它提供了一种面向对象的方式来操作和管理数据库，通过定义实体类和使用 LINQ 查询来进行数据库操作。
- 数据库迁移和升级：
   - DbUp: 专注于数据库的迁移和升级。允许你通过脚本文件或嵌入在应用程序中的脚本来执行数据库的创建、修改和删除操作。DbUp 提供了一种自动化的方式来执行这些脚本，并记录执行过程的日志。
   - EF Core: 也提供了数据库迁移的功能，可以通过定义数据迁移和更新代码来管理数据库的变化。它允许你通过命令行工具或代码生成迁移脚本，并自动执行这些脚本来更新数据库结构。除了迁移之外，EF Core 还提供了对数据库的查询和持久化操作的支持。
- 灵活性和控制:
    - DbUp: 提供了更多的灵活性和控制，你可以编写自定义的脚本逻辑，包括 SQL 脚本和其他脚本类型。你可以根据需要执行任意的数据库操作，但需要自己处理脚本的编写和管理。
    - EF Core: 提供了一种面向对象的开发模型，能够通过实体类和 LINQ 查询来进行数据库操作。
- 生态系统和集成
     - DBUp: DbUp 是一个相对较轻量级的库，它专注于数据库升级的功能。它可以和各种数据库管理系统 (如 SQL Server、MySQL、PostgreSQL 等) 集成，并且可以与各种应用程序类型 (如 .NET Framework、.NET Core) 一起使用。
    - EF Core: EF Core 是一个强大而全面的 ORM 框架，具有广泛的生态系统和工具支持。
##### 如果主要关注数据库迁移和升级，可以选择 DbUp；如果需要面向对象的数据库操作和 ORM 功能，可以选择 EF Core。在某些情况下，它们也可以一起使用，例如使用 DbUp 进行数据库的初始化和升级，然后使用 EF Core 进行日常的数据库操作。

# 配置
### 1.引入包
![image.png](https://upload-images.jianshu.io/upload_images/29491970-a533ace5eef2895f.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

#### 2.自定义类
```
public class DbUpRunner
{
    private readonly string _connectionString;   //字符串

    public DbUpRunner(string connectionString)  //构造函数 传入字符串
    {
        _connectionString = connectionString;
    }

    public void Run()   //run方法
    {
        EnsureDatabase.For.MySqlDatabase(_connectionString);   //确保数据库存在或创建数据库的操作。

        // DbUp 库来执行数据库升级操作的配置和构建过程
        var upgradeEngine = DeployChanges.To.MySqlDatabase(_connectionString)  //创建一个用于执行数据库升级的 UpgradeEngine 实例
            .WithScriptsEmbeddedInAssembly(typeof(DbUpRunner).Assembly)   //指定要执行的数据库脚本文件嵌入在 DbUpRunner 类所在的程序集中。
            .WithTransaction()  //在升级过程中启用事务。这意味着如果升级过程中出现错误，会自动回滚已执行的数据库更改。
            .LogToAutodetectedLog()  //动检测并配置日志记录器。DbUp 会尝试自动检测可用的日志记录器，并将升级过程的日志信息发送到该记录器中。
            .LogToConsole() //将升级过程的日志信息输出到控制台 
            .Build();

        var result = upgradeEngine.PerformUpgrade();  //执行数据库升级操作，并将结果存储在 result 变量中。
                                    //检查当前数据库的版本和已执行的升级脚本版本之间的差异。
                                    // 根据差异，执行未执行的升级脚本来将数据库升级到目标版本。
                                    // 在升级过程中，将记录升级的日志信息。

        if (!result.Successful)  //如果没有升级成功则抛出异常
            throw result.Error;
        {
            Console.ForegroundColor = ConsoleColor.Green;  //打印颜色为绿色
            Console.WriteLine("Success!");   //打印
            Console.ResetColor();   //恢复默认设置
        }
    }
}
```
### 3. 新建一个数据脚本
![image.png](https://upload-images.jianshu.io/upload_images/29491970-d26ae36a950a1631.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
```
create table if not exists user_question
(
   id int auto_increment
   primary key,
   created_at int not null,
   question varchar(512) charset utf8 not null,
   rasa_predicted_qid int not null,
   rasa_configence decimal(8,3) null,
    anyq_predicted_qid int not null,
    anyq_confidence decimal(8,3) null,
    corredt_qid int not null,
    model3_predicted_qid int default 0 null,
    model3_confidence decimal(8,3) default 0.000 null,
    status int default 0 null,
    remark varchar(512) charset utf8 null,
    ask_by varchar(128) charset utf8 null,
    constraint idx_question
    unique (question)
)
charset=utf8mb4
```
### 4.在.csproj中添加
```
    <ItemGroup>
        <EmbeddedResource Include="DB/Scripts_2023/Script0001_inital_tables.sql"/>
    </ItemGroup>
```
### 5.在Program中new的DbUpRunner类中的run方法
```
new DBUp("server=localhost;userid=root;password=1372213206;database=core").run();
```
### 6.运行成功
![image.png](https://upload-images.jianshu.io/upload_images/29491970-ffb06e703ad2f23e.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

# DBUP是如何不重复执行SQL文件的
 - 版本控制 
     通过对每个脚本文件进行版本控制来管理数据库的升级。DBUp会跟踪研究执行的脚本，并记录最后一个已执行的版本号。
 - 脚本文件状态记录
    DBUP会在数据库中创建一个用于记录已执行的脚本表-----“SchemaVersions”，这个表会存储研究执行的脚本的版本号和执行时间等信息。
   ![image.png](https://upload-images.jianshu.io/upload_images/29491970-461204747a6b8a09.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
- 脚本文件内容验证（根据哈希值进行计算）
    计算脚本文件的哈希值，并与之前执行过的脚本的哈希值进行比对。   
