using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole
{
    internal class Course
    {
        public int ID { get; }

        public string Name { get; }

        public Course(string name, int id)
        {
          Name = name;
          ID = id;
        }
              
        public void DeleteCourse(int idOfCours)
        {
            
        }

    }
}
