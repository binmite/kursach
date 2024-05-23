using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсач.Enities
{
    public class User
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private List<Appointment> appointments;
        public List<Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }
    }
    public class SortUserByName : IComparer<User>
    {
        public int Compare(User o1, User o2)
        {
            return o1.FullName.CompareTo(o2.FullName);
        }
    }

    public class SortByLogin : IComparer<User>
    {
        public int Compare(User o1, User o2)
        {
            return o1.Login.CompareTo(o2.Login);
        }
    }

}
