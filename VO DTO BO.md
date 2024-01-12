![image.png](https://upload-images.jianshu.io/upload_images/29491970-27ab6d998d8992cc.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
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

![image.png](https://upload-images.jianshu.io/upload_images/29491970-a7920e303d741701.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

![image.png](https://upload-images.jianshu.io/upload_images/29491970-df2d60bef9e5dd01.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

# VO  
  展示用的数据，给人看的数据。把某个指定页面的所有数据封装起来。

- 特点
1. 不可变性：一旦创建不可以被修改
2.根据值进行相等性的比较：
3.无标识性：没有唯一的标识符

# VO和DTO的区别
  - 字段不一样，VO会删减一些字段
  - 值不一样 VO会根据DTO的值进行展示业务的解释

DTO：
![image.png](https://upload-images.jianshu.io/upload_images/29491970-3441a495b2739c01.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
VO：
![image.png](https://upload-images.jianshu.io/upload_images/29491970-17f4a4b141d02002.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

![image.png](https://upload-images.jianshu.io/upload_images/29491970-50595954d1252297.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

# PO
PO持久层对象，由一组属性和属性的get和set方法组成。对应数据库中某个表的记录，PO层不能包含对数据库的操作。

# BO 
BO通常指的是业务对象，表示业务概念和业务逻辑的实体。BO是对现实世界中业务实体的抽象和建模，封装了与业务相关的数据和操作。
PO是一条交易记录，BO是一个人全部的交易记录集合对象。
BO是一个业务对象，一类业务就会对应一个BO，数量上没有限制，而且BO会有很多业务操作，除了get、set方法以外，BO会有很多根据自身数据进行计算的方法。

#####  BO功能

-  业务数据：BO层包含业务对象的属性和数据，也用于描述业务实体的特征和状态。属性可以是简单的值类型，也可以是负责的业务对象。
- 业务操作： 定义了对业务对象进行的操作和行为（创建、修改、删除），与业务逻辑相关。
- 业务验证： BO层负责验证业务对象的数据的合法性和完整性
- 业务逻辑：BO层包含了与业务实体相关的业务逻辑和规则。

BO层的设计目标是将业务逻辑和业务数据集中在一起，使其独立于应用程序的其他层（如表示层、数据访问层等）。这样可以实现业务逻辑的重用、可测试性和可维护性。
![image.png](https://upload-images.jianshu.io/upload_images/29491970-24f4094dac8f2f95.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

