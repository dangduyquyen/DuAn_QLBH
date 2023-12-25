namespace SV20T1080048.Web.Models
{
    public class PersonDAL
    {
        public List<Person> List()
        {
            List<Person> list = new List<Person>(); // ten bien su dung quy tac: camelCase
            list.Add(new Person()
            {
                PersonId = 1, 
                Name = "Trần Nguyên Phong",
                Address = "77 Nguyễn Huệ",
                Email = "tnphong@gmail.com"
            });
            list.Add(new Person()
            {
                PersonId = 2,
                Name = "Đặng Duy Quyền",
                Address = "Thuận Hòa, Hương Phong",
                Email = "ddquyen@gmail.com"
            });
            list.Add(new Person()
            {
                PersonId = 3,
                Name = "Nguyễn Quốc Trung",
                Address = "Kiệt 2, Trần phú",
                Email = "nqtrung@gmail.com"
            });

            return list;
        }
    }
}
