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
    internal class Jour5
    {
        //Données d'entrées
        List<seed> seeds = new List<seed>();

        public void Exec()
        {

            string path = "input/input_5.txt";
            //path = "input/input_example_5.txt";


            List<corresp> current_map = new List<corresp>();

            //stockage
            using (FileStream fs = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(fs, Encoding.UTF8, true, 128))
                {
                    int compteur = -1;
                    Int64 last_nbr = -1;


                    String line;
                    while ((line = streamReader.ReadLine()) != null)  //foreach line
                    {
                        //creation seed
                        if(line.Contains("seeds"))
                        {
                            foreach(var seed in line.Split(':')[1].Split(" ").Where(x => x != " ")) 
                            {
                                if (seed.Length > 0)
                                {
                                    if (last_nbr == -1)
                                    {
                                        last_nbr = Int64.Parse(seed.Trim());
                                    }
                                    else
                                    {
                                        Parallel.For(last_nbr, last_nbr + Int64.Parse(seed.Trim()), index =>
                                        {
                                            seed ns = new seed();
                                            ns.values = new List<Int64> { -1, -1, -1, -1, -1, -1, -1 };
                                            ns.number = index;
                                            seeds.Add(ns);
                                        }) ;
                                    }
                                }
                            }
                            continue;

                        }

                        //on traite les inputs et on reset la current_map
                        if(line.Contains("map"))
                        {
                            if(current_map.Count > 0)
                            {
                                traiter(current_map, compteur);
                            }

                            current_map = new List<corresp>();
                            compteur++;
                            continue;
                        }

                        //creation corresp
                        if(line.Length>0)
                        {
                            corresp c = new corresp();
                            c.src_start = Int64.Parse(line.Split(' ')[1]);
                            c.dst_start = Int64.Parse(line.Split(' ')[0]);
                            c.size = Int64.Parse(line.Split(' ')[2]);
                            current_map.Add(c);
                            continue;
                        }
                        
                        
                    }

                    traiter(current_map, compteur);
                }           
            }


            Console.WriteLine(seeds.Min(x => x.values[6]));

        }


        public void traiter(List<corresp> map,  int compteur)
        {

            Parallel.ForEach(seeds, seed =>
            {
                Parallel.ForEach(map, corresp =>
                {
                    if (compteur == 0)
                    {
                        if (seed.number > corresp.src_start && seed.number < corresp.src_start + corresp.size)
                        {
                            seed.values[compteur] = corresp.dst_start + (seed.number - corresp.src_start);
                            //continue;
                        }
                    }
                    else
                    {
                        if (seed.values[compteur - 1] >= corresp.src_start && seed.values[compteur - 1] < corresp.src_start + corresp.size)
                        {
                            seed.values[compteur] = corresp.dst_start + (seed.values[compteur - 1] - corresp.src_start);
                            //continue;
                        }

                    }
                });
                if (seed.values[compteur] == -1)
                {
                    if (compteur == 0)
                    {
                        seed.values[compteur] = seed.number;
                    }
                    else
                    {
                        seed.values[compteur] = seed.values[compteur - 1];
                    }
                }
            });
        }




    }


    internal class corresp
    {
        public Int64 src_start { get; set; }
        public Int64 dst_start {  get; set; }
    public Int64 size { get; set; }
}

    internal class seed
    {
        public Int64 number {  get; set; }
        public List<Int64> values { get; set; }


    }
}
