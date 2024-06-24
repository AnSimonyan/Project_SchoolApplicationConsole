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
        public const string MESSAGEADDCONTINUE = "Adding a new {0}. Press any key to continue or 'Escape' to exit";
        public const string MESSAGELINESEPARATOR = "\n-----------------------------------------------------------------\n";
        public const int MAXNOTE = 20;

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
            string messageAddContinue = MESSAGEADDCONTINUE.Replace("{0}", "student");//"Adding a new student. Press any key to continue or 'Escape' to exit";

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
                Console.WriteLine("ID  :{0}. First name:  {1}, Last name:  {2} ", student.ID, student.FirstName, student.LastName);
            }
        }

        public void DisplayInformationForStudent(Student selectedStudent)
        {
            Console.WriteLine(MESSAGELINESEPARATOR);

            Console.WriteLine("Informations sur l'élève : \n ");
            Console.WriteLine("{0,-18} {1,-20}", "Nom :", selectedStudent.FirstName);
            Console.WriteLine("{0,-18} {1,-20}", "Prénom :", selectedStudent.LastName);
            Console.WriteLine("{0,-18} {1,-20}", "Date de naissance :", selectedStudent.DateOfBirth.ToString("d"));

        }

        public void StudentInformation()
        {
            
            int selectedIDStudent;

            do
            {
                Console.Write("To select a student type corresponding ID from the list following:\n");
                Console.Write(MESSAGELINESEPARATOR);
                DisplayListOfStudents();
                Console.Write(MESSAGELINESEPARATOR);
                Console.Write("ID of student:");

            } while (!Int32.TryParse(Console.ReadLine(), out selectedIDStudent) || StudentsList.Find(x => x.ID == selectedIDStudent) == null);

            Console.Clear();
            Student selectedStudent = StudentsList.Find(x => x.ID == selectedIDStudent);

            DisplayInformationForStudent(selectedStudent);

            DisplayInformationForStudentNotes(selectedStudent);



        }
        /////////////////////////////////////////////////////////////
        public void UserAddTheCourse()
        {
            ConsoleKey key = ConsoleKey.Add;
            Console.Clear();
            string messageAddContinue = MESSAGEADDCONTINUE.Replace("{0}", "course");//"Adding a new course. Press any key to continue or 'Escape' to exit";

            Console.WriteLine(messageAddContinue);
            key = Console.ReadKey(true).Key;

            while (key != ConsoleKey.Escape)
            {
                Course newCourse = CreateCourse();
                Console.WriteLine("New course by ID {0} was added! \n", newCourse.ID);

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

        public void DisplayListOfCours()
        {
            Console.WriteLine(MESSAGELINESEPARATOR);
            foreach (Course stucoursent in CoursesList)
            {
                Console.WriteLine("ID  :{0}. Name: {1}", stucoursent.ID, stucoursent.Name);
            }
            Console.WriteLine(MESSAGELINESEPARATOR);
        }

        public void DeleteCourse()
        {
            int idCourseToDelete;
            ConsoleKey key = ConsoleKey.Add;
            Console.Clear();

            do
            {
                Console.WriteLine("You want to delete a course. Select the ID of cours for delete, please. ");
                DisplayListOfCours();
            } while (!Int32.TryParse(Console.ReadLine(), out idCourseToDelete) || CoursesList.Find(x => x.ID == idCourseToDelete) == null);

            Course courseToDelete = CoursesList.Find(x => x.ID == idCourseToDelete);

            Console.WriteLine("Every note relating to this course will be deleted! Are you sure you want to delete the course {0}? Y/N", courseToDelete.Name);
            do
            {
                Console.WriteLine("Enter Y to confirm or N to cancel deleting!");
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            if (key == ConsoleKey.Y)
            {
                foreach (Student student in StudentsList)
                {
                    student.DeleteNotesByCourse(courseToDelete);
                }
                CoursesList.Remove(courseToDelete);
            }


        }


        /////////////////////////////////////////////////////////////////
        public void AddNotes()
        {
            Console.Clear();
            string messageAddContinue = MESSAGEADDCONTINUE.Replace("{0}", "note"); 

            Console.WriteLine(messageAddContinue);
            int selectedIDStudent;

            do
            {
                Console.Write("To select a student type corresponding ID from the list following:\n");
                Console.Write(MESSAGELINESEPARATOR);
                DisplayListOfStudents();
                Console.Write(MESSAGELINESEPARATOR);
                Console.Write("ID of student:");

            } while (!Int32.TryParse(Console.ReadLine(), out selectedIDStudent) || StudentsList.Find(x => x.ID == selectedIDStudent) == null);

            Console.Clear();
            Student selectedStudent = StudentsList.Find(x => x.ID == selectedIDStudent);

            DisplayInformationForStudent(selectedStudent);

            CreateNoteForStudentParCours(selectedStudent);

        }


        //////////////////////////////////////////////////
        public void CreateNoteForStudentParCours(Student student)
        {
            int selectedIDCourse;
            ConsoleKey key = ConsoleKey.Add;

            string messageAddContinue = MESSAGEADDCONTINUE.Replace("{0}", "note");

            Console.WriteLine(messageAddContinue);
            key = Console.ReadKey(true).Key;

            while (key != ConsoleKey.Escape)
            {
                do
                {
                    Console.Write("To select a course type corresponding ID from the list following:\n");
                    DisplayListOfCours();

                } while (!Int32.TryParse(Console.ReadLine(), out selectedIDCourse) || CoursesList.Find(x => x.ID == selectedIDCourse) == null);

                Course selectedCours = CoursesList.Find(x => x.ID == selectedIDCourse);
                Console.WriteLine("Selected course: " + selectedCours.Name + "\n");

                int noteOfCourse;
                do
                {
                    Console.Write("Enter note :");

                } while (!Int32.TryParse(Console.ReadLine(), out noteOfCourse));

                Console.Write("Enter appreciation :");

                string appreciation = Console.ReadLine();

                student.AddTheNoteForStudent(selectedCours, noteOfCourse, appreciation);
               
                key = Console.ReadKey(true).Key;
            }
        }

        public void DisplayInformationForStudentNotes(Student selectedStudent)
        {
            Console.WriteLine("Résultats scolaires:\n");

            List<Note> studentNotes = selectedStudent.GetStudentsNotes();
            double notesTotal = 0; double avarageNotes = 0;

            foreach (Note note in studentNotes)
            {
                Console.WriteLine("\t Cours : " + note.Course.Name);
                Console.WriteLine("\t \t Note : " + note.Value + "/" + Convert.ToString(MAXNOTE));
                Console.WriteLine("\t \t Appréciation : " + note.Appreciation + "\n");

                notesTotal = notesTotal + note.Value;
            }
            if (studentNotes.Count != 0) avarageNotes = notesTotal / studentNotes.Count;

            Console.WriteLine("\t Moyenne : " + Convert.ToString(avarageNotes) + "\n");
            Console.Write(MESSAGELINESEPARATOR);
        }


    }


}
