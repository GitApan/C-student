具体依赖抽象，上层依赖下层。假设B是较A低的模块，但B需要使用到A的功能，这个时候，B不应当直接使用A中的具体类： 而应当由B定义一个抽象接口，并由A来实现这个抽象接口，B只使用这个抽象接口：这样就达到了依赖倒置的目的，B也解除了对A的依赖，反过来是A依赖于B定义的抽象接口。通过上层模块难以避免依赖下层模块，假如B也直接依赖A的实现，那么就可能造成循环依赖。一个常见的问题就是编译A模块时需要直接包含到B模块的cpp文件，而编译B时同样要直接包含到A的cpp文件。
```
// 定义抽象接口
public interface IDatabase
{
    void Connect();
    void Disconnect();
    void Query(string sql);
}

// 实现具体的MySQL数据库类
public class MySQLDatabase : IDatabase
{
    public void Connect()
    {
        Console.WriteLine("Connecting to MySQL database");
    }

    public void Disconnect()
    {
        Console.WriteLine("Disconnecting from MySQL database");
    }

    public void Query(string sql)
    {
        Console.WriteLine("Executing MySQL query: " + sql);
    }
}

// 实现高层模块，依赖于抽象接口
public class DataAccessLayer
{
    private IDatabase database;

    public DataAccessLayer(IDatabase db)
    {
        database = db;
    }

    public void PerformDatabaseOperations()
    {
        database.Connect();
        database.Query("SELECT * FROM table");
        database.Disconnect();
    }
}

// 在应用程序中使用
public class Program
{
    public static void Main()
    {
        // 创建具体的数据库对象
        MySQLDatabase mysqlDb = new MySQLDatabase();

        // 创建高层模块对象，并将具体的数据库对象传递给它
        DataAccessLayer dataAccessLayer = new DataAccessLayer(mysqlDb);

        // 执行数据库操作
        dataAccessLayer.PerformDatabaseOperations();
    }
}
```
