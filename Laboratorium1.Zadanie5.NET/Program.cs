namespace Laboratorium1.Zadanie5.NET
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Wybierz opcję:\n1. Zapisz dane użytkownika.\n2. Odczytaj dane użytkownika.");
            var option = Console.ReadLine();

            if (option == "1")
            {
                HandleSaveDataOption();
            }
            else if (option == "2")
            {
                HandleReadDataOption();
            }
            else
            {
                Console.WriteLine("Nieznana opcja.");
            }
        }

        static void HandleSaveDataOption()
        {
            Console.WriteLine("Podaj imię:");
            var imie = Console.ReadLine();

            int wiek;
            while (true)
            {
                Console.WriteLine("Podaj wiek:");
                var wiekStr = Console.ReadLine();

                if (int.TryParse(wiekStr, out wiek) && wiek > 0)
                {
                    break;
                }

                Console.WriteLine("Wiek powinien być liczbą całkowitą, większą od zera.");
            }

            Console.WriteLine("Podaj adres:");
            var adres = Console.ReadLine();

            using var fs = new FileStream("data.bin", FileMode.Create);
            using var bw = new BinaryWriter(fs);

            bw.Write(CheckNull(imie));
            bw.Write(wiek);
            bw.Write(CheckNull(adres));

            Console.WriteLine("Dane zostały zapisane.");
        }

        static void HandleReadDataOption()
        {
            if (File.Exists("data.bin"))
            {
                using var fs = new FileStream("data.bin", FileMode.Open);
                if (fs.Length > 0)
                {
                    using var br = new BinaryReader(fs);

                    var imie = br.ReadString();
                    var wiek = br.ReadString();
                    var adres = br.ReadString();

                    Console.WriteLine($"Imię: {imie}");
                    Console.WriteLine($"Wiek: {wiek}");
                    Console.WriteLine($"Adres: {adres}");
                }
                else
                {
                    Console.WriteLine("File is empty.");
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }

        static string CheckNull(string? input)
        {
            return input ?? string.Empty;
        }
    }
}