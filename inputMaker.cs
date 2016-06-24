using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;


namespace Inputer
{
    class Inputer
    {
        static void Main(string[] args)
        {
            int i = 0,n = Int32.Parse(args[0]);
            string path = @"C:\Users\Brian F Cook\Documents\Organ\input.txt";
            string f,l,o,b;
            GetRandomNames gen = new GetRandomNames();
            DateTime date = new DateTime(1978,8,4,10,45,00);
            Patient p = new Patient(10,"Brian ","Cook ","Liver","B+");
            
            Random r = new Random(); 
            StreamWriter sw;
            
            using (sw = File.CreateText(path))
            {
                while (i < n)
                {
                    sw.WriteLine(date.ToString());
                    sw.WriteLine(p.getString());
                    
                    date = new DateTime(r.Next(1900,2000),r.Next(1,12),r.Next(1,28),r.Next(24),r.Next(60),0);
                    f = gen.getName(r);
                    l = gen.getName(r);
                    o = gen.getOrgan(r);
                    b = gen.getBlood(r);
                    p = new Patient(i,f,l,o,b);
                    i++;
                }
            }
        }
    }
    
    class GetRandomNames
    {
        private string path;
        //private StreamReader sr;
        
        public GetRandomNames(){
            path = @"C:\Users\Brian F Cook\Documents\Organ\yob1880.txt"; 
            //sr = File.OpenText(path);
        }
        
        public string getName(Random r){
            int i = 0, n = r.Next(300);
            string name;
            string[] readIn;
            
            StreamReader sr = File.OpenText(path);
            using (sr = File.OpenText(path))
            {
                while(i<n){
                    sr.ReadLine();
                    i++;
                }
                name = sr.ReadLine();
                readIn = name.Split(',');
            }
            //name = name.Remove(name.Length-1);
            
            return readIn[0] + " ";
        }
        
        public string getOrgan(Random r){
            int n = r.Next(3);
            string o;
            switch(n){
                case 0: o = "Heart"; break;
                case 1: o = "Liver"; break;
                case 2: o = "Kidney"; break;
                case 3: o = "Lung"; break;
                case 4: o = "Pancreas"; break;
                case 5: o = "Intestine"; break;
                case 6: o = "Stomach"; break;
                case 7: o = "Cornea"; break;
                case 8: o = "Bone Marrow"; break;
                default: o = "Valve"; break;
            }
            
            return o;
        }
        
        public string getBlood(Random r){
            int n = r.Next(4);
            string b;
            switch(n){
                case 0: b = "A"; break;
                case 1: b = "B"; break;
                case 2: b = "AB"; break;
                case 3: b = "O"; break;
                case 4: b = "A-"; break;
                case 5: b = "B-"; break;
                case 6: b = "AB-"; break;
                case 7: b = "O-"; break;
                case 8: b = "A+"; break;
                case 9: b = "B+"; break;
                case 10: b = "AB+"; break;
                case 11: b = "O+"; break;
                default: b = "O"; break;
            }
            
            return b;
        }
    }
    
    //---------------------------------------------------------------------------
    //Classes
    //---------------------------------------------------------------------------
    
    //--------------------Organ--------------------
    class Organ
    {
        private int _id;
        private string _organName;
        private string _bloodType;
    
        public Organ(int i,string o,string b)
        {
            _id = i;
            _organName = o;
            _bloodType = b;
        }
        
        public int ID{
            get{
                return _id;
            }set{
                _id = value;
            }
        }
    
        public string OrganName{
            get{
                return _organName;
            }set{
                _organName = value;
            }
        }
    
        public string BloodType{
            get{
                return _bloodType;
            }set{
                _bloodType = value;
            }
        }
        
        //both compare methods use CompareOrdinal 
        public int Compare(ref Organ p){
            return String.CompareOrdinal(p._organName +" "+ p._bloodType,_organName +" "+ _bloodType);
        }
    
        public string getString(){
            return _organName + " " + _bloodType;
        }
    }
    
    //--------------------Patient--------------------
    class Patient
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private Organ _organ;
    
        public Patient(int i, string f,string l,string o,string b)
        {
            _id = i;
            _firstName = f;
            _lastName = l;
            _organ = new Organ(i,o,b);
        }
        
        public int ID{
            get{
                return _id;
            }set{
                _id = value;
            }
        }
    
        public string FirstName{
            get{
                return _firstName;
            }set{
                _firstName = value;
            }
        }
    
        public string LastName{
            get{
                return _lastName;
            }set{
                _lastName = value;
            }
        }
        
        public Organ Organ{
            get{
                return _organ;
            } set {
                _organ = value;
            }
        }
        
        public int Compare(int i){
            return (i - _id);
        }

        public int Compare(ref Patient p){
            return String.CompareOrdinal(p._firstName +" "+ p._lastName,_firstName +" "+ _lastName);
        }
        
        public int Compare(object obj){
            if (obj == null) return 1;

            Patient p = obj as Patient;
            if (p != null) 
                return this.Compare(p.ID);
            else
               throw new ArgumentException("Object is not a Patient");
        }
    
        public string getString(){
            return _firstName + _lastName + _organ.getString();
        }
    }
}