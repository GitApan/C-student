using SimpleIOC.Model.IOC;
namespace SimpleIOC.Model
{
    [IOCServiceClass]
    public class Teacher
    {
        public string Teach() {
            return "教书";
        }
    }
}
