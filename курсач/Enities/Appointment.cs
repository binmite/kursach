using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсач.Enities
{
    public class Appointment
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string doctorName;
        public string DoctorName
        {
            get { return doctorName; }
            set { doctorName = value; }
        }

        private DateOnly date;
        public DateOnly Date
        {
            get { return date; }
            set { date = value; }
        }

        private TimeOnly time;
        public TimeOnly Time
        {
            get { return time; }
            set { time = value; }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public override string ToString()
        {
            return $"ID = {Id}\nДоктора: {DoctorName}\nДата: {Date}\nВремя: {Time}\n";
        }
    }
}

