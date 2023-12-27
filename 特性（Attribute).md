特性（Attribute）是用于在运行时传递程序中各种元素（比如类、方法、结构、枚举、组件等）的行为信息的声明性标签。


### Net 框架提供了三种预定义特性：
##### AttributeUsage :
预定义特性 AttributeUsage 描述了如何使用一个自定义特性类。它规定了特性可应用到的项目的类型。

[AttributeUsage(AttributeTargets.Class |
AttributeTargets.Constructor |
AttributeTargets.Field |
AttributeTargets.Method |
AttributeTargets.Property, 
AllowMultiple = true)]

  CustomAttribute 特性可以应用于类和方法，但不允许多次应用,也不会被派生类继承。
```
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method ,AllowMultiple = false ,Inherited = false)]
public class CustomAttribute : Attribute
{
    
}
```

##### Conditional:
用于在调试时进行条件编译。可以应用于方法、类、结构体、接口和其他代码元素，用于指定在特定条件下是否应该编译和执行相关的代码。
[Conditional("DEBUG")]
```
public class MyClass
{
    [Conditional("DEBUG")]
    public void DebugMethod()
    {
        // 在 DEBUG 模式下才会被编译和执行的代码
    }

    public void NormalMethod()
    {
        // 始终会被编译和执行的代码
    }
}
```

#### Obsolete:
  用于标记已过时或不推荐使用的代码元素。它可以应用于类、结构体、方法、属性、字段等代码元素
  
 [Obsolete(message)]    参数 message，是一个字符串，描述项目为什么过时以及该替代使用什么。
          
```
  [Obsolete("方法过时")]
   public static int add(int a,int b)
   {
      return a + b;
   }
```

[Obsolete( message,iserror)]

参数 iserror，是一个布尔值。如果该值为 true，编译器应把该项目的使用当作一个错误。默认值是 false（编译器生成一个警告）。
```
public class MyClass
{
    [Obsolete("This method is deprecated. Use the NewMethod instead.", true)]
    public void DeprecatedMethod()
    {
        // 已过时的方法实现
    }

    public void NewMethod()
    {
        // 新方法的实现
    }
}
```
