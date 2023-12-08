using AdventOfCode.jours;


namespace AdventOfCode
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Bienvenue sur l'Advent Of Code de Lucas \nTape le numéro d'un jour entre 1 et 25");
            string day = Console.ReadLine();

            while (!IsValid(day))
            {
                Console.WriteLine("Saisie invalide");
                day = Console.ReadLine();
            }

            Console.Clear();

            switch (day)
            {
                case "1":
                    Jour1 jour1 = new Jour1();
                    jour1.Exec();
                    break;
                case "2":
                    Jour2 jour2 = new Jour2();
                    jour2.Exec();
                    break;
                case "3":
                    Jour3 jour3 = new Jour3();
                    jour3.Exec();
                    break;
                default:
                    Console.WriteLine("Jour pas encore disponible");
                    break;
            }

        }


        public static bool IsValid(string value)
        {
            if (value.All(char.IsNumber) && Int32.Parse(value)>0 && Int32.Parse(value)<26) return true; 
            else return false;
        }
    }
}