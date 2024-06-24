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

        private List<Note> NotesOfStudent = new List<Note>();

        public Student(string firstName, string lastName, DateTime dateOfBirth, int id)
        {
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            ID = id;
        }

        public void DeleteNotesByCourse(Course course)
        {
            NotesOfStudent.RemoveAll(x => x.Course == course);
        }

        public void AddTheNoteForStudent(Course course, double note, string appreciation = "")
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


