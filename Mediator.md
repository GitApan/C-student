Mediator是一种设计模式，用于解决对象之间的复杂通信和相互依赖关系，通过引入一个中介者对象来减少对象之间的直接交互。

![image.png](https://upload-images.jianshu.io/upload_images/29491970-b60169bf5a0ee2f7.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


从上图可以发现，如果不使用中介者模式的话，每个对象之间过度耦合，这样的既不利于类的复用也不利于扩展。如果引入了中介者模式，那么对象之间的关系将变成星型结构，采用中介者模式之后会形成如下图所示的结构：

![image.png](https://upload-images.jianshu.io/upload_images/29491970-7f9152c7934037ca.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)    
     
####  1.目的
降低对象之间的耦合性，使对象能够独立地进行交互和通信。通过引入一个中介者对象，对象之间不再直接相互引用，而是通过中介者进行通信，从而减少对象之间的依赖关系。
####  2.参与角色
  Mediator（中介者）：
       中介者对象定义了对象之间的通信接口，并负责协调和管理对象之间的交互。它了解各个对象的接口和行为，并根据接收到的消息进行相应的处理和转发。
 Colleague（同事）：
同事对象是 Mediator 模式中的参与者，它们之间不直接通信，而是通过中介者进行通信。同事对象之间可以有不同的角色和行为，但它们共享同一个中介者来进行交互。

#### 3.工作流程
当一个同事对象需要与其他对象进行通信时，它将消息发送给中介者对象。
中介者接收到消息后，根据消息的内容和发送者的标识，进行相应的处理和转发。
中介者将消息传递给目标对象，或者将消息广播给其他相关对象。
目标对象接收到消息后，根据自身的逻辑进行处理。
这样，同事对象之间的通信就通过中介者对象进行了解耦，每个对象只需与中介者进行通信，而无需直接与其他对象进行交互。

#### 4.优点：

减少了对象之间的依赖关系，提高了系统的灵活性和可维护性。
简化了对象之间的通信，使系统更易于理解和扩展。
集中化了交互和控制逻辑，提高了系统的可控性和可测试性。

实例代码：租房例子
```
//中介者  抽象类
public abstract class Mediator
{
    //交互方法
    public abstract void Contact(String messsage,Person person);
}

//中介实现类
public class ConcreteMediator : Mediator
{
    public Tenant Tenant { get; set;}

    public HouserOwner HouserOwner { get; set; }

    public override void Contact(string messsage, Person person)
    {
        if (person == Tenant)
        {
            Tenant.GetMessage(messsage);
        }
        else
        {
            HouserOwner.getMessage(messsage);
        }
    }
}



public  abstract class Person
{
    protected string Name { get; set; }
    protected Mediator _mediator;   //传入中介模式
    protected Person(string name,Mediator mediator)
    {
        this.Name = name;
        this._mediator = mediator;
    }
}

//租户 
public class Tenant : Person
{
    public Tenant(string name, Mediator mediator) : base(name, mediator)
    {
        
    }

    public void Contact(string message)
    {
        _mediator.Contact(message,this);
    }

    public void GetMessage(string messsage)
    {
        Console.WriteLine("租房者："+Name+"发布消息："+messsage);
    }
}

//房东
public class HouserOwner : Person
{
    public HouserOwner(string name, Mediator mediator) : base(name, mediator)
    {
    }

    public void Contact(string message)
    {
        _mediator.Contact(message,this);
    }

    public void getMessage(string Message)
    {
        Console.WriteLine("房主；"+Name+"消息："+Message);
    }
}

public class Program
{
    public static void main(string[] arg)
    {
        ConcreteMediator m = new ConcreteMediator();
        
        HouserOwner houserOwner = new HouserOwner("lily", m);
        Tenant t = new Tenant("sam", m);

        m.HouserOwner = houserOwner;
        m.Tenant = t;
        
        //通过中介进行交互
        t.Contact("我要租房");
        houserOwner.Contact("有房");
    }
}
```
