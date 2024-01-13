![image.png](https://upload-images.jianshu.io/upload_images/29491970-27ab6d998d8992cc.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

![image.png](https://upload-images.jianshu.io/upload_images/29491970-717c4ebed5f99abe.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

# DTO数据传输对象
   DTO是前后端之间的传输，主要用于将数据从一个层传递到另一个层。
   在后端是一个对像，在前端是一个JSON，就是通过AJAX请求的数据体。
   - 目的
实现在不同层之间进行数据之间的传递和交换，仅仅只有数据属性，没有任何的业务逻辑和行为。

  - 好处
1. 接耦层之间的依赖：每一层可以根据自己的需求定义自己的数据结构，不会依赖其他层的实体对象。
2.避免数据暴露： 在DTO可以选择性的暴露特定的数字属性，而不会暴露整个对象的所有属性。
3.提高性能： 减少层之间的传输数据量
4.灵活性和可扩展性：可以根据自定义的数据结构，适应不同层之间的数据交互。

# VO  
  视图对象，用于展示层，它的作用是把某个指定页面（或组件）的所有数据封装起来。

- 特点
1. 不可变性：一旦创建不可以被修改
2.根据值进行相等性的比较：
3.无标识性：没有唯一的标识符

# PO
持久化对象，它跟持久层（通常是关系型数据库）的数据结构形成一一对应的映射关系，如果持久层是关系型数据库，那么，数据表中的每个字段（或若干个）就对应PO的一个（或若干个）属性。由一组属性和属性的get和set方法组成。对应数据库中某个表的记录，PO层不能包含对数据库的操作。

# BO 
BO通常指的是业务对象，表示业务概念和业务逻辑的实体。BO是对现实世界中业务实体的抽象和建模，封装了与业务相关的数据和操作。
PO是一条交易记录，BO是一个人全部的交易记录集合对象。
BO是一个业务对象，一类业务就会对应一个BO，数量上没有限制，而且BO会有很多业务操作，除了get、set方法以外，BO会有很多根据自身数据进行计算的方法。

# DO
从现实世界中抽象出来的有形或无形的业务实体。DO 通常包含领域模型中的属性、方法和行为，它们负责封装业务规则、处理业务操作和维护对象的一致性。DO 的设计目标是反映业务领域的语义和规则，以便更好地理解和操作业务数据。

#####  BO功能

-  业务数据：BO层包含业务对象的属性和数据，也用于描述业务实体的特征和状态。属性可以是简单的值类型，也可以是负责的业务对象。
- 业务操作： 定义了对业务对象进行的操作和行为（创建、修改、删除），与业务逻辑相关。
- 业务验证： BO层负责验证业务对象的数据的合法性和完整性
- 业务逻辑：BO层包含了与业务实体相关的业务逻辑和规则。

BO层的设计目标是将业务逻辑和业务数据集中在一起，使其独立于应用程序的其他层（如表示层、数据访问层等）。这样可以实现业务逻辑的重用、可测试性和可维护性。

# 易混点一 VO和DTO
   VO是视图对象，说白就是展示用的。DTO是展示层与服务层之间传递数据的对象。在绝大部分的场景，DTO和VO的数据值基本是一致的。
```
// Value Object (VO)
public class AddressVO
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }

    public AddressVO(string street, string city, string state, string postalCode)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }
}

// Data Transfer Object (DTO)
public class CustomerDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public AddressVO Address { get; set; }
}

// Usage
public class Program
{
    public static void Main()
    {
        // 创建一个 Value Object (VO)
        AddressVO address = new AddressVO("123 Main St", "City", "State", "12345");

        // 创建一个 DTO 对象，并设置其属性值
        CustomerDTO customerDTO = new CustomerDTO();
        customerDTO.Name = "John Doe";
        customerDTO.Email = "john.doe@example.com";
        customerDTO.Address = address;

        // 使用 DTO 对象进行数据传输或其他操作
        Console.WriteLine($"Name: {customerDTO.Name}");
        Console.WriteLine($"Email: {customerDTO.Email}");
        Console.WriteLine($"Address: {customerDTO.Address.Street}, {customerDTO.Address.City}, {customerDTO.Address.State}, {customerDTO.Address.PostalCode}");
    }
}
```
AddressVO 是一个值对象（VO），它表示客户的地址，具有不可变的属性（如街道、城市、州和邮政编码）。

CustomerDTO 是一个数据传输对象（DTO），它包含客户的属性（如姓名、电子邮件）以及一个地址对象（使用了值对象 AddressVO）。

# 既然有了VO，为什么还需要DTIO？🤔️
  ### 举一个例子🌰
  某公司有一个后台服务，服务层有一个getuser的方法返回一个系统用户，包活sex、age。对于服务层，DTO的定义是：1-男性、2-女性、0-未指定。而对于展示层来说，可能需要“帅哥”代表男性，“美女”代表女性，“秘密”代表未指定。
 # 那为什么不能直接在服务层直接返回“帅哥、美女”？🙋

  对于大部门的应用来说，这不是问题。但是假设，如果允许客户可以指定风格，而不同的客户端对表现层的要求有所不同，那么，问题就来了。从设计层面分析，从职责单一原则来看，服务层仅仅只是负责业务，与具体的表现形式无关，因此，返回的DTO，不应该出现与表现形式的耦合。
 # VO与DTO的应用
 #### 在什么场景中，可以考虑VO与DTO合二为一？🙋
  - 合二为一：
1.当需求非常清晰稳定，而且客户端很明确只有一个的时候，就没有必要吧VO和DTO区分开来，这个时候VO就可以隐退，用一个DTO即可。
   - 为什么是VO隐退而不是DTO？🙋
   回到设计层面，服务层的职责依然不应与展示层耦合，DTO 对于 “性别”来说，依然不能用“帅哥美女”，这个转换应该依赖于页面的脚本（如 JavaScript）或其他机制（JSTL、EL、CSS）。
- 2.客户端可以进行定制，或者存在多个不同的客户端，如果客户端能够用某种技术（脚本或其他机制）实现转换，同样可以让 VO隐退。

#### 什么场景中，可以使用VO、DTO并存？🙋   
- 因为某种技术原因，比如某个框架提供自动把 POJO 转换为 UI 中某些 Field 时，可以考虑在实现层面定义出 VO，这个权衡完全取决于使用框架的自动转换能力带来的开发和维护效率提升与设计多一个VO所多做的事情带来的开发和维护效率的下降之间的比对。
- 如果页面出现一个“大视图”，而组成这个大视图的所有数据需要调用多个服务，返回多个DTO来组装。

# 易混点二 DTO与DO
  - 概念区别：DTO是展示层与服务层之间的数据传输对象
                      DO是现实世界各种业务角色的抽象
例如：UserInfo 和 User ，对于一个 getUser 方法来说，本质上它永远不应该返回用户的密码，因此 UserInfo 至少比 User 少一个 password 的数据。
```
// Data Transfer Object (DTO)
public class CustomerDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}

// Business Object (BO)
public class CustomerBO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }

    public void AddCustomer()
    {
        // 实现添加客户的业务逻辑
        // ...
    }

    public void UpdateCustomer()
    {
        // 实现更新客户的业务逻辑
        // ...
    }

    public CustomerDTO ToDTO()
    {
        return new CustomerDTO
        {
            Name = this.Name,
            Email = this.Email,
            Age = this.Age
        };
    }
}

// Usage
public class Program
{
    public static void Main()
    {
        // 创建一个 BO 对象
        CustomerBO customerBO = new CustomerBO();
        customerBO.Name = "John Doe";
        customerBO.Email = "john.doe@example.com";
        customerBO.Age = 30;

        // 调用 BO 的业务方法
        customerBO.AddCustomer();
        customerBO.UpdateCustomer();

        // 将 BO 对象转换为 DTO 对象
        CustomerDTO customerDTO = customerBO.ToDTO();

        // 使用 DTO 对象进行数据传输或其他操作
        Console.WriteLine($"Name: {customerDTO.Name}");
        Console.WriteLine($"Email: {customerDTO.Email}");
        Console.WriteLine($"Age: {customerDTO.Age}");
    }
}
```
CustomerDTO 是一个数据传输对象（DTO），它包含了客户的属性（如姓名、电子邮件、年龄），用于在不同层之间传输数据。

CustomerBO 是一个业务对象（BO），它具有与业务相关的属性和方法（如添加客户、更新客户），并且可以将自身转换为 DTO 对象。

# 为什么不在服务层中直接返回 DO 呢？这样可以省去 DTO 的编码和转换工作。🙋
- DO 具有一些不应该让展示层知道的数据；
- DO 具有业务方法，如果直接把 DO 传递给展示层，展示层的代码就可以绕过服务层直接调用它不应该访问的操作，对于基于 AOP 拦截服务层来进行访问控制的机制来说，这问题尤其突出，而在展示层调用DO的业务方法也会因为事物的问题，让事物难以控制。
- 从设计层面来说，展示层依赖于服务层，服务层依赖于领域层，如果把DO暴露出去，就会导致展示层直接依赖于领域层，这虽然依然单向依赖，但这种跨层依赖会导致不必要的耦合。

# 易混点三：BO和PO
  一个PO的数据结构式对应库中表的结构，表中的一条记录就是一个PO属性。
  BO是业务对象，对应的是某个具体的业务块，可以包含多个属性、对象
### 一个列子🌰
```
// Business Object (BO)
public class CustomerBO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }

    public void AddCustomer()
    {
        // 实现添加客户的业务逻辑
        // ...
    }

    public void UpdateCustomer()
    {
        // 实现更新客户的业务逻辑
        // ...
    }
}

// Persistence Object (PO)
public class CustomerPO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }

    public void Save()
    {
        // 实现保存客户到数据库的逻辑
        // ...
    }

    public void Delete()
    {
        // 实现从数据库中删除客户的逻辑
        // ...
    }
}

// Usage
public class Program
{
    public static void Main()
    {
        // 创建一个 BO 对象
        CustomerBO customerBO = new CustomerBO();
        customerBO.Name = "John Doe";
        customerBO.Email = "john.doe@example.com";
        customerBO.Age = 30;

        // 调用 BO 的业务方法
        customerBO.AddCustomer();
        customerBO.UpdateCustomer();

        // 将 BO 对象的数据映射到 PO 对象
        CustomerPO customerPO = new CustomerPO();
        customerPO.Id = 1;
        customerPO.Name = customerBO.Name;
        customerPO.Email = customerBO.Email;
        customerPO.Age = customerBO.Age;

        // 调用 PO 的持久化方法
        customerPO.Save();
        customerPO.Delete();
    }
}
```
CustomerBO 是一个表示业务对象的 BO 类，它具有一些属性（如姓名、电子邮件、年龄）和业务方法（如添加客户、更新客户）。


CustomerPO 是一个表示持久化对象的 PO 类，它具有与数据库存储相关的属性（如 ID、姓名、电子邮件、年龄）和持久化方法（如保存客户、删除客户）。

# 易混点四：BO和DTO
从用途上进行根本的区别，BO是业务对象，DTO是数据传输对象，虽然BO也可以排列组合数据，但它的功能是对内的，在提供对外接口时，BO对象中的某些属性对象可能用不到或者不方便对外暴露，那么此时DTO只需要在BO的基础上，抽取自己需要的数据，然后对外提供。
```
// Business Object (BO)
public class CustomerBO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public void AddCustomer()
    {
        // 实现添加客户的业务逻辑
        // ...
    }

    public void UpdateCustomer()
    {
        // 实现更新客户的业务逻辑
        // ...
    }

    public CustomerDTO ToDTO()
    {
        return new CustomerDTO
        {
            Id = this.Id,
            Name = this.Name,
            Email = this.Email
        };
    }
}

// Data Transfer Object (DTO)
public class CustomerDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// Usage
public class Program
{
    public static void Main()
    {
        // 创建一个 BO 对象
        CustomerBO customerBO = new CustomerBO();
        customerBO.Id = 1;
        customerBO.Name = "John Doe";
        customerBO.Email = "john.doe@example.com";

        // 调用 BO 的业务方法
        customerBO.AddCustomer();
        customerBO.UpdateCustomer();

        // 将 BO 对象转换为 DTO 对象
        CustomerDTO customerDTO = customerBO.ToDTO();

        // 使用 DTO 对象进行数据传输或其他操作
        Console.WriteLine($"Id: {customerDTO.Id}");
        Console.WriteLine($"Name: {customerDTO.Name}");
        Console.WriteLine($"Email: {customerDTO.Email}");
    }
}
```

#  命名规范：
- 数据对象：xxxPO，xxx即为数据表名。(也可DO)
- 数据传输对象：xxxDTO，xxx为业务领域相关的名称。
- 展示对象：xxxVO，xxx一般为网页名称。
- 业务对象：xxxBO，xxx是业务名称。
