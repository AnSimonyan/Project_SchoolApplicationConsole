using Projet1_ApplicationConsole.App;
using Projet1_ApplicationConsole.Data;
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
            Database dataTools = new Database();
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

        public void StudentInformation()
        {
            Student selectedStudent = SelectStudentFromList();

            Console.Clear();

            DisplayInformation.DisplayInformationForStudent(selectedStudent);

            DisplayInformation.DisplayInformationForStudentNotes(selectedStudent, _appToolsInitialised);

            Log.Information("Consultation des détails de l'élève: " + selectedStudent.ID);
        }

        private Student CreateStudentAndAddToList(string firstName, string lastName)
        {
           Student newStudent = DataTools.CreateNewStudent(_appToolsInitialised, firstName, lastName);
           DataTools.AddStudentToTheList(_appToolsInitialised, newStudent);
           return newStudent;
        }

        private Student SelectStudentFromList()
        {
            uint selectedIDStudent = DisplayInformation.DisplayStudentsForSelection(_appToolsInitialised);
            return DataTools.FindStudentByID(selectedIDStudent, _appToolsInitialised);
        }
        #endregion <----------------------------------------------------------------------STUDENTS

        #region COURSE-------------------------------------------------------------------------->
        public Course CreateCourse()
        {
            Course newCourse = DataTools.CreateNewCourse(_appToolsInitialised, DisplayInformation.EnterNameOfCourse());
            DataTools.AddCourseToTheList(_appToolsInitialised,newCourse);   
            Log.Information("Ajout d'un nouveau cours : " + newCourse.Name);
            Save();
            return newCourse;
        }

        public void DeleteCourse()
        {
            Console.WriteLine(ConstantsAPP.MESSAGEDELETECOURSE);

            Course courseToDelete = CouseSelectionByID();

            Console.WriteLine(ConstantsAPP.MESSAGEWARNINGDELETECOURSE, courseToDelete.Name);

            bool deletingConfirmed = DisplayInformation.WarningBeforeDeletingCourse();

            if (deletingConfirmed)
            {
                ConfirmationCourseDeleting(courseToDelete);
             
                Save();

                Log.Information("Supprimer le cours :" + courseToDelete);
            }
        }
  

        private void ConfirmationCourseDeleting(Course courseToDelete)
        {
            foreach (Student student in _appToolsInitialised.StudentsList)
            {
                student.DeleteNotesByCourse(courseToDelete.ID);
            }
            _appToolsInitialised.CoursesList.Remove(courseToDelete);
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
            uint idCourse = DisplayInformation.DisplayCouresForSelection(_appToolsInitialised);

            return DataTools.FindCourseByID(idCourse, _appToolsInitialised);
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

            DisplayInformation.DisplayInformationForStudent(selectedStudent);

            CreateNoteForStudentParCours(selectedStudent);

            Save();
        }

        public void CreateNoteForStudentParCours(Student student)
        {
            if (IsCoursesListIsEmpty()) return;

            Course selectedCours = CouseSelectionByID();

            Console.WriteLine("Selected course: " + selectedCours.Name + "\n");

            uint noteEntered = DisplayInformation.EnterNoteByCourse();

            string? appreciation = DisplayInformation.EnterAppreciation();

            student.AddTheNoteForStudent(selectedCours, noteEntered, appreciation);

            Log.Information("Add note:" + selectedCours.Name + noteEntered + "/" + ConstantsAPP.MAXNOTE + " " + appreciation);
        }

        #endregion <--------------------------------------------------------------------------NOTES
    }
}
