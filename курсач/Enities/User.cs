using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсач.Enities
{
    public class User
    {
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
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
    }
    public class SortUserByName : IComparer<User>
    {
        public int Compare(User o1, User o2)
        {
            return o1.FullName.CompareTo(o2.FullName);
        }
    }
}
