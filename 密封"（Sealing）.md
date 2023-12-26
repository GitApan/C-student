"密封"（Sealing）是一个面向对象编程的概念，用于表示一个类或方法不可被继承或重写。当一个类或方法被标记为密封（sealed）时，它将变为最终版本，不允许其他类继承该类或其他方法重写该方法。
1.密封类（Sealed Class）:
```
public sealed class SealedClass
{
    // Class implementation...
}
```
SealedClass被标记为sealed，其他类无法从SealedClass继承。

2.密封方法（Sealed Method）:
```
public class BaseClass
{
    public virtual void SomeMethod()
    {
        // Method implementation...
    }
}

public class DerivedClass : BaseClass
{
    public sealed override void SomeMethod()
    {
        // Method implementation...
    }
}
```
BaseClass中的SomeMethod方法被标记为virtual，表示它可以被子类重写。
而DerivedClass中的SomeMethod方法则使用sealed关键字标记，这意味着它是最终版本，不允许其他类再次重写该方法。
