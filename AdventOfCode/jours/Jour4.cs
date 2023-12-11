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
    internal class Jour4
    {

        List<string> data = new List<string>();

        public void Exec()
        {

            string path = "input/input_4.txt";
            //path = "input/input_example_4.txt";

            

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

            int somme = 0;


            foreach (string card in data) 
            {
                int game_sum = 0;
                List<string> winning_nbr = card.Split('|')[0].Split(':')[1].Split(' ').ToList();
                List<string> game_nbr = card.Split('|')[1].Split(' ').ToList();

                foreach(string n in game_nbr.Where(x => winning_nbr.Contains(x) && x.Length > 0))
                {
                    
                    if(game_sum == 0)
                    {
                        game_sum++;
                    } else
                    {
                        game_sum *= 2;
                    }
                }


                somme += game_sum;

            }

            Console.WriteLine(somme);




            //PARTIE 2
            int nbr_cards = 0;

            foreach(var original_card in data)
            {
                nbr_cards += get_cards_number(original_card);
            }


            Console.WriteLine(nbr_cards);
        }



        public int get_cards_number(string card)
        {
            int total_nbr = 1;
            int nbr_match = 0;
            int card_id = Int32.Parse(card.Split('|')[0].Split(':')[0].Trim().Split('d')[1]);

            List<string> winning_nbr = card.Split('|')[0].Split(':')[1].Split(' ').ToList();
            List<string> game_nbr = card.Split('|')[1].Split(' ').ToList();

            foreach (string n in game_nbr.Where(x => winning_nbr.Contains(x) && x.Length > 0))
            {
                nbr_match++;
            }

            for(var i = card_id; i < card_id+nbr_match; i++ )
            {
                total_nbr += get_cards_number(data[i]);
            }




            return total_nbr;
        }


    }
}
