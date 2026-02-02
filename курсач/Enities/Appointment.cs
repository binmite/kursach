using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсач.Enities
{
    public class Appointment
    {
        private int appointmentId;
        public int AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; }
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

        private int medicalCardId;
        public int MedicalCardId
        {
            get { return medicalCardId; }
            set { medicalCardId = value; }
        }

        public override string ToString()
        {
            return $"ID = {appointmentId}\nДоктор: {DoctorName}\nДата: {Date}\nВремя: {Time}\nID питомца к которому встреча: {MedicalCardId}\n";
        }
    }
}