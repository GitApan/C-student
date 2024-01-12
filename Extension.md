扩展方法（Extension methods）是一种在 C# 和其他支持扩展方法的编程语言中扩展现有类型的能力。它允许您向现有类型添加新的方法，而无需修改原始类型的定义。通过扩展方法，可以为现有的类型添加新的方法，就像它们的类型成员一样。可以在不修改原始类型的情况下，向其自定义行为。

##### 最常见的扩展方法是 LINQ 标准查询运算符，它们向现有System.Collections.IEnumerable和System.Collections.Generic.IEnumerable<T>类型添加查询功能

  #使用
扩展方法被定义为静态方法，可以通过实例方法进行调用，第一个参数该方法操作的类型，参数前要用this关键字修饰。
- public startic:指定扩展方法必须声明为公共的和静态的。
- retrunType：指定扩展方法的返回类型。
- MethodName:指定扩展方法的名称。
- this [Type] [instance] :   this关键字将扩展方法绑定到指定的类型上  [type]是扩展方法所扩展的类型，[instance]类型的实例。
- [parameters]:指定扩展方法的参数列表。
```
public static [returnType] [MethodName](this [Type] [instance], [parameters])
{
    // 扩展方法的实现
}
```

###  考核项目例子
```
using System.Linq.Expressions;

namespace PractiseForSavannah.Core.Extension;

public static class QueryableExtension
{
    //将给定的属性名称转换为一个表示属性访问的 Lambda 表达式。
    private static Expression<Func<T, object>> ToLambda<T>(string? propertyName)
    {
        //建一个表示泛型类型 T 的参数表达式，并将其赋值给 parameter 变量。这个参数表达式将用于表示 Lambda 表达式中的输入参数
        var parameter = Expression.Parameter(typeof(T));

        //将 parameter 和 propertyName 组合起来创建一个表示属性访问的表达式，并将其赋值给 property 变量。这个表达式表示要访问对象 parameter 的名为 propertyName 的属性。
        var property = Expression.Property(parameter, propertyName);

        //将 property 表达式转换为 typeof(object) 类型的表达式，并将其赋值给 propAsObject 变量。这是因为返回的 Lambda 表达式的类型是 Func<T, object>，即将属性的值转换为 object 类型。
        var propAsObject = Expression.Convert(property, typeof(object));

        //将 propAsObject 和 parameter 组合成一个 Lambda 表达式，并将其返回为 Expression<Func<T, object>> 类型。
        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
    
    //可排序的查询序列
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string? propertyName)
    {
        //传递一个 Lambda 表达式作为排序的依据  按照指定的属性进行排序
        return source.OrderBy(ToLambda<T>(propertyName));
    }
    
    //用于对给定的 IQueryable<T> 对象进行降序排序操作。它接受一个字符串参数 propertyName，表示要按照哪个属性进行降序排序。
    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source,string? properyName)
    {
        return source.OrderByDescending(ToLambda<T>(properyName));
    }
}
```
```
    public IQueryable<UserQuestion> GenerateUserQuestionsSorting(IQueryable<UserQuestion> query, string sortField, string sortDirection)
    {
        var sortPropertyName = FindSortPropertyNameBySortField(sortField);   //使用方法获取描述字段
        if (sortPropertyName != null)   //如果描述字段不为空的话
        {
            query = sortDirection switch  //排序方向的字符串参数
            {
                //当 sortDirection 等于 Constants.SortDirectionAscending 时，调用 query.OrderBy(sortPropertyName) 对查询进行升序排序。
                Constants.SortDirectionAscending => query.OrderBy(sortPropertyName),
                //当 sortDirection 等于 Constants.SortDirectionDescending 时，调用 query.OrderByDescending(sortPropertyName) 对查询进行降序排序。
                Constants.SortDirectionDescending => query.OrderByDescending(sortPropertyName),
                _=>query     //当 sortDirection 不匹配任何已定义的值时，返回原始的查询 query，即不进行排序。
            };
        }
        return query;
    }
```
### 列子2
```
namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static int WordCount(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            string[] words = input.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
    }
}
```
```
string text = "Hello, World!";
string reversedText = text.Reverse();
int wordCount = text.WordCount();

Console.WriteLine(reversedText); // 输出：!dlroW ,olleH
Console.WriteLine(wordCount); // 输出：2
```

##  扩展方法与自定义方法重名
   可以使用扩展方法来扩展类或接口，但不能覆盖它们。永远不会调用与接口或类方法具有相同名称和签名的扩展方法。扩展方法的优先级始终低于类型本身定义的实例方法
```
namespace ExtensionMethods
{
    public class MyClass
    {
        public void Reverse()
        {
            Console.WriteLine("Custom Reverse method");
        }
    }

    public static class MyExtensions
    {
        public static void Reverse(this MyClass myClass)
        {
            Console.WriteLine("Extension Reverse method");
        }
    }

    public class Program
    {
        public static void Main()
        {
            MyClass obj = new MyClass();
            obj.Reverse(); // 调用自定义方法，输出：Custom Reverse method
        }
    }
}
```
# 扩展与定义类型
扩展预定义类型是指在 C# 中可以对预定义类型（如object、string、int等）应用扩展方法的特殊机制。可以编写扩展方法来为现有的类型添加新的方法，以扩展其功能。通常情况下，只能为自定义的类或结构添加扩展方法。但是，C# 还引入了一个特殊的机制，即扩展预定义类型，允许为预定义类型添加扩展方法。

-  string 类型添加扩展方法的示例：
```
namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            // 检查字符串是否是回文
            // ...
        }
    }

    public class Program
    {
        public static void Main()
        {
            string text = "level";
            bool isPalindrome = text.IsPalindrome(); // 调用扩展方法
            Console.WriteLine(isPalindrome); // 输出：True
        }
    }
}
```
通过扩展预定义类型的机制，可以为预定义类型添加自定义的功能，提高代码的可读性和可维护性。
## 但请注意，扩展预定义类型可能会与命名空间中其他定义的方法产生冲突，因此请谨慎使用，并确保在不会引起歧义的情况下使用。

如果确实为给定类型实现扩展方法，请记住以下几点：
- 如果扩展方法与类型中定义的方法具有相同的签名，则永远不会调用该扩展方法。
- 扩展方法被引入命名空间级别的范围。例如，如果有多个静态类，这些静态类在名为 的单个命名空间中包含扩展方法Extensions，则它们都将被指令带入作用域using Extensions;。
