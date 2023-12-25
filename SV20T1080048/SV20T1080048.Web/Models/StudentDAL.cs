namespace SV20T1080048.Web.Models
{
    public class Student
    {
        public string StudentId { get; set; }

        public string StudentName { get; set; }


    }
    public class StudentDAL
    {
        public List<Student> List()
        {
            List<Student> students = new List<Student>();


            students.Add(new Student()
            {
                StudentId = "20t1087004",
                StudentName = "Nguyen Van A"
            });
            students.Add(new Student()
            {
                StudentId = "20t1087002",
                StudentName = "Nguyen Van B "
            });
            return students;
        }
    }
}
