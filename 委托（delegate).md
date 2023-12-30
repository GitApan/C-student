## 什么是委托?
   如果我们要把方法当做参数来传递的话，就要用到委托。简单来说委托是一个类型，这个类型可以复制一个方法的引用。
     在C#中使用一个类分两个阶段，首先定义这个类，告诉编译器这个类由什么字段和方法组成的，然后使用这个类的实例化对象。在我们使用委托的时候，也需要经过这两个阶段，首先定义委托，告诉编译器我们这个委托可以指向哪些类型的方法，然后，创建该委托的实例。
### 声明委托：
  关键字 delegate，无返回值
```
  delegate void  IntMethodInvoker(int X);
```
带有返回值
 ```
delegate double TowLongOP(long firt,long second);`
delegate string GetAString();
```
### 使用委托
```
Private delegate string GetAString();
static void Main(){
   int x =40;
   GetASTring fristStringMethod =new GetASTtring(x.ToString);
   }
}
```
### 通过委托示例调用方法有两种方式
````
fristStringMethod();
firstStringMethod.Invoke();
````
### 委托的赋值
````
GetAString firstStringMethod = new GetAString(x.ToString);//只需要把方法名给一个委托的构造方法就可以了
GetAString firstStringMethod = x.ToString;也可以把方法名直接给委托的实例
````
### Action委托和Func委托
除了我们自己定义的委托之外，系统还给我们提供过来一个内置的委托类型，Action和Func.
-  Action委托引用了一个void返回类型的方法，T表示方法参数
Action
Action<in T>
Action<in T1,in T2>
Action<in T1,in T2 .... inT16>
      - 无参数：
```
private static void Test1(){
   Console.WriteLine("test1");
}
static void Main(STring[] args){
  Action method =Test1;
method();
}
```
   - 有参数
```
     private static void Test2(int x ){
      Console.WriteLine("Test2"+x);
  }
   static void Main(String[] args){
     Action<int> method =Test2;
      method();
  }
```
- Func引用了一个带有一个返回值的方法，它可以传递0或者多到16个参数类型，和一个返回类型
Func<out TResult>
Func<in T,out TResult>
Func<int T1,inT2,,,,,,in T16,out TResult>
  - 无参数
```
   private static string Test1(){
     return "Test1";
  }
   static  void   Main（string[] args）{
      Func<string> f =Test1;
      f();
  }
```
  - 有参数  Func<参数类型，返回值>
```
   private static string Test1(int x){
     return "Test1"+x;
  }
   static  void   Main（string[] args）{
      Func<int,string> f =Test1;
      f();
  }
```
### 多播委托
前面使用的委托都只包含一个方法的调用，但是委托也可以包含多个方法，这种委托叫做多播委托。
```
Action action1 = Test1;
action1+=Test2;
action1-=Test1;
```
多播委托包含一个逐个调用的委托集合，如果通过委托调用的其中一个方法抛出异常，整个
 迭代就会停止。
- 取得多播委托中所有方法的委托
```
Action a1 = Method1;
a1+=Method2;
Delegate[] delegates=a1.GetInvocationList();
foreach(delegate d in delegates){
//d();
d.DynamicInvoke(null);
}
```
遍历多播委托中所有的委托，然后单独调用
