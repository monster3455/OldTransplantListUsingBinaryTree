using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

using System.Diagnostics;

namespace Project
{
    class Project
    {
        static void Main(string[] args)
        {
            /*Stopwatch timer = new Stopwatch();
            timer.Start();*/
            
            int i = 1;
            DateTime dateTime;
            Patient patient;
            Node node;
            BinaryTree organList;
            PatientList patientList;
            string path = @"C:\Users\Brian F Cook\Documents\Organ\input.txt";
            string[] readIn;
            StreamReader sr;
            
            using (sr = File.OpenText(path))
            {
                string s = "";
                if((s = sr.ReadLine()) == null){
                    Console.Write("Input is empty\n");
                    return;
                }
                
                dateTime = DateTime.Parse(s);
                s = sr.ReadLine();
                readIn = s.Split(' ');
                patient = new Patient(i,readIn[0],readIn[1],readIn[2],readIn[3]);
                node = new Node(dateTime,patient.Organ);
                    
                organList = new BinaryTree(node);
                patientList = new PatientList(patient);
                i++;
                
                while ((s = sr.ReadLine()) != null)
                {
                    //Console.Write(i + " ");
                    
                    dateTime = DateTime.Parse(s);
                    //Console.Write(i + " ");
                    s = sr.ReadLine();
                    //Console.Write(i + " ");
                    readIn = s.Split(' ');
                    //Console.Write(i + "a");
                    //Console.WriteLine(readIn[0] + " " + readIn[1] + " " + readIn[2] + " " + readIn[3]);
                    //Console.Write(i + "b");
                    patient = new Patient(i,readIn[0],readIn[1],readIn[2],readIn[3]);
                    //Console.Write(i + " ");
                    node = new Node(dateTime,patient.Organ);
                    //Console.Write(i + " ");
                    organList.addNode(node,organList.Root,null);
                    //Console.Write(i + " ");
                    patientList.addToList(patient);
                    i++;
                }
            }
            
            //                          deleteNode test
            
            organList.print();            
                             
            Organ toFind = new Organ(-1,"Liver","B");
            
            Node found;
            
            found = organList.searchFor(toFind,1,organList.Root,null);
            Console.WriteLine();
            if(found != null){
                Console.WriteLine(found.getString());
                Console.WriteLine();
            } else {
                Console.WriteLine("Organ not found");
                Console.WriteLine();
            }
            
            organList.print();
            /*
            int command = 0;
            string reading;
            
            while(command != -1){
                Console.WriteLine("Commands");
                Console.WriteLine("1 - add");
                Console.WriteLine("2 - retreive");
                Console.WriteLine("3 - print");
                Console.WriteLine("-1 - exit");
                Console.WriteLine("Enter Command: ");
                command = Int32.Parse(Console.ReadLine());
                
                if(command == 1){
                    Console.WriteLine("Enter Format");
                    Console.WriteLine("MM/DD/YYYY HH:MM:SS AM");
                    Console.WriteLine("FIRST LAST ORGAN BLOOD");
                    
                    dateTime = DateTime.Parse(Console.ReadLine());
                    
                    reading = Console.ReadLine();
                    readIn = reading.Split(' ');
                    patient = new Patient(i,readIn[0],readIn[1],readIn[2],readIn[3]);
                    
                    node = new Node(dateTime,patient.Organ);
                    organList.addNode(node,organList.Root,null);
                    patientList.addToList(patient);
                    i++;
                } else if (command == 2){
                    Console.WriteLine("Enter Format");
                    Console.WriteLine("ORGAN BLOOD");
                    
                    reading = Console.ReadLine();
                    readIn = reading.Split(' ');
                    
                    toFind = new Organ(-1,readIn[0],readIn[1]);
                    found = organList.searchFor(toFind,1,organList.Root,null);
                    
                    if(found != null){
                        Console.WriteLine(found.getString());
                    } else {
                        Console.WriteLine("No organ matching discription");
                    }
                } else if (command == 3){
                    organList.print();
                }
            }*/
            
            //patientList.print();
            //Console.WriteLine(i);
            //Console.WriteLine(timer.ElapsedMilliseconds);
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
        public int Compare(Organ p){
            return String.CompareOrdinal(_organName +" "+ _bloodType,p._organName +" "+ p._bloodType);
        }
    
        public string getString(){
            return _organName + " " + _bloodType;
        }
    }
    
    //--------------------Patient--------------------
    class Patient : IComparable
    {
        protected int _id;
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

        public int Compare(Patient p){
            return String.CompareOrdinal(_firstName +" "+ _lastName,p._firstName +" "+ p._lastName);
        }

        public int CompareTo(object obj){
            if (obj == null) return 1;

            Patient p = obj as Patient;
            if (p != null) 
                return this.ID.CompareTo(p.ID);
            else
               throw new ArgumentException("Object is not a Patient");
        }
    
        public string getString(){
            return _id + ": " +_firstName + " " + _lastName;
        }
    }
    
    //--------------------ListNode--------------------
    class Node
    {
        private DateTime _date;
        private Organ _organ;
        private int _leftHeight;
        private int _rightHeight;
        public Node _leftLeaf;
        public Node _rightLeaf;
    
        public Node(DateTime d, Organ p)
        {
            _date = d;
            _organ = p;
            _leftHeight = 0;
            _rightHeight = 0;
            _leftLeaf = null;
            _rightLeaf = null;
        }
        
        public DateTime Date{
            get{
                return _date;
            } set {
                _date = value;
            }
        }
        
        public Organ Organ{
            get{
                return _organ;
            } set {
                _organ = value;
            }
        }
        
        public int LeftHeight{
            get{
                return _leftHeight;
            } set {
                _leftHeight = value;
            }
        }
        
        public int RightHeight{
            get{
                return _rightHeight;
            } set {
                _rightHeight = value;
            }
        }
        
        public Node LeftLeaf{
            get{
                return _leftLeaf;
            } set {
                _leftLeaf = value;
            }
        }
        
        public Node RightLeaf{
            get{
                return _rightLeaf;
            } set {
                _rightLeaf = value;
            }
        }
        
        public bool isLeaf(){
            return (_leftLeaf == null && _rightLeaf == null);
        }
        
        public bool isLeaf(Node n){
            return (n._leftLeaf == null && n._rightLeaf == null);
        }
        
        public int Compare(Node n){
            int i;
            if((i = _organ.Compare(n._organ)) != 0){
                return i;
            } else if((i = _date.CompareTo(n.Date)) != 0){
                return i;
            }
            return 0;
        }
        
        public string getString(){
            return _date.ToString() + "\n" + _organ.getString() + " " + _leftHeight + " " + _rightHeight;
        }
    }
    
    //--------------------BinaryTree--------------------
    class BinaryTree
    {
        private Node _root;
        
        public BinaryTree(Node r)
        {
            _root = r;
        }
        
        public Node Root{
            get{
                return _root;
            } set {
                _root = value;
            }
        }
        
        //n is the node that is being added
        public int addNode(Node n, Node temp, Node parent) {
            int height = 1;
            if (temp == null && _root == null)
            {
                _root = n;
                return 0;
            }
            //Console.WriteLine("L");
            
            if(temp.Compare(n) > 0){
                if(temp.LeftLeaf == null){
                    temp.LeftLeaf = n;
                    temp.LeftHeight = height;
                } else {
                    height = addNode(n, temp.LeftLeaf, temp) + 1;
                    //Console.WriteLine(height);
                    if(temp.LeftHeight < height)
                        temp.LeftHeight = height;
                }
            } else {
                if(temp.RightLeaf == null){
                    temp.RightLeaf = n;
                    temp.RightHeight = height;
                } else {
                    height = addNode(n, temp.RightLeaf, temp) + 1;
                    if(temp.RightHeight < height)
                        temp.RightHeight = height;
                }
            }
            
            if((temp.LeftHeight - temp.RightHeight) > 1 || (temp.LeftHeight - temp.RightHeight) < -1 ){
                //Console.WriteLine("Trim" + temp.LeftHeight + " " + temp.RightHeight);
                if(parent == null){
                    _root = Trimming(temp, temp.LeftHeight - temp.RightHeight);
                } else {
                    if(temp.Compare(parent.LeftLeaf) == 0){
                        parent.LeftLeaf = Trimming(temp, temp.LeftHeight - temp.RightHeight);
                        height -= 1;
                    } else if (temp.Compare(parent.RightLeaf) == 0) {
                        parent.RightLeaf = Trimming(temp, temp.LeftHeight - temp.RightHeight);
                        height -= 1;
                    } else {
                        Console.WriteLine("Error in comparison for Trimming");
                    }
                }
            }
            
            return height;
        }
        
        //used to keep tree trimmed (all branches relativly same height)
        private Node Trimming(Node n, int i){
            Node temp;
            int height;
            
            if(i > 1){
                //Console.WriteLine("Left");
                if(n.LeftLeaf.LeftLeaf != null){
                    temp = n.LeftLeaf;
                
                    n.LeftLeaf = temp.RightLeaf;
                    height = n.LeftHeight;
                    n.LeftHeight = temp.RightHeight;
                
                    temp.RightLeaf = n;
                    temp.RightHeight = height-1;
                } else {//Exception case, only occurs at bottom branches
                    temp = n.LeftLeaf.RightLeaf;
                    
                    n.LeftLeaf.RightLeaf = null;
                    n.LeftLeaf.RightHeight = 0;
                    
                    temp.LeftLeaf = n.LeftLeaf;
                    temp.LeftHeight = 1;
                    
                    n.LeftLeaf = null;
                    height = n.LeftHeight;
                    n.LeftHeight = 0;
                    
                    temp.RightLeaf = n;
                    temp.RightHeight = height-1;
                }
                
            } else if (i < -1){
                //Console.WriteLine("Right");
                if(n.RightLeaf.RightLeaf != null){
                    temp = n.RightLeaf;
                
                    n.RightLeaf = temp.LeftLeaf;
                    height = n.RightHeight;
                    n.RightHeight = temp.LeftHeight;
                
                    temp.LeftLeaf = n;
                    temp.LeftHeight = height-1;
                } else {//Exception case, only occurs at bottom branches
                    temp = n.RightLeaf.LeftLeaf;
                    
                    n.RightLeaf.LeftLeaf = null;
                    n.RightLeaf.LeftHeight = 0;
                    
                    temp.RightLeaf = n.RightLeaf;
                    temp.RightHeight = 1;
                    
                    n.RightLeaf = null;
                    height = n.RightHeight;
                    n.RightHeight = 0;
                
                    temp.LeftLeaf = n;
                    temp.LeftHeight = height-1;
                }
                
            } else {
                //Console.WriteLine("Same");
                return n;
            }
            
            return temp;
        }
        
        //o is what is being searched for
        //d is if it to be deleted
        //n is for recursion
        public Node searchFor(Organ o,int d, Node n, Node p){
            
            if(n == null)
                return null;
            /*Console.WriteLine();
            Console.WriteLine(n.getString());*/
            
            int i = n.Organ.Compare(o);
            Node temp = null;
            if(i > 0){
                Console.WriteLine("1");
                if(d == 1){
                    temp = searchFor(o, d, n.LeftLeaf,n);
                    if(temp != null)
                        n.LeftHeight -= 1;
                    return temp;
                }
                
                return searchFor(o, d, n.LeftLeaf,n);
            } else if (i < 0){
                Console.WriteLine("2");
                if(d == 1){
                    temp = searchFor(o, d, n.RightLeaf,n);
                    if(temp != null)
                        n.RightHeight -= 1;
                    return temp;
                }
                
                return searchFor(o, d, n.RightLeaf,n);
            } else {
                Console.WriteLine("3");
                if((temp = searchFor(o, d, n.LeftLeaf,n)) == null){
                    temp = n;
                    if(d == 1){
                        deleteNode(temp,p);
                    }
                }
                if(d == 1){
                    n.LeftHeight -= 1;
                }
            }
            
            return temp;
        }
        
        private void deleteNode(Node n, Node p){
            Node temp,pTemp = n;
            
            if(n.isLeaf()){
                Console.WriteLine("4");
                 temp = null;
            } else if (n.LeftHeight > n.RightHeight){
                temp = pTemp.LeftLeaf;
                
                while(temp.RightLeaf != null){
                    pTemp = temp;
                    temp.RightHeight = temp.RightHeight - 1;
                    temp = temp.RightLeaf;
                }
                
                if(pTemp != n){
                    pTemp.RightLeaf = temp.LeftLeaf;
                    pTemp.RightHeight = temp.LeftHeight;
                }
                
                if(temp != n.LeftLeaf){
                    temp.LeftLeaf = n.LeftLeaf;
                } else {
                    temp.LeftLeaf = null;
                }
                    
                temp.LeftHeight = n.LeftHeight - 1;
                temp.RightLeaf = n.RightLeaf;
                temp.RightHeight = n.RightHeight;
            } else {
                temp = pTemp.RightLeaf;
                
                while(temp.LeftLeaf != null){
                    pTemp = temp;
                    temp.LeftHeight = temp.LeftHeight - 1;
                    temp = temp.LeftLeaf;
                }
                
                if(pTemp != n){
                    pTemp.LeftLeaf = temp.RightLeaf;
                    pTemp.LeftHeight = temp.RightHeight;
                }
                
                temp.LeftLeaf = n.LeftLeaf;
                temp.LeftHeight = n.LeftHeight;
                
                if(temp != n.RightLeaf){
                    temp.RightLeaf = n.RightLeaf;
                } else {
                    temp.RightLeaf = null;
                }
                temp.RightHeight = n.RightHeight - 1;
            }
            
            if(p != null){
                if(p.LeftLeaf == n){
                    p.LeftLeaf = temp;
                } else if (p.RightLeaf == n) {
                    p.RightLeaf = temp;
                }
            } else {
                _root = temp;
            }
            
        }
        
        public void print(){
            if(_root != null){
                print(_root);
            } else {
                Console.WriteLine("Empty");
            }
        }
        
        public void print(Node n){
            
            if(n.LeftLeaf != null){
                print(n.LeftLeaf);
            }
            
            Console.WriteLine(n.getString());
            
            if(n.RightLeaf != null){
                print(n.RightLeaf);

            }
        }
    }
    
    //--------------------PatientList--------------------
    class PatientList
    {
        private Patient[] _patients;
        private int _size;
        
        public PatientList(Patient p){
            _patients = new Patient[1000000];
            _patients[0] = p;
            _size = 1;
        }
        
        public void addToList(Patient p){
            
            int i = _size;
            _patients[i] = p;
            _size++;
            
            while(i > 0){
                if(_patients[i] != null){
                    if(_patients[i].ID < _patients[i-1].ID){
                        Swap(i-1,i);
                    }
                }
                i--;
            }
        }
        
        /*public void Sort(int lo, int hi){
            int i = hi - 1;
            while(lo < hi){
                _patients[i];
            }
        }*/
        
        public void Swap(int i, int j){
            Patient n = _patients[i];
            _patients[i] = _patients[j];
            _patients[j] = n;
        }
        
        public int findInList(int id){
            int i = _size/2,j = 0;
            Patient p;
            
            while(i != 0){
                p = _patients[i + j];
                
                if(p.ID == id){
                    return i+j;
                } else if(id < p.ID) {
                    i /= 2;
                } else {
                    j = j + i;
                    i /= 2;
                }
            }
            
            return -1;
        }
        
        public void print(){
            int i;
            for(i=0;i<_size;i++){
                Console.WriteLine(_patients[i].getString());
            }
        }
        
    }
}