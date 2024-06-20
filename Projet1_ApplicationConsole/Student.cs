using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole
{
    internal class Student
    {

        public int ID { get; }
        public string FirstName { get; }
        public string LastName { get;}
        public DateTime DateOfBirth { get;  }

        private List<Note> NotesOfStudent = new List<Note>();

        public Student(string lastName, string firstName, DateTime dateOfBirth,int id)
        {
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            ID = id;
            
        }

        public void DisplayStudentInformation()
        {
           
        }

        public void AddTheNoteForStudent(Course course, double note, string appreciation="")
        { 
            Note noteToAdd = new Note(this, course, note, appreciation);
            NotesOfStudent.Add(noteToAdd);

        }

        public List<Note> GetStudentsNotes()
        { 
            return NotesOfStudent;
        }




       
    }
}


