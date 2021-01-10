using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Attention, indispensable pour lire et ecrire un fichier 
using System.IO;


namespace Source_Target
{
    class Program
    {
        //Il faut mettre le classeur qu'on veut transformer dans le BIN/DEBUG sous format NOMSOURCE1;TARGET1;TARGET2 \n NOMSOURCE2;TARGET1;... etc etc avec \n le saut de ligne
        //Il en resortira un classeur resultat de la forme NOMSOURCE1;TARGET1 \n NOMSOURCE1;TARGET2 ... etc etc !!
        public static string fichierEntree = "ClasseurEntree.txt";
        public static string fichierSortie = "ClasseurResultat.txt";

        static void LectureEcriture(string fichier_source,string fichier_cible)
        {
            // Création d'une instance de StreamReader pour permettre la lecture de notre fichier source fichierDico 
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            StreamReader monStreamReader = new StreamReader(fichier_source, encoding);

            // Création d'une instance de StreamWriter pour permettre l'ecriture de notre fichier cible
            StreamWriter monStreamWriter = File.CreateText(fichier_cible);

            string ligne = monStreamReader.ReadLine();
            monStreamWriter.Write("Source" + "\t" + "Target");

            while (ligne != null)
            {
                
                //Pour chaque ligne, 
                // Pour une ligne on veut prendre le premier pseudo, le garder en mémoire quelque part pour le réécrire à chaque ligne avant d'écrire, après une tabulation, le pseudo d'après
                //Pour différencier le premier des autre on met en place un compteur qui, égale à zéro, indique qu'on est bien sur le premier pseudo. C'est avant d'avoir trouver le premier moint virgule.
                int nbPseudo = 0; //compte le nombre de mot

                //Ensuite, à chaque fois qu'on rencontre un point virgule, on saute une ligne et on réécrit notre premier pseudo avant d'écrire ce qu'on trouve.
                string premierPseudo = "";
                string autrePseudo = "";

                for (int i = 0; i < ligne.Length; i++)
                {
                    //on initialise le premierPseudo pour chaque ligne
                    
                    char lettresPseudo = ligne[i];
                    //tant qu'on est sur le premier pseudo, on l'enregistre quelque part
                    if (nbPseudo == 0)
                    {
                        if (lettresPseudo.Equals(',') == false)
                        {
                            premierPseudo += lettresPseudo;
                        }
                        else if (lettresPseudo.Equals(',') == true)
                        {
                            nbPseudo++;
                        }
                    }
                    //On arrive au cas de la première target et de toutes les autres
                    else
                    {
                        
                        if (lettresPseudo.Equals(',') == false)
                        {
                            autrePseudo += lettresPseudo;
                        }
                        else if (lettresPseudo.Equals(',') == true)
                        {
                            nbPseudo++;
                            if (autrePseudo.Equals("") == false)
                            {
                                monStreamWriter.Write("\n" + premierPseudo + "\t" + autrePseudo);
                            }
                            autrePseudo = "";
                        }

                        if(i.Equals(ligne.Length - 1) == true)
                        {
                            nbPseudo++;
                            monStreamWriter.Write("\n" + premierPseudo + "\t" + autrePseudo);
                            autrePseudo = "";
                        }
                    }                    
                }
                ligne = monStreamReader.ReadLine();
            }

            monStreamReader.Close();
            monStreamWriter.Close();

        }

        private static void CentrerLeTexte(string texte)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Yellow;            
            int nbEspaces = (Console.WindowWidth - texte.Length) / 2;
            Console.SetCursorPosition(nbEspaces, Console.CursorTop);
            Console.WriteLine(texte);
        }

        static void Main()
        {

            CentrerLeTexte("Hello ESPECE DE MEC TROP STYLE !");
            CentrerLeTexte(" \n (oui oui je parle à toi, pas à Tom, Tom n'aurait pas écrit un message comme ça à sa propre destination :p xD");
            CentrerLeTexte(" \n\n PUTAIIIIIN MAIS TU VA CLIQUER OUI OU MERDE POUR FAIRE DEMARER CE PUTAIN DE BAIL TROP STYLE !");

            Console.ReadKey();
            CentrerLeTexte("CA DEMARREEEEEEEEEEEEEEEEEE !!! YES PUTAIN J'ADORE CHAUFFER !");
            LectureEcriture(fichierEntree, fichierSortie);

            CentrerLeTexte("ET C'EST FINI !!!! ON VA BIEN VOIR SI TOM A FAIT DU SACRE BON BOULOT PUTAIN !");

        }
    }
}
