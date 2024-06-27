using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projet1_ApplicationConsole
{
    public class UserTools
    {
        private AppData _appToolsInitialised;

        public UserTools(AppData appToolsInitialised)
        {
            this._appToolsInitialised = appToolsInitialised;
        }
        public void Save()
        {
            DatabaseTools dataTools = new DatabaseTools();
            dataTools.SaveData(_appToolsInitialised);
        }

        #region STUDENTS -------------------------------------------------------------------------->

        public Student CreateStudent()
        {
            Console.Write("First name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last name: ");
            string lastName = Console.ReadLine();

            Student newStudent = CreateStudentAndAddToList(firstName, lastName);

            Save();

            Log.Information("CreateStudent");

            return newStudent;
        }

        public void DisplayListOfStudents()
        {
            foreach (Student student in _appToolsInitialised.StudentsList)
            {
                Console.WriteLine("ID  :{0,5}. First name:  {1,-10}  Last name:  {2,-20} ", student.ID, student.FirstName, student.LastName);
            }
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
            Student selectedStudent = SelectStudentFromList();

            Console.Clear();

            DisplayInformationForStudent(selectedStudent);

            DisplayInformationForStudentNotes(selectedStudent);

            Log.Information("Consultation des détails de l'élève: " + selectedStudent.ID);
        }

        private Student CreateStudentAndAddToList(string firstName, string lastName)
        {
            Student newStudent = new Student(firstName, lastName, GetingBirthDateInDateTime(), GetMaxIDOfStudents() + 1);

            _appToolsInitialised.StudentsList.Add(newStudent);

            return newStudent;
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

        private int GetMaxIDOfStudents()
        {
            int result = 0;
            if (_appToolsInitialised.StudentsList.Count != 0) result = _appToolsInitialised.StudentsList.Max(x => x.ID);
            return result;
        }

        private Student FindStudentByID(int idStudent)
        {
            return _appToolsInitialised.StudentsList.Find(x => x.ID == idStudent);
        }

        private Student SelectStudentFromList()
        {
            int selectedIDStudent;
            do
            {
                Console.Write("To select a student type corresponding ID from the list following:\n");
                Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
                DisplayListOfStudents();
                Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
                Console.Write("ID of student:");


            } while (!Int32.TryParse(Console.ReadLine(), out selectedIDStudent) || FindStudentByID(selectedIDStudent) == null);

            return FindStudentByID(selectedIDStudent);
        }

        #endregion <----------------------------------------------------------------------STUDENTS

        #region COURSE-------------------------------------------------------------------------->
        public Course CreateCourse()
        {
            Console.Clear();
            Console.WriteLine("Addind a new course.");
            Console.Write("Name of course: ");

            string courseName = Console.ReadLine();

            Course newCourse = new Course(courseName, GetMaxIDOfCourses() + 1);

            _appToolsInitialised.CoursesList.Add(newCourse);

            Log.Information("Ajout d'un nouveau cours : " + courseName);

            Save();

            return newCourse;
        }

        public void DisplayListOfCours()
        {
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);

            foreach (Course stucoursent in _appToolsInitialised.CoursesList)
            {
                Console.WriteLine("ID  :{0}. Name: {1}", stucoursent.ID, stucoursent.Name);
            }
            
            Console.WriteLine(ConstantsAPP.MESSAGELINESEPARATOR);
        }

        public void DeleteCourse()
        {
            Console.WriteLine(ConstantsAPP.MESSAGEDELETECOURSE);

            Course courseToDelete = CouseSelectionByID();

            Console.WriteLine(ConstantsAPP.MESSAGEWARNINGDELETECOURSE, courseToDelete.Name);

            bool deletingConfirmed = WarningBeforeDeletingCourse();

            if (deletingConfirmed)
            {
                ConfirmationCourseDeleting(courseToDelete);
             
                Save();

                Log.Information("Supprimer le cours :" + courseToDelete);
            }
        }

        private int GetMaxIDOfCourses()
        {
            int result = 0;
            if (_appToolsInitialised.CoursesList.Count != 0) result = _appToolsInitialised.CoursesList.Max(x => x.ID);
            return result;
        }

        private bool WarningBeforeDeletingCourse()
        {
            bool resultat = false;
            ConsoleKey key = ConsoleKey.Add;
            do
            {
                Console.WriteLine("Enter Y to confirm or N to cancel deleting!");
                key = Console.ReadKey(true).Key;

            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            if (key == ConsoleKey.Y) resultat = true;

            return resultat;
        }

        private void ConfirmationCourseDeleting(Course courseToDelete)
        {
            foreach (Student student in _appToolsInitialised.StudentsList)
            {
                student.DeleteNotesByCourse(courseToDelete.ID);
            }
            _appToolsInitialised.CoursesList.Remove(courseToDelete);
        }

        private Course FindCourseByID(uint idCourse)
        {
            return _appToolsInitialised.CoursesList.Find(x => x.ID == idCourse);
        }

        private bool IsCoursesListIsEmpty()
        {
            bool resultat = false;

            if (_appToolsInitialised.CoursesList.Count == 0)
            {
                Console.WriteLine("There is no courses added. Add at first a course");
                resultat = true;
            }
            return resultat;
        }

        private Course CouseSelectionByID()
        {
            uint idCourse = 0;
            do
            {
                Console.Write("To select a course type corresponding ID from the list following:\n");
                DisplayListOfCours();

            } while (!UInt32.TryParse(Console.ReadLine(), out idCourse) || FindCourseByID(idCourse) == null);

            Course selectedCours = FindCourseByID(idCourse);

            return selectedCours;
        }

        #endregion <-------------------------------------------------------------------------COURSES

        #region NOTES-------------------------------------------------------------------------->
        public void AddNotes()
        {
            Console.Clear();
            string messageAddContinue = ConstantsAPP.MESSAGEADDCONTINUE.Replace("{0}", "note");

            Console.WriteLine(messageAddContinue);

            Student selectedStudent = SelectStudentFromList();

            Console.Clear();

            DisplayInformationForStudent(selectedStudent);

            CreateNoteForStudentParCours(selectedStudent);

            Save();
        }

        public void CreateNoteForStudentParCours(Student student)
        {
            if (IsCoursesListIsEmpty()) return;

            Course selectedCours = CouseSelectionByID();

            Console.WriteLine("Selected course: " + selectedCours.Name + "\n");

            uint noteEntered = EnterNoteByCourse();

            string? appreciation = EnterAppreciation();

            student.AddTheNoteForStudent(selectedCours, noteEntered, appreciation);

            Log.Information("Add note:" + selectedCours.Name + noteEntered + "/" + ConstantsAPP.MAXNOTE + " " + appreciation);
        }

        public void DisplayInformationForStudentNotes(Student selectedStudent)
        {
            double notesTotal = 0; double avarageNotes = 0;

            Console.WriteLine("Résultats scolaires:\n");

            List<Note> studentNotes = selectedStudent.GetStudentsNotes();

            if (studentNotes != null) notesTotal = ShowNotesListWithNotesTotal(studentNotes);

            if (studentNotes.Count != 0) avarageNotes = notesTotal / studentNotes.Count;

            Console.WriteLine("\t Moyenne : " + Convert.ToString(avarageNotes));

            Console.Write(ConstantsAPP.MESSAGELINESEPARATOR);
        }

        private uint EnterNoteByCourse()
        {
            uint noteOfCourse;
            do
            {
                Console.Write("Enter note :");

            } while (!UInt32.TryParse(Console.ReadLine(), out noteOfCourse) || noteOfCourse > 20);

            return noteOfCourse;
        }

        private string EnterAppreciation()
        {
            Console.Write("Enter appreciation :");
            return Console.ReadLine();
        }

        private double ShowNotesListWithNotesTotal(List<Note> studentNotes)
        {
            double notesTotal = 0;

            foreach (Note note in studentNotes)
            {
                Console.WriteLine("\t Cours : " + note.GetTheNoteCourseByID(_appToolsInitialised.CoursesList).Name);
                Console.WriteLine("\t \t Note : " + note.Value + "/" + Convert.ToString(ConstantsAPP.MAXNOTE));
                Console.WriteLine("\t \t Appréciation : " + note.Appreciation + "\n");

                notesTotal = notesTotal + note.Value;
            }
            return notesTotal;
        }
        #endregion <--------------------------------------------------------------------------NOTES
    }


}
