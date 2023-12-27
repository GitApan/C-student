namespace WebApplication1.Model;

public class IocFactory
{
    private IMyService _myService;

    public IocFactory(IMyService myService)
    {
        this._myService = myService;
    }

    public void Factory()
    {
        _myService.DoSomething();
    }
}