
LINQ中提供了很多集合的扩展方法，配合lambda能简化数据处理。大部分都在System。Linq命名空间中。

 ``` javascript
  		   int[] nums = new int[] { 3, 5, 3453, 33, 2, 9, 35 };
            IEnumerable<int>  result=nums.Where(i => i > 10);
            foreach (int i in result) {
                Console.WriteLine(i);
            }		
```

* Where方法：
每一项数据都会经过predicate的测试，如果针对一个元素，predicate执行的返回值为true,那么这个元素就会放到返回值中。
where参数是一个lambda表达式的格式匿名方法。
 ``` javascript
IEnumberabl<Employee> item =list.where (e => e.Age>30);	
```

* Count方法
 ``` javascript
    list.Count();
    list.Count(e=>e.Age>20);
```
* Any
是否至少有一条数据
 ``` javascript
	list.any(e=>e.Salary>30000);
```

* 获取一条数据（是否带参数的两种写法）：
Single：有且只有一条满足要求的数据
SingleOrDefaul：最多只有一条满足要求的数据
First：至少有一条，返回第一条；
FirstOrDefault：返回第一条或默认值

* 排序
Order（）对数据正序排序
OrderByDescending（）倒序排序
 ``` javascript
	list.OrderBy(e=>e.Age);
```

* 多规则排序：
   可以在Order()\OrderByDescending()后继续写ThenBy（）、ThenByDescending().
``` javascript
	var items =list.OrderBy(e=>e.Age).ThenBy(e=>e.Salary);
```


* 限制结果集，获取部分数据
 Skip(n)跳过n条数据，
 Take(n)获取n条数据
 
 ``` javascript
	var items =list.Skip(3).Task(2);   //跳过三条数据，去两条数据
```


* 聚合函数：
Max()  、Min()  、 Average（） 、Sum（）、Count（）
LINQ中所有的扩展方法几乎都是针对IEnumerable接口的，而几乎所有能返回集合的都返回IEnumerable。所以是可以把几乎所有返回方法"链式使用"的
``` javascript
	list.Where(e=>e.Age>30).Min(e=>e.Salary);
	list.Max(e=>e.Age);
	list.Where(e=>e.Age>=30).Average(e=>e.Salary);
```

* 分组
GroupBy（）方法参数是分组条件表达式，返回值为IGrouping<Tkey,TSource>类型的泛型IEnumerable,也就是每一组以一个IGrouping对象的形式返回。
IGrouping是一个继承自IEnumerable的接口，IGrouping中Key属性表示这一组的分组数据的值。

``` javascript
	IEnumerable<IGroupding<int,Employedd>>  items =list.GroupBy(e=>e.Age);
		foreach(IGrouping<int,Employee> g in items){
            Console.WriteLine(g.key);
        foreach(Employee e in g ){
                Console.WriteLine(e);
    }
}
```
* 集合转换
有一些地方需要数组类型或者List类型的变量，我们可以用ToArray()方法和ToList()分别吧IEnumerable<T>转换为数组类型和List<T>类型。
``` javascript
    IEnumerable<Employee> items1 =list.Where (e=>e.Salary >6000 );
    List<Employee> list2 =items1.ToList();
    Employee[] array2 =items1.ToArray();
```

* 链式调用
Where 、Select 、OrderBY、GroupBy 、Take 、SKip 等返回值都是 IEnumerable<T>类型，所以链式调用

* 查询语法
使用Where、OrderBy、Select等扩展方法进行数据查询的写法叫做“LINQ方法语法”

``` javascript
     var items1=from e int list
                where e.Salary > 3000
                Orderby e.Age
                Select new {e.Name,e.Age,Gender=e.Gender ? "男"，"女"};
```

