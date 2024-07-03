using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1_ApplicationConsole.Menus
{
    public static class MenuLists
    {

        public static List<string> StudentsMetuLevel1()
        {
            List<string> listMenuStudents = new List<string>();
            listMenuStudents.Add("Lister les élèves");
            listMenuStudents.Add("Créer un nouvel élève");
            listMenuStudents.Add("Consulter un élève existant");
            listMenuStudents.Add("Ajouter une note et une appréciation pour un cours sur un élève existant");
            listMenuStudents.Add("Revenir au menu principal");

            return listMenuStudents;
        }

        public static List<string> CourseMetuLevel1()
        {
            List<string> listMenuScool = new List<string>();
            listMenuScool.Add("Lister les cours existants");
            listMenuScool.Add("Ajouter un nouveau cours au programme");
            listMenuScool.Add("Supprimer un cours par son identifiant");
            listMenuScool.Add("Revenir au menu principal");
            return listMenuScool;
        }

        public static List<string> MenuScoolList()
        {
            List<string> listMenuScool = new List<string>();
            listMenuScool.Add("Elèves");
            listMenuScool.Add("Cours");
            listMenuScool.Add("Promotion");
            listMenuScool.Add("Sortir");

            return listMenuScool;
        }

        public static List<string> MenuPromotionLevel1()
        {
            List<string> listMenuScool = new List<string>();
            listMenuScool.Add("Ajouter promotion aux étudiants");
            listMenuScool.Add("Liste des promotions");
            listMenuScool.Add("Liste des élèves en promotion");
            listMenuScool.Add("Moyenne par cours de tous les élèves per promotion");
            listMenuScool.Add("Moyenne de chaque promotion par cours");
            listMenuScool.Add("Revenir au menu principal");

            return listMenuScool;
        }

    }
}
