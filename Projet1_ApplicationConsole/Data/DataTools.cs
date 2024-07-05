using Projet1_ApplicationConsole.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole.Data
{
    public static class DataTools
    {
        #region COURSES------------------------------------------------------------->
        public static Course FindCourseByID(uint idCourse, AppData appData)
        {
            return appData.CoursesList.Find(x => x.ID == idCourse);
        }

        public static Course FindCourseByIndex(uint idCourse, AppData appData)
        {
            return appData.CoursesList[Convert.ToInt32(idCourse)];
        }

        public static Course CreateNewCourse(AppData appData, string courseName)
        {
            return new Course(courseName, GetMaxIDOfCourses(appData) + 1);
        }

        public static void AddCourseToTheList(AppData appData, Course newCourse)
        {
            appData.CoursesList.Add(newCourse);
        }

        public static int GetMaxIDOfCourses(AppData appData)
        {
            int result = 0;
            if (appData.CoursesList.Count != 0) result = appData.CoursesList.Max(x => x.ID);
            return result;
        }
        #endregion <-----------------------------------------------------------COURSES

        #region STUDENTS------------------------------------------------------------->
        public static Student FindStudentByID(uint idStudent, AppData appData)
        {
            return appData.StudentsList.Find(x => x.ID == idStudent);
        }

        public static Student FindStudentByIndex(uint idStudent, AppData appData)
        {
            return appData.StudentsList[Convert.ToInt32(idStudent)];
        }

        public static int GetMaxIDOfStudents(AppData appData)
        {
            int result = 0;
            if (appData.StudentsList.Count != 0) result = appData.StudentsList.Max(x => x.ID);
            return result;
        }

        public static Student CreateNewStudent(AppData appData, string firstName, string lastName)
        {
            return new Student(firstName, lastName, ReadLineAndControls.GettingBirthDateInDateTimeFormat(), DataTools.GetMaxIDOfStudents(appData) + 1);
        }

        public static void AddStudentToTheList(AppData appData, Student student)
        {
            appData.StudentsList.Add(student);
        }

        public static void AddPromotionToStudent(Student student, string promotion)
        {
            student.SetPromotion(promotion);
        }

        public static List<Student> GetPromoStudentsList(AppData appDataInitialised)
        {
            return appDataInitialised.StudentsList.FindAll(s => s.GetPromotion() != "").ToList();
        }

        public static List<Student> GetStudentsListByPromo(AppData appDataInitialised, string promo)
        {
            return appDataInitialised.StudentsList.FindAll(s => s.GetPromotion() == promo).ToList();
        }

        public  static List<string> GetUniqPomotionListForStudentList(List<Student> students)
        {
           return students.Select(x => x.GetPromotion()).ToList().Distinct().ToList();
        }

        public static List<Course> GetListOfUniquCoursesIDFromStudentsList(List<Student> students,AppData appData)
        {
            var courseList = from s in students from n in s.NotesOfStudent select n.GetTheNoteCourseByID(appData.CoursesList);
            return  courseList.Distinct().ToList();
        }

        public static List<double> GetNotesValuesByCouresIdAndPromoFromStudentsList(List<Student> students,string promo, int courseID)
        {
            var noteList = from s in students
                           where s.GetPromotion() == promo
                           from n in s.NotesOfStudent
                           where n.CourseID == courseID
                           select n.Value;

            return noteList.ToList();
        }

        #endregion <-----------------------------------------------------------STUDENTS
    }
}
