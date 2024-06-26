using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole
{
    public class Student
    {

        public int ID { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }

        public List<Note> NotesOfStudent;

        public Student(string firstName, string lastName, DateTime dateOfBirth, int id)
        {
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            ID = id;
            NotesOfStudent = new List<Note>();
        }

        public void DeleteNotesByCourse(int courseID)
        {
            NotesOfStudent.RemoveAll(x => x.CourseID == courseID);
        }

        public void AddTheNoteForStudent(Course Cours, double note, string appreciation = "")
        {
            Note noteToAdd = new Note(Cours, note, appreciation);
            NotesOfStudent.Add(noteToAdd);
        }

        public List<Note> GetStudentsNotes()
        {
            return NotesOfStudent;
        }





    }
}


