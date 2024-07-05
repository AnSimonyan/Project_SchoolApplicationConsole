# Introduction
Afin de révolutionner le système informatique de l'éducation nationale, il etais nécessaire de créer d'une application console permettant de réaliser la saisie des notes des élèves.  La condition d'obtention du diplôme nécessite bien évidemment la réussite des examens. Examens catégorisés par matières, et donnant lieu à une note ainsi qu'une appréciation de la part du professeur.
La solution permettant aux professeurs de saisir les notes et appréciations de leurs différents élèves. Les interfaces graphiques (web ou applicatives) sont à proscrire afin de ne pas heurter les potentiels utilisateurs daltonniens. Afin d'éviter toute fuite de données, aucun système de gestion en ligne ne a pas  été utilisé. Toutes les données saisies et manipulées dans l'application enregistrent dans un fichier texte au format JSON afin de pouvoir facilement être échangées sur support amovible (disquette/CD-ROM/clé USB).
# Technologies
## C#.Net
Le programme est développé dans le langage C# en utilisant le framework .Net Core 8.0
## Newtonsoft.JSON
La persistence se fait dans un simple fichier texte au format JSON.
La librairie Newtonsoft.JSON est utilisée pour la manipulation des données au format JSON
# Spécification fonctionnelle
## Menu
Au lancement de l'application, un menu permet à l'utilisateur de choisir entre ces entrées :
### Elèves
### Cours
#### Le menu Elèves permet de :
* Lister les élèves
* Créer un nouvel élève
* Consulter un élève existant
* Ajouter une note et une appréciation pour un cours sur un élève existant
* Revenir au menu principal
#### Le menu Cours permet de :
* Lister les cours existants
* Ajouter un nouveau cours au programme
* Supprimer un cours par son identifiant
* Revenir au menu principal
* La notion d'Élève
#### Un élève est composé des attributs suivants :
* Un identifiant unique au format numérique
* Un nom au format texte
* Un prénom au format texte
* Une date de naissance
* Un liste de notes et d'appréciation du professeur  pour chaque cours
* La moyenne de ses notes qui calcule à la volée et ne enregistre pas dans le fichier
#### Un cours est composés des attributs suivants :
* Un identifiant unique au format numérique
* Un nom au format texte
    
  Quand un cours est supprimé du programme,  les notes et appréciations de ce cours sont également supprimer pour tous les élèves.
  Toute modification faite par l'utilisateur (ajout/suppression de cours, ajout de note etc...) donne lieux à une sauvegarde au format JSON dans le fichier.
  Toutes les saisies utilisateurs sont contrôlées afin d'éviter les erreurs de saisie.
#### La notion de "Promotion" avec les fonctionnalité suivants permet:
* Rattacher chaque élève   à une promotion
* Obtenir la liste des promotions est déduites des élèves existants (pas stockées dans le fichier JSON)
* Afficher dans le menu principal  la liste des promotions existantes
* Afficher la liste des élèves dans une promotion (nom/prénom/etc... pas les notes)
* Afficher la moyenne par cours de tous les élèves d'une promotion donnée
* Afficher  la moyenne de chaque promotion par cours
  
