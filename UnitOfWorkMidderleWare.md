# Mediator中的中间件类：
  - 中间件使用静态类
  - 在静态类中添加静态方法，用UseXxx命名约定 
```
//要使用一个静态类
public static class UnitOfWorkMidderleWare
{
//静态方法 传入管道的配置信息，传入为空的工作单元
    public static void UseUnitOfWordk<TContext>(this IPipeConfigurator<TContext> configurator, IUnitOfWork unitOfWork = null) 
        where TContext : IContext <IMessage>   //必须实现 IContext<IMessage> 接口
    {
    //  如果工作单元为空 同时 当前的依赖范围对象 为空
        if (unitOfWork == null && configurator.DependencyScope == null) 
        {     
      // 抛出异常
            throw new DependencyScopeNotConfiguredException($"{nameof(unitOfWork)} is not provided and IDependencyScope is not configured, Please ensure {nameof(unitOfWork)} is registered properly if you are using IoC container, otherwise please pass {nameof(unitOfWork)} as parameter");
        }
      //不满足条件时 unitOfWork和IUnitOfWork建立依赖关系
        unitOfWork ??= configurator.DependencyScope.Resolve<IUnitOfWork>();
     //是将一个 UnitOfWorkSpecification<TContext> 实例添加到管道配置器的操作。
        configurator.AddPipeSpecification(new UnitOfWorkSpecification<TContext>(unitOfWork));
    }
}
```

自定义类 实现管道，实现Mediator的中间件
```
//自定义类实现了管道规范
public class UnitOfWorkSpecification<TContext> : IPipeSpecification<TContext> where TContext : IContext<IMessage>
{
//注入IUnitOfWork
    private readonly IUnitOfWork _unitOfWork;
    
    public UnitOfWorkSpecification(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    //应该执行
    public bool ShouldExecute(TContext context, CancellationToken cancellationToken)
    {
        return true;
    }
  //前执行的
    public Task BeforeExecute(TContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
//执行的
    public Task Execute(TContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
//后执行的
    public async Task AfterExecute(TContext context, CancellationToken cancellationToken)
    {
    //如果ShouldSaveChanges = true
        if (_unitOfWork.ShouldSaveChanges) 
        {
      //异步保存
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
          //设置为false
            _unitOfWork.ShouldSaveChanges = false;
        }
    }
//捕获异常
    public Task OnException(Exception ex, TContext context) {
        ExceptionDispatchInfo.Capture(ex).Throw(); 
        throw ex;
    }
}
```
接口类
 ```
public interface IUnitOfWork
{
   //异步保存
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    //保存
    bool ShouldSaveChanges { get; set; }
}
···
