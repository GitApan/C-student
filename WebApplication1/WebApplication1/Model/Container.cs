namespace WebApplication1.Model;

//注册和解析接口与具体实现之间的映射关系，并通过反射动态创建对象。
public class Container
{
   private Dictionary<Type, Type> Dictionary = new Dictionary<Type, Type>();
   public void Register<TInterface,TImplementaion>(){  //映射

      Dictionary[typeof(TInterface)] = typeof(TImplementaion);
   }

   public TInterface Resolve<TInterface>()   //通过反射获取接口
   {
      Type type = Dictionary[typeof(TInterface)];
      var instance = Activator.CreateInstance(type);
      return (TInterface)instance;
   }
}