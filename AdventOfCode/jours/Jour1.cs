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
    internal class Jour1
    {
        public void Exec()
        {

            string path = "input/input_1.txt";
            //path = "input/input_example_1.1.txt";
            int somme1 = 0;
            int somme2 = 0;

            using (FileStream fs = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(fs, Encoding.UTF8, true, 128))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        int num;
                        string res = new String(line.Where(Char.IsDigit).ToArray());
                        if (res.Length > 0)
                        {
                            num = Int32.Parse(res.First() + "" + res.Last());
                        } else
                        {
                            num = 0;
                        }
                        somme1 = somme1 + num;
                        
                    }
                }
                Console.WriteLine("\nsomme chiffres " + somme1.ToString());

                
            }

            //path = "input/input_example_1.2.txt";

            using (FileStream fs = File.OpenRead(path))
            {
                using (var streamReader2 = new StreamReader(fs, Encoding.UTF8, true, 128))
                {
                    String line;
                    while ((line = streamReader2.ReadLine()) != null)
                    {
                        string temp = "";
                        int num;
                        foreach (char c in line)
                        {
                            temp = temp + c;
                            temp = temp.Replace("one", "1");
                            temp = temp.Replace("two", "2");
                            temp = temp.Replace("three", "3");
                            temp = temp.Replace("four", "4");
                            temp = temp.Replace("five", "5");
                            temp = temp.Replace("six", "6");
                            temp = temp.Replace("seven", "7");
                            temp = temp.Replace("eight", "8");
                            temp = temp.Replace("nine", "9");
                        }
                        string res1 = new String(temp.Where(Char.IsDigit).ToArray());

                        temp = "";
                        foreach (char c in line.Reverse())
                        {
                            temp = c + temp;
                            temp = temp.Replace("one", "1");
                            temp = temp.Replace("two", "2");
                            temp = temp.Replace("three", "3");
                            temp = temp.Replace("four", "4");
                            temp = temp.Replace("five", "5");
                            temp = temp.Replace("six", "6");
                            temp = temp.Replace("seven", "7");
                            temp = temp.Replace("eight", "8");
                            temp = temp.Replace("nine", "9");
                        }
                        string res2 = new String(temp.Where(Char.IsDigit).ToArray());

                        if (res1.Length > 0 && res2.Length >0)
                        {
                            num = Int32.Parse(res1.First() + "" + res2.Last());
                        }
                        else
                        {
                            num = 0;
                        }
                        somme2 = somme2 + num;

                    }
                }
                Console.WriteLine("\nsomme chiffres&lettres " + somme2.ToString());
            }

        }
    }
}
