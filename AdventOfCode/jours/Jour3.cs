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
    internal class Jour3
    {

        List<string> data = new List<string>();

        public void Exec()
        {

            string path = "input/input_3.txt";
            //path = "input/input_example_3.txt";

            

            //mise en mémoire
            using (FileStream fs = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(fs, Encoding.UTF8, true, 128))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)  //foreach line
                    {
                        data.Add(line);
                    }                    
                }           
            }


            string current_voisinage = "";
            string current_number = "";
            int ligne = 0;
            int colonne = 0;
            int somme = 0;

            foreach (string line in data)
            {
                foreach(char c in line)
                {

                    if(Char.IsDigit(c))
                    {
                        current_number = current_number + c;
                        current_voisinage = current_voisinage + get_voisins(data, ligne, colonne);
                    }
                    else
                    {
                        //fin du nombre
                        if(current_number != "")
                        {
                            if(contient_spchar(current_voisinage))
                            {
                                somme = somme + Int32.Parse(current_number);
                            }
                        }
                        
                        
                        current_number = "";
                        current_voisinage = "";
                    }

                    colonne++;
                }
                ligne++;
                colonne = 0;
            }

            Console.WriteLine(somme);


            //PARTIE 2

            ligne = 0;
            colonne = 0;
            somme = 0;
            List<int?> nbr_voisins = new List<int?>();
            List<int> nbr_voisins_not_null = new List<int>();

            foreach (string line in data)
            {
                foreach (char c in line)
                {

                    if (c == '*')
                    {
                        nbr_voisins = get_nbr_voisins(data, ligne, colonne);
                        nbr_voisins_not_null.Clear();

                        foreach (int v in nbr_voisins.Where(x => x != null))
                        {
                            if(!nbr_voisins_not_null.Contains(v))   nbr_voisins_not_null.Add(v);
                        }

                        if (nbr_voisins_not_null.Count == 2)
                        {

                            int nbr1 = nbr_voisins_not_null.Min();
                            int nbr2 = nbr_voisins_not_null.Max();


                            somme = somme + (nbr1 * nbr2);
                        }
                    }

                    colonne++;
                }
                ligne++;
                colonne = 0;
            }

            Console.WriteLine(somme);


        }

        public string get_voisins(List<string> data, int l, int c)
        {
            string voisins = "";

            //ligne du dessus
            if(l > 0)
            {
                if (c > 0)
                {
                    voisins = voisins + (data[l - 1][c - 1]);
                }
                voisins = voisins + (data[l - 1][c]);
                if (c < data.First().Count()-1)
                {
                    voisins = voisins + (data[l - 1][c + 1]);
                }
            }

            //ligne du dessous
            if(l < data.Count-1)
            {
                if (c > 0)
                {
                    voisins = voisins + (data[l + 1][c - 1]);
                }
                voisins = voisins + (data[l+1][c]);
                if (c < data.First().Count()-1)
                {
                    voisins = voisins + (data[l + 1][c + 1]);
                }
            }

            //droite
            if(c < data.First().Count()-1)
            {
                voisins = voisins + (data[l][c + 1]);
            }

            //gauche
            if(c > 0)
            {
                voisins = voisins + (data[l][c-1]);
            }

            return voisins;
        }

        public List<int?> get_nbr_voisins(List<string> data, int l, int c)
        {
            List<int?> nbr_voisins = new List<int?>();

            //ligne du dessus
            if (l > 0)
            {
                if (c > 0)
                {
                    nbr_voisins.Add(get_nbr(data,l - 1,c - 1));
                }
                nbr_voisins.Add(get_nbr(data, l - 1, c));
                if (c < data.First().Count() - 1)
                {
                    nbr_voisins.Add(get_nbr(data, l - 1, c + 1));
                }
            }

            //ligne du dessous
            if (l < data.Count - 1)
            {
                if (c > 0)
                {
                    nbr_voisins.Add(get_nbr(data, l + 1, c - 1));
                }
                nbr_voisins.Add(get_nbr(data, l + 1, c));
                if (c < data.First().Count() - 1)
                {
                    nbr_voisins.Add(get_nbr(data, l + 1, c + 1));
                }
            }

            //droite
            if (c < data.First().Count() - 1)
            {
                nbr_voisins.Add(get_nbr(data, l, c + 1));
            }

            //gauche
            if (c > 0)
            {
                nbr_voisins.Add(get_nbr(data, l, c - 1));
            }

            return nbr_voisins;
        }

        public int? get_nbr(List<string> data, int l, int c)
        {
            string nbr = "";
            int i = 1;

            if (Char.IsDigit(data[l][c]))
            {
                while (c - i >= 0 && Char.IsDigit(data[l][c - i]))
                {
                    nbr = data[l][c - i] + nbr;
                    i++;
                }
                i = 0;
                while (c + i < data[0].Count() && Char.IsDigit(data[l][c + i]))
                {
                    nbr = nbr + data[l][c + i];
                    i++;
                }

                return Int32.Parse(nbr);
            }
            else
            {
                return null;
            }

            

        }

        public bool contient_spchar(string s)
        {
            foreach(char c in s)
            {
                if (!Char.IsDigit(c) && !c.Equals('.')) return true;
            }
            return false;
        }



    }
}
