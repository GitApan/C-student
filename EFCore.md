##  什么是ORM？
RORM：Object R而是拉提偶哪里 Mapping。让开发者用对象操作的形式操作关系数据库。
#### 比如插入：
```
User user =new User(){Name= " admin",Password = '123'};
orm.Save(user);
```
####  比如查询：
```
Book b =orm.Bookd.Single(b=>b.Id==3 || b.Name.Contains("Net"));
String bookName= b.Name;
int  boold = b.Id;
```
有那些ORM：EF core、Dapper、SQLSugar 、FreeSQL

EF Core 优点： 功能强大、官方支持、生产效率高、力求屏蔽底层数据库差异。
缺点：负责、上手门槛高

##  约定配置：
1.表名采用DbContext中的对象的DbSet的属性名
2.数据表列的名字采用实体类属性的名字，列的数据类型和实体类属性类型最兼容的类型
3.数据表列的可空性取决于对应的实体类属性的可空性。
4.名字为ID的属性为主键，如果主键为short int long类型，则默认采用自增字段，如果主键为Guid类型，则默认采用默认的Guid生成机制生成主键值。

##  两种配置方式：
1.Data Annotation
把配置以特性的形式标注在实体类中。
优点：简单
缺点：耦合
```
[Table("T_Books")]
public class Book{
}     
```
2.Fluent API
把配置写到单独的配置类中
缺点：复杂
优点：解耦
```
 public class BookConfig : IEntityTypeConfiguration<Book>
    {
   
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("T_Books");
            builder.Property(b=>b.Id).HasMaxLength(5).IsRequired();
            builder.Property(b=>b.Title).HasMaxLength(10).IsRequired();
            }
    }
```
1.视图与实体类映射：
```
modelBuilder.Entity<Blog>.ToView("blogsView");
```
2.排除属性映射：
```
modelBuilder.Entity<Blog>().Ignore(b+b.Name2);
```
3.配置列名：
```
modelBuilder.Entity<Blog>().Property(b=>b.BlogId).HasColumnName("blog_id");
```
4.配置列数据类型：
```
builder.Property(e=>e.Title).HasColumnType("varchar(200)");
```
5.配置主键
默认吧名字为ID或者实体类型+Id”的属性作为主键，可以用HasKey来配置其他属性作为主键
```
modelBuilder.Entity<Student>().HasKey(c=>c.Number);
```
6.生成列的值
```
modelBuilder.ENtity<Student>().Property(b=>b.Number).ValueFeneratesOnAdd();
```
7.可以用HasDefaultvalue（）为属性设置默认值
```
modelBuilder.Entity<Student>().Property(b=>b.Age).HasDefaultValue(6);
```
8.索引
```
modelBuilder.Entity<Blog>().HasIndex(b=>b.Url);
```
9.复合索引
```
modelBuilder.ENtity<Person>().HasIndex(p=>new{p,FirstName,p.LastName});
```
10.唯一索引
```
IsUnique();
```
11.聚集索引:
```
IsClustered();
```
###  EF Core列子 （用Fluent API配置）
1、引入基础包Microsoft.EntityFrameworkCore、根据你使用的数据库类型引用对应的包，这里我使用Mysql则引用Pomelo.EntityFrameworkCore.MySql。
2.创建Config类继承IEntityTypeConfiguration
```
namespace WebApplication8
{
    //配置实体类和数据库表的对应关系
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
    
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("T_Books");
            builder.Property(b=>b.Id).HasMaxLength(5).IsRequired();
            builder.Property(b=>b.Title).HasMaxLength(10).IsRequired();
        }
    }
}
```
3.创建一个Context类，继承DbContext，重写OnConfiguring(配置数据库)和OnModelCreating(配置实体)方法
```
public class TestDbContext : DbContext
    {
        //实体类型
        public DbSet<Book> Books { get; set; }

        //指定连接数据库
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = "server=localhost;user=root;pwd = 123456;port=3306;database = cores";
             optionsBuilder.UseMySQL(connStr);
        }
        //指定配置类
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);  //从当前程序集加载
        }
    }
}
```
4.创建实体类
```
public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime PubTime { get; set; }
         public double Price { get; set; }
    }
```
官网：https://learn.microsoft.com/zh-cn/ef/core/
