AutoMapper 是一个对象-对象映射器。对象-对象映射的工作原理是将一种类型的输入对象转换为不同类型的输出对象。
安装AutoMapper.Contrib.Autofac.DependencyInjection
```
public class TestA
    {
        public int A { get; set; }
        public string B { get; set; }
  }
    public class TestB
    {
        public int A { get; set; }
        public string B { get; set; }
    }
```
映射配置：把A映射到B中
```
 MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
              cfg.CreateMap<TestA, TestB>();
            });
```
创建映射器：
```
 IMapper mapper = configuration.CreateMapper();
```
使用 .Map() 方法将 TestA 中字段的值复制到 TestB 中。
```
 TestA a = new TestA();
 TestB b = mapper.Map<TestB>(a);
```
.ForMember() 方法用于创建一个字段的映射逻辑，可以类别成员做操作，并确定目标的类别成员，以及该成员对应的操作，有两个表达式 ({表达式1} , {表达式2})，其中表达式1代表 TestB 映射的字段；表达式2代表这个字段的值从何处来。

表达式2有常用几种映射来源：
-MapFrom() 从 TestA 取得；
-AllowNull() 设置空值；
-Condition() 有条件地映射；
-ConvertUsing() 类型转换；
```
 MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                // TestA -> TestB
                cfg.CreateMap<TestA, TestB>()
                // 左边是 TestB 的字段，右边是为字段赋值的逻辑
                .ForMember(b => b.A, cf => cf.MapFrom(a => a.A))
                .ForMember(b => b.B, cf => cf.MapFrom(a => a.B))
                .ForMember(b => b.Id, cf => cf.MapFrom(a => Guid.Parse(a.Id)));
            });
```
Profile 配置
除了 MapperConfiguration 外，还可以使用继承 Profile 的方式定义映射配置
```
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            base.CreateMap<TestA, TestB>().ForMember(... ...);
        }
    }
```
ReverseMap配置反向映射
```
CreateMap<TestA,TestB>().ReverseMap();
```
映射继承 ：Include与IncludeBase
Inculde:当一个类包含另一个类作为其属性
```
   CreateMap<BaseEntity, BaseDto>()
   .Include<DerivedEntity, DerivedDto>()
   .ForMember(dest => dest.SomeMember, opt => opt.MapFrom(src => src.OtherMember));
    CreateMap<DerivedEntity, DerivedDto>();
```
IncludeBase:一个类继承自另一个类
```
   CreateMap<BaseEntity, BaseDto>()
   .ForMember(dest => dest.SomeMember, opt => opt.MapFrom(src => src.OtherMember));
    CreateMap<DerivedEntity, DerivedDto>()
    .IncludeBase<BaseEntity, BaseDto>();
```
空替换。NullSubstitute()可以替换数据源中为Null的值
```
    var config = new MapperConfiguration(cfg => cfg.CreateMap<Source, Dest>()
    .ForMember(destination => destination.Value, opt => opt.NullSubstitute("Other Value")));
```
价值转换器
     从一个类型映射A到另一个类型B时，都会使用该类型转换器。
```
类型转换器=Func<TSource, TDestination, TDestination>
值解析器 =Func<TSource, TDestination, TDestinationMember>
成员值解析器=Func<TSource, TDestination, TSourceMember, TDestinationMember>
值转换器 =Func<TSourceMember, TDestinationMember>
```
