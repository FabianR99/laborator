using System;
using System.Collections.Generic;
 namespace Lab02
 {
     public class StudentRepo
     {
        public List<Student> student = new List<Student>()
        { 
            new Student()
            {
                uid = 1,
                nume = "Halapi",
                prenume ="Edward",
                facultatea = "AC",
                an = 4
            }
        };
        public List<Student> Students
        {
            get
            {
                return student;
            }
        }
     }
 }
