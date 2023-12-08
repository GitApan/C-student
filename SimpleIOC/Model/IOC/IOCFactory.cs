using System.Reflection;

namespace SimpleIOC.Model.IOC
{
    public class IOCFactory
    {
      private Dictionary<string,object> objDictionary =new Dictionary<string,object>();
      private Dictionary<string,Type> typeDictionary =new Dictionary<string,Type>();

        //加载程序集
        public void Load(string LoadName) {
            Assembly ass = Assembly.Load(LoadName);
           Type[] types= ass.GetTypes();
            foreach (Type t in types)
            {
                IOCServiceClass iOCServiceClass = (IOCServiceClass)t.GetCustomAttribute(typeof(IOCServiceClass));
                if (iOCServiceClass!=null) {
                    typeDictionary.Add(t.Name,t);
                }
            }

        }
        public object Getobj(string objName) {
            Type type = typeDictionary[objName];

            object objValue=Activator.CreateInstance(type);

            //获取属性值
            PropertyInfo[] infos = type.GetProperties();

            foreach (PropertyInfo pi in infos)
            {
                IOCServicePro iOCServicePro = (IOCServicePro)pi.GetCustomAttribute(typeof(IOCServicePro));
                if (iOCServicePro != null) {
                    pi.SetValue(objValue, Getobj(pi.PropertyType.Name));
                
                }

            }
            objDictionary.Add(objName, objValue);
            return objValue;




        }

      
        
        

    }
}
