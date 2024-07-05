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
    public static class UserTools
    {

        public static void Save(AppData _appDataInitialised)
        {
            Database dataTools = new Database();
            dataTools.SaveData(_appDataInitialised);
        }
        #region STUDENTS -------------------------------------------------------------------------->

        public static Student CreateStudent(AppData _appDataInitialised)
        {
            string firstName = ReadLineAndControls.ReadStringWithNullAndSpasesControl("First name: ");
            string lastName = ReadLineAndControls.ReadStringWithNullAndSpasesControl("Last name: ");

            Student newStudent = CreateStudentAndAddToList(firstName, lastName, _appDataInitialised);

            Save(_appDataInitialised);

            Log.Information("CreateStudent");

            return newStudent;
        }

        public static void AddPromotionToStudent(AppData _appDataInitialised)
        {
            string Promotion = ReadLineAndControls.ReadStringWithNullAndSpasesControl("Enter promotion:").ToUpper();
            Student selectedSelected = SelectStudentFromList(_appDataInitialised);
            DataTools.AddPromotionToStudent(selectedSelected, Promotion);
            DisplayInformation.DisplayStudentWasChanged(selectedSelected);
        }

        public static void StudentInformation(AppData _appDataInitialised)
        {
            Student selectedStudent = SelectStudentFromList(_appDataInitialised);

            Console.Clear();

            DisplayInformation.DisplayInformationForStudent(selectedStudent);

            DisplayInformation.DisplayInformationForStudentNotes(selectedStudent, _appDataInitialised);

            Log.Information("Consultation des détails de l'élève: " + selectedStudent.ID);
        }

        private static Student CreateStudentAndAddToList(string firstName, string lastName, AppData _appDataInitialised)
        {
            Student newStudent = DataTools.CreateNewStudent(_appDataInitialised, firstName, lastName);
            DataTools.AddStudentToTheList(_appDataInitialised, newStudent);
            return newStudent;
        }

        private static Student SelectStudentFromList(AppData _appDataInitialised)
        {
            uint selectedIDStudent = ReadLineAndControls.DisplayStudentsForSelection(_appDataInitialised);
            // return DataTools.FindStudentByID(selectedIDStudent, _appDataInitialised);
            return DataTools.FindStudentByIndex(selectedIDStudent, _appDataInitialised);
        }
        #endregion <----------------------------------------------------------------------STUDENTS

        #region COURSE-------------------------------------------------------------------------->
        public static Course CreateCourse(AppData _appDataInitialised)
        {
            Course newCourse = DataTools.CreateNewCourse(_appDataInitialised, ReadLineAndControls.ReadStringWithNullAndSpasesControl("Name of course: "));
            DataTools.AddCourseToTheList(_appDataInitialised, newCourse);
            Log.Information("Ajout d'un nouveau cours : " + newCourse.Name);
            Save(_appDataInitialised);
            return newCourse;
        }

        public static void DeleteCourse(AppData _appDataInitialised)
        {
            Console.WriteLine(ConstantsAPP.MESSAGEDELETECOURSE);

            Course courseToDelete = CouseSelectionByID(_appDataInitialised);

            Console.WriteLine(ConstantsAPP.MESSAGEWARNINGDELETECOURSE, courseToDelete.Name);

            bool deletingConfirmed = ReadLineAndControls.WarningBeforeDeletingCourse();

            if (deletingConfirmed)
            {
                ConfirmationCourseDeleting(courseToDelete, _appDataInitialised);

                Save(_appDataInitialised);

                Log.Information("Suppression de cours: " + courseToDelete.Name);
                Log.Information("Chaque note relative à ce cours a été supprimée!");
            }
        }

        private static void ConfirmationCourseDeleting(Course courseToDelete, AppData _appDataInitialised)
        {
            foreach (Student student in _appDataInitialised.StudentsList)
            {
                student.DeleteNotesByCourse(courseToDelete.ID);
            }
            _appDataInitialised.CoursesList.Remove(courseToDelete);
        }

        private static Course CouseSelectionByID(AppData _appDataInitialised)
        {
            uint idCourse = ReadLineAndControls.DisplayCouresForSelection(_appDataInitialised);
           
            return DataTools.FindCourseByIndex(idCourse, _appDataInitialised);
            // return DataTools.FindCourseByID(idCourse, _appDataInitialised);
        }
        #endregion <-------------------------------------------------------------------------COURSES

        #region NOTES-------------------------------------------------------------------------->
        public static void AddNotes(AppData _appDataInitialised)
        {
            Console.Clear();
            string messageAddContinue = ConstantsAPP.MESSAGEADDCONTINUE.Replace("{0}", "note");

            Console.WriteLine(messageAddContinue);

            Student selectedStudent = SelectStudentFromList(_appDataInitialised);

            Console.Clear();

            DisplayInformation.DisplayInformationForStudent(selectedStudent);

            CreateNoteForStudentParCours(selectedStudent, _appDataInitialised);

            Save(_appDataInitialised);
        }

        public static void CreateNoteForStudentParCours(Student student, AppData _appDataInitialised)
        {
            if (ReadLineAndControls.IsListIsEmpty(_appDataInitialised.CoursesList)) return;

            Course selectedCours = CouseSelectionByID(_appDataInitialised);

            Console.WriteLine("Selected course: " + selectedCours.Name + "\n");

            uint noteEntered = ReadLineAndControls.EnterNoteByCourse();

            string? appreciation = ReadLineAndControls.EnterAppreciation();

            student.AddTheNoteForStudent(selectedCours, noteEntered, appreciation);

            Log.Information("Add note:" + selectedCours.Name + noteEntered + "/" + ConstantsAPP.MAXNOTE + " " + appreciation);
        }

        #endregion <--------------------------------------------------------------------------NOTES

        #region PROMOTIONS-------------------------------------------------------------------------->
        public static void StudentsListByPromotions(AppData _appDataInitialised)
        {
            DisplayInformation.DisplayStudentInformationByPromotions(DataTools.GetPromoStudentsList(_appDataInitialised));
        }

        public static void PromotionsList(AppData _appDataInitialised)
        {
            DisplayInformation.DisplayPromotions(DataTools.GetPromoStudentsList(_appDataInitialised));
        }
        public static void CoursesAvarageByPromotions(AppData _appDataInitialised)
        {
            string selectedPromo = ReadLineAndControls.SelectThePromotionFromList(DataTools.GetPromoStudentsList(_appDataInitialised), _appDataInitialised);
            List<Student> studentsPromo = DataTools.GetStudentsListByPromo(_appDataInitialised, selectedPromo);
            Dictionary<Course, List<double>> NotesTable = new Dictionary<Course, List<double>>();
          
            foreach (Student student in studentsPromo)
            {
                foreach (Note note in student.NotesOfStudent)
                {
                    Course course = note.GetTheNoteCourseByID(_appDataInitialised.CoursesList);
                    if (!NotesTable.ContainsKey(course)) NotesTable.Add(course, new List<double>());
                    NotesTable[course].Add(note.Value);
                }
            }
            DisplayInformation.DisplayCoursesPromo(NotesTable);
        }

        public static void PromotionAvarageByCourses(AppData _appDataInitialised)
        {
            List<Student> students = DataTools.GetPromoStudentsList(_appDataInitialised);
            List<string> promoStudentsDistinct = DataTools.GetUniqPomotionListForStudentList(students);
            var courseListDistinct = DataTools.GetListOfUniquCoursesIDFromStudentsList(students, _appDataInitialised);
            DisplayInformation.DisplayPromoAvarageByCourse(students, courseListDistinct, promoStudentsDistinct);
        }

        #endregion <--------------------------------------------------------------------------PROMOTIONS
    }
}
