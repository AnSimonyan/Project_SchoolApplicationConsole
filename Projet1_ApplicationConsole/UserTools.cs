using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace Projet1_ApplicationConsole
{
    public class UserTools
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
            string messageAddContinue = ConstantsAPP.MESSAGEADDCONTINUE.Replace("{0}", "student");//"Adding a new student. Press any key to continue or 'Escape' to exit";

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
            JsonFiles.SaveJsonFile(this);
            Log.Information("CreateStudent");

            return newStudent;
        }

        private int GetMaxIDOfStudents()
        {
            int result = 0;
            if (StudentsList.Count != 0)  result = StudentsList.Max(x => x.ID);
            return result;
        }

        public void DisplayListOfStudents()
        {
            foreach (Student student in StudentsList) Console.WriteLine("ID  :{0,5}. First name:  {1,-10}  Last name:  {2,-20} ", student.ID, student.FirstName, student.LastName);
                      
            Log.Information("Consultation de la liste des élèves.");

        }

        public void DisplayInformationForStudent(Student selectedStudent)
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
           
            Console.WriteLine("Informations sur l'élève : \n ");
            Console.WriteLine("{0,-18} {1,-23}", "Nom :", selectedStudent.FirstName);
            Console.WriteLine("{0,-18} {1,-23}", "Prénom :", selectedStudent.LastName);
            Console.WriteLine("{0,-18} {1,-22}", "Date de naissance :", selectedStudent.DateOfBirth.ToString("d"));


        }

        public void StudentInformation()
        {
            
            int selectedIDStudent;

            do
            {
                Console.Write("To select a student type corresponding ID from the list following:\n");
                Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
                DisplayListOfStudents();
                Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
                Console.Write("ID of student:");

            } while (!Int32.TryParse(Console.ReadLine(), out selectedIDStudent) || StudentsList.Find(x => x.ID == selectedIDStudent) == null);

            Console.Clear();
            Student selectedStudent = StudentsList.Find(x => x.ID == selectedIDStudent);

            DisplayInformationForStudent(selectedStudent);

            DisplayInformationForStudentNotes(selectedStudent);

            Log.Information("Consultation des détails de l'élève: "+ selectedStudent.ID);

        }
        /////////////////////////////////////////////////////////////
        public void UserAddTheCourse()
        {
            ConsoleKey key = ConsoleKey.Add;
            Console.Clear();
            string messageAddContinue = ConstantsAPP.MESSAGEADDCONTINUE.Replace("{0}", "course");

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
            
            Log.Information("Ajout d'un nouveau cours : " + courseName);
            JsonFiles.SaveJsonFile(this);

            return newCourse;
        }

        private int GetMaxIDOfCourses()
        {
            int result = 0;
            if (CoursesList.Count != 0) result = CoursesList.Max(x => x.ID);
            return result;
        }

        public void DisplayListOfCours()
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
            foreach (Course stucoursent in CoursesList)  Console.WriteLine("ID  :{0}. Name: {1}", stucoursent.ID, stucoursent.Name);
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);

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
                    student.DeleteNotesByCourse(courseToDelete.ID);
                }
                CoursesList.Remove(courseToDelete);
            }

            JsonFiles.SaveJsonFile(this);

            Log.Information("Supprimer le cours :" + courseToDelete);


        }


        /////////////////////////////////////////////////////////////////
        public void AddNotes()
        {
            Console.Clear();
            string messageAddContinue = ConstantsAPP.MESSAGEADDCONTINUE.Replace("{0}", "note"); 

            Console.WriteLine(messageAddContinue);
            int selectedIDStudent;

            do
            {
                Console.Write("To select a student type corresponding ID from the list following:\n");
                Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
                DisplayListOfStudents();
                Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
                Console.Write("ID of student:");

            } while (!Int32.TryParse(Console.ReadLine(), out selectedIDStudent) || StudentsList.Find(x => x.ID == selectedIDStudent) == null);

            Console.Clear();
            Student selectedStudent = StudentsList.Find(x => x.ID == selectedIDStudent);

            DisplayInformationForStudent(selectedStudent);

            CreateNoteForStudentParCours(selectedStudent);

            JsonFiles.SaveJsonFile(this);
            
        }


        //////////////////////////////////////////////////
        public void CreateNoteForStudentParCours(Student student)
        {
            int selectedIDCourse;
            ConsoleKey key = ConsoleKey.Add;

            string messageAddContinue = ConstantsAPP.MESSAGEADDCONTINUE.Replace("{0}", "note");

            Console.WriteLine(messageAddContinue);
            key = Console.ReadKey(true).Key;

            if (CoursesList.Count == 0)
            {
                Console.WriteLine("There is no courses added. Add at first a course");
                return;
            }

            while (key != ConsoleKey.Escape)
            {
                do
                {
                    Console.Write("To select a course type corresponding ID from the list following:\n");
                    DisplayListOfCours();
                    key = Console.ReadKey(true).Key;

                } while (!Int32.TryParse(Console.ReadLine(), out selectedIDCourse) || CoursesList.Find(x => x.ID == selectedIDCourse) == null );

                Course selectedCours = CoursesList.Find(x => x.ID == selectedIDCourse);
                Console.WriteLine("Selected course: " + selectedCours.Name + "\n");

                int noteOfCourse;
                do
                {
                    Console.Write("Enter note :");
                    key = Console.ReadKey(true).Key;

                } while (!Int32.TryParse(Console.ReadLine(), out noteOfCourse));

                Console.Write("Enter appreciation :");

                string appreciation = Console.ReadLine();

                student.AddTheNoteForStudent(selectedCours, noteOfCourse, appreciation);
                
                Log.Information("Add note:" + selectedCours.Name +  noteOfCourse +"/" + ConstantsAPP.MAXNOTE + " " + appreciation);

                
            }
        }

        public void DisplayInformationForStudentNotes(Student selectedStudent)
        {
            Console.WriteLine("Résultats scolaires:\n");

            List<Note> studentNotes = selectedStudent.GetStudentsNotes();
            double notesTotal = 0; double avarageNotes = 0;
            if (studentNotes != null)
            {
                foreach (Note note in studentNotes)
                {
                    //Console.WriteLine("\t Cours : " + note.Course.Name);
                    Console.WriteLine("\t Cours : " + note.GetTheNoteCourseByID(CoursesList).Name);
                    Console.WriteLine("\t \t Note : " + note.Value + "/" + Convert.ToString(ConstantsAPP.MAXNOTE));
                    Console.WriteLine("\t \t Appréciation : " + note.Appreciation + "\n");

                    notesTotal = notesTotal + note.Value;
                }
            }
            if (studentNotes.Count != 0) avarageNotes = notesTotal / studentNotes.Count;

            Console.WriteLine("\t Moyenne : " + Convert.ToString(avarageNotes));
            Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
        }


    }


}
