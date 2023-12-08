using SimpleIOC.Model.IOC;
namespace SimpleIOC.Model
{
    [IOCServiceClass]
    public class Student
    {
        [IOCServicePro]
        public Teacher Teacher { get; set; }

        public string Studeny() {
            return "学生在学习";
        }
    }
}
