using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.jours
{
    internal class Jour2
    {
        public void Exec()
        {

            string path = "input/input_2.txt";
            //path = "input/input_example_2.txt";

            int nb_max_red = 12;
            int nb_max_green = 13;
            int nb_max_blue = 14;

            int somme = 0;


            using (FileStream fs = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(fs, Encoding.UTF8, true, 128))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)  //foreach game
                    {
                        bool valid = true;
                        int game_id = Int32.Parse(line.Split(':')[0].Split(" ")[1]);
                        string[] tirages = line.Split(":")[1].Split(";");

                        foreach(string tirage in tirages)
                        {
                            string[] mains = tirage.Split(",");

                            foreach(string m in mains)
                            {
                                string couleur = m.Split(" ")[2];
                                int nombre = Int32.Parse(m.Split(" ")[1]);

                                if(couleur == "red" && nombre > nb_max_red) valid = false;
                                if(couleur == "green" && nombre > nb_max_green) valid = false;
                                if(couleur == "blue" && nombre > nb_max_blue) valid = false;
                            }
                        }

                        if(valid)
                        {
                            somme = somme + game_id;
                        }
                        
                    }
                    Console.WriteLine("\nSomme des ID des jeux possibles : " + somme);
                }           
            }

            somme = 0;

            using (FileStream fs = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(fs, Encoding.UTF8, true, 128))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)  //foreach game
                    {

                        int max_red = 0;
                        int max_green = 0;
                        int max_blue = 0;

                        bool valid = true;
                        int game_id = Int32.Parse(line.Split(':')[0].Split(" ")[1]);
                        string[] tirages = line.Split(":")[1].Split(";");

                        foreach (string tirage in tirages)
                        {
                            string[] mains = tirage.Split(",");

                            foreach (string m in mains)
                            {
                                string couleur = m.Split(" ")[2];
                                int nombre = Int32.Parse(m.Split(" ")[1]);

                                if (couleur == "red" && nombre > max_red) max_red = nombre;
                                if (couleur == "green" && nombre > max_green) max_green = nombre;
                                if (couleur == "blue" && nombre > max_blue) max_blue = nombre;
                            }
                        }

                        if (valid)
                        {
                            somme = somme + (max_blue*max_green*max_red);
                        }
                        Console.WriteLine(somme);
                    }
                    Console.WriteLine("\nSomme des multiplications des nbr de couleurs maximum : " + somme);
                }
            }
        }
    }
}
