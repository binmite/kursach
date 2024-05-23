namespace курсач.Enities
{
    public class MedicalCard
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string petName;
        public string PetName
        {
            get { return petName; }
            set { petName = value; }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private string sex;
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        private string kindOfPet;
        public string KindOfPet
        {
            get { return kindOfPet; }
            set { kindOfPet = value; }
        }

        private bool hasSurgeries;
        public bool HasSurgeries
        {
            get { return hasSurgeries; }
            set { hasSurgeries = value; }
        }

        public override string ToString()
        {
            return $"ID = {Id}\nИмя: {PetName}\nВозраст: {Age}\nВид питомца: {KindOfPet}\nПол питомца: {Sex}\nПроводились ли хирургические операции: {HasSurgeries}\n";
        }
    }

    public class SortByName : IComparer<MedicalCard>
    {
        public int Compare(MedicalCard o1, MedicalCard o2)
        {
            return o1.PetName.CompareTo(o2.PetName);
        }
    }

    public class SortByAge : IComparer<MedicalCard>
    {
        public int Compare(MedicalCard o1, MedicalCard o2)
        {
            return o1.Age.CompareTo(o2.Age);
        }
    }
}
