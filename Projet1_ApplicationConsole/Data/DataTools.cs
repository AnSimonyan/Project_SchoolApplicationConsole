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
        public static Course FindCourseByID(uint idCourse,AppData appData)
        {
            return appData.CoursesList.Find(x => x.ID == idCourse);
        }

        public static Course CreateNewCourse(AppData appData, string courseName )
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
       
        public static int GetMaxIDOfStudents(AppData appData)
        {
            int result = 0;
            if (appData.StudentsList.Count != 0) result = appData.StudentsList.Max(x => x.ID);
            return result;
        }

        public static Student CreateNewStudent(AppData appData, string firstName, string lastName)
        {
            return  new Student(firstName, lastName, DisplayInformation.GetingBirthDateInDateTime(), DataTools.GetMaxIDOfStudents(appData) + 1);
        }

        public static void AddStudentToTheList(AppData appData, Student student)
        {
            appData.StudentsList.Add(student);
        }
        #endregion <-----------------------------------------------------------STUDENTS
    }
}
