using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projet1_ApplicationConsole
{
    internal class UserTools
    {
        public List<Student> StudentsList { get; }
        public List<Course> CoursesList { get; }

        public UserTools(List<Student> studentsList, List<Course> coursesList)
        {
            this.StudentsList = studentsList;
            this.CoursesList = coursesList;
        }

        public void UserAddTheStudent()
        {
            ConsoleKey key = ConsoleKey.Add;
            Console.Clear();
            string messageAddContinue = "Adding a new student. Press any key to continue or 'Escape' to exit";

            Console.WriteLine(messageAddContinue);
            key = Console.ReadKey(true).Key;

            while (key != ConsoleKey.Escape)
            {

                Student newStudent = CreateStudent();
                Console.WriteLine("New student by ID {0} was added! ", newStudent.ID);
                Console.WriteLine("  ");
                Console.WriteLine(messageAddContinue);
                key = Console.ReadKey(true).Key;

            }
        }

        private DateTime GetingBirthDateInDateTime()
        {
            DateTime dateOfBirth;
            string input;
            do
            {
                Console.Write("Birthday (in format dd.mm.yyy): ");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dateOfBirth));

            return dateOfBirth;
        }

        public Student CreateStudent()
        {
            Console.Write("First name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last name: ");
            string lastName = Console.ReadLine();

            DateTime dateOfBirth = GetingBirthDateInDateTime();

            int maxID = GetMaxIDOfStudents() + 1;

            Student newStudent = new Student(firstName, lastName, dateOfBirth, maxID);
            StudentsList.Add(newStudent);

            return newStudent;
        }

        private int GetMaxIDOfStudents()
        {
            int result = 0;
            if (StudentsList.Count != 0)
            {
                result = StudentsList.Max(x => x.ID);
            }
            return result;
        }

        public void DisplayListOfStudents()
        {
            foreach (Student student in StudentsList)
            {
                Console.WriteLine("ID  :{0}. , First name:  {1}, Last name:  {2} ", student.ID, student.FirstName, student.LastName);
            }
        }
        /////////////////////////////////////////////////////////////
        public void UserAddTheCourse()
        {
            ConsoleKey key = ConsoleKey.Add;
            Console.Clear();
            string messageAddContinue = "Adding a new course. Press any key to continue or 'Escape' to exit";

            Console.WriteLine(messageAddContinue);
            key = Console.ReadKey(true).Key;

            while (key != ConsoleKey.Escape)
            {
                Course newCourse = CreateCourse();
                Console.WriteLine("New course by ID {0} was added! ", newCourse.ID);
                Console.WriteLine("  ");
                Console.WriteLine(messageAddContinue);
                key = Console.ReadKey(true).Key;

            }
        }

        public Course CreateCourse()
        {
            Console.Clear();
            Console.WriteLine("Addind a new course.");
            Console.Write("Name of course: ");
            string courseName = Console.ReadLine();

            int maxID = GetMaxIDOfCourses() + 1;

            Course newCourse = new Course(courseName, maxID);
            CoursesList.Add(newCourse);

            return newCourse;
        }

        private int GetMaxIDOfCourses()
        {
            int result = 0;
            if (CoursesList.Count != 0)
            {
                result = CoursesList.Max(x => x.ID);
            }
            return result;
        }

        /////////////////////////////////////////////////////////////////
        public void UserAddNotes()
        {
            ConsoleKey key = ConsoleKey.Add;
            Console.Clear();
            string messageAddContinue = "Adding a new note. Press any key to continue or 'Escape' to exit";

            Console.WriteLine(messageAddContinue);
            key = Console.ReadKey(true).Key;
            int selectedIDStudent;

            do
            {
                Console.Write("To select a student type corresponding ID from the list following:\n");
                Console.Write("-----------------------------------------------------------------\n");
                DisplayListOfStudents();
                Console.Write("-----------------------------------------------------------------\n");
                Console.Write("ID of student:");

            } while (!Int32.TryParse(Console.ReadLine(), out selectedIDStudent) && StudentsList.Find(x => x.ID == selectedIDStudent) != null);


            Console.Clear();
            Student selectedStudent = StudentsList.Find(x => x.ID == selectedIDStudent);
            Console.WriteLine("Student selected : " + selectedStudent.FirstName + " " + selectedStudent.LastName);

            CreateNoteForStudentParCours(selectedStudent);


            //while (key != ConsoleKey.Escape)
            //{
            //    Note newNote = CreateNote();
            //    Console.WriteLine("New course by ID {0} was added! ", newCourse.ID);
            //    Console.WriteLine("  ");
            //    Console.WriteLine(messageAddContinue);
            //    key = Console.ReadKey(true).Key;

            //}
        }


        public void DisplayListOfCours()
        {
            foreach (Course stucoursent in CoursesList)
            {
                Console.WriteLine("ID  :{0}., Name: {1}", stucoursent.ID, stucoursent.Name);
            }
        }
        //////////////////////////////////////////////////
        public void CreateNoteForStudentParCours(Student student)
        {
            int selectedIDCourse;
            ConsoleKey key = ConsoleKey.Add;

            string messageAddContinue = "Adding a new note. Press any key to continue or 'Escape' to exit";

            Console.WriteLine(messageAddContinue);
            key = Console.ReadKey(true).Key;
            int selectedIDStudent;
            while (key != ConsoleKey.Escape)
            {
                do
                {
                    Console.Write("To select a course type corresponding ID from the list following: ");
                    DisplayListOfCours();

                } while (!Int32.TryParse(Console.ReadLine(), out selectedIDCourse) && CoursesList.Find(x => x.ID == selectedIDCourse) != null);

                Console.WriteLine("  ");
                Course selectedCours = CoursesList.Find(x => x.ID == selectedIDCourse);
                Console.WriteLine("Selected course: " + selectedCours.Name);
                Console.WriteLine("  ");
                int noteOfCourse;
                do
                {

                    Console.WriteLine("Enter note :");

                } while (!Int32.TryParse(Console.ReadLine(), out noteOfCourse));

                Console.WriteLine("Enter appreciation :");

                string appreciation = Console.ReadLine();

                Note newNote = new Note(student, selectedCours, noteOfCourse, appreciation);
                key = Console.ReadKey(true).Key;
            }
            }
    }


}
