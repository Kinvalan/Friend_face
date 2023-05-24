//TODO: lage en Bruker klasse X
//TODO: lage en "hovedbruker" når programmet starter, som er "innlogget" X
//TODO: lage en rekke andre brukere X

//Kommandoer:
//TODO: kunne velge en av brukerene og printe ut profilinformasjonen deres X
//TODO: AddFriend-metode  X
//TODO: RemoveFriend-metode  X

//TODO: Printe ut en liste av alle man har lagt til som venn  (WIP)

namespace Friend_face
{
    internal class Program
    {
        static User? LoggedInUser;
        static List<User>? AllUsers;
        static ConsoleStage ConsoleStage = ConsoleStage.Start;

        static void Main(string[] args)
        {
            LoggedInUser = new User("Admin", 2147483647, "Localhost", "Internet", "beep boop", "Admin");
            FillUserList();
            Start();
        }

        private static void Start()
        {
            Console.Clear();
            ConsoleStage = ConsoleStage.Start;
            HelpText();

            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) Start();

            if (input!.ToLower() == "print")
            {
                string output = string.Empty;
                foreach (var user in AllUsers!)
                {
                    output += user.Name + " " + user.Age + "\n";
                }

                List(output);
            }
            else if (input!.ToLower() == "exit") Environment.Exit(0);
            else Start();
        }

        private static void List(string Userlist)
        {
            Console.Clear();
            ConsoleStage = ConsoleStage.List;
            HelpText();
            Console.WriteLine("\n\n" + Userlist);

            var input = Console.ReadLine();
            string output = string.Empty;

            if (string.IsNullOrEmpty(input)) List(Userlist);

            if (input!.ToLower() == "back") Start();

            PersonSelected(ref input);


            //output += user.Name + " " + user.Age + " " + user.City + " " + user.Country + "\n" + @"    " + user.Bio + "\n\n";
        }

        private static void PersonSelected(ref string input)
        {
            while(true)
            {
                Console.Clear();
                ConsoleStage = ConsoleStage.PersonSelected;
                HelpText();
                Console.WriteLine("\n\n");

                string output = string.Empty;

                foreach (var user in AllUsers!)
                {
                    if (input.ToLower() == user.Name.ToLower())
                    {
                        output += user.Name + " " + user.Age + " " + user.City + " " + user.Country + "\n" + @"    " + user.Bio + "\n\n";
                    }
                }
                Console.WriteLine(output);

                // For å kjøre metodene som kan fjerne eller legge til en venn.

                string actionInput = Console.ReadLine(); 


                if (actionInput.ToLower() == "add" && addFriend(input))
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                else if (actionInput.ToLower() == "remove")
                {
                    removeFriend(input);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                else if (actionInput.ToLower() == "back")
                {
                    Start();
                }
            }

        }

        private static void HelpText()
        {
            Console.WriteLine("Available commands:");
            switch (ConsoleStage)
            {
                case ConsoleStage.Start:
                    Console.WriteLine("    Print - prints a list of all users on the site");
                    Console.WriteLine("    Exit  - exits the application");
                    break;
                case ConsoleStage.List:
                    Console.WriteLine("    Back           - Go back to previous stage");
                    Console.WriteLine("    [name of user] - Prints information about the selected user");
                    break;
                case ConsoleStage.PersonSelected:
                    Console.WriteLine("    Back   - Go back to previous stage");
                    Console.WriteLine("    Add    - Adds the person to your friendslist");
                    Console.WriteLine("    Remove - Removes the person to your friendslist");
                    break;
            }
        }

        private static void FillUserList()
        {
            AllUsers = new()
            {
                new User("Mikkel", 32, "Tønsberg", "Norway", "Hei hei", "mikkelRev"),
                new User("Jonas", 21, "Madrid", "Spain", "Hola", "sombrero"),
                new User("Jack", 45, "Texas", "USA", "Howdy", "cowboy"),
                new User("Walter", 62, "Alberqueque", "USA", "We need to cook", "Heisenberg"),
                new User("Jesse", 28, "Alberqueque", "USA", "Ayo", "Bitch"),
                new User("Sebastian", 22, "Larvik", "Norway", "Skate 4 when? :(", "passord"),
                new User("Eskil", 46, "Stavern", "Norway", "Har du ikke et growth mindset blocker jeg deg", "passord"),
                new User("Theodor", 26, "Larvik", "Norway", "Skulle ønske jeg var en like god kodelærer som Stian :(", "passord"),
                new User("Stian", 25, "Tønsberg", "Norway", "Hvorfor er det en d i fridge men ikke i refrigerator?", "passord"),
                new User("Terje", 55, "Stavern", "Norway", "if > switch", "passord")
            };
        }

        static bool addFriend(string input)
        {
            Console.Clear();
            ConsoleStage = ConsoleStage.PersonSelected;
            HelpText();


            /*
            Finn brukeren som skal legges til som venn
            
            Lambda-uttrykket sjekker om navnet til hver bruker er likt det brukeren har skrevet inn.
            Hvis det finnes en bruker med det samme navnet i listen "AllUsers", 
            vil den brukeren bli tildelt til "selectedUser" variabelen. 
            Hvis det ikke finnes en sånn bruker, vil "selectedUser" være null.

            "?" i lambda uttrykket er en del av null-forebyggende operatører i C#. 
            Det brukes for å unngå en nullreferanse-feil når du prøver å få tilgang til en egenskap eller
            metode på et objekt som kan være null.
            */

            User selectedUser = AllUsers?.FirstOrDefault(user => user.Name.ToLower() == input.ToLower());

            if(selectedUser == null)
            {
                Console.WriteLine("User not found.");
                return false;
            }

            // Sjekk om brukeren allerede er en venn

            if(LoggedInUser.Friends.Contains(selectedUser))
            {
                Console.WriteLine("The selected user is already one of your friends.");
                return false;
            }

            // Legg til bruker som venn

           Console.WriteLine("\n\n");
           LoggedInUser.Friends.Add(selectedUser);

           Console.WriteLine(selectedUser.Name.ToLower() + " has been added as a friend!");
           Console.WriteLine("You now have " + LoggedInUser.Friends.Count + " friend(s).");

           Console.WriteLine("Friends: ");
           foreach (var user in LoggedInUser.Friends)
           {
               Console.WriteLine(user.Name);
           }
           return true;
        }

        // Fjern bruker som venn

        /*
        Eventuelle endringer som gjøres inne i metoden med ref i parameteret, 
        vil påvirke den orginale variabelen utenfor metoden, 
        og den vil inneholde den nye verdien vi gir den i metoden med ref når vi bruker parameteret.
        */

        static void removeFriend(string input)
        {
            Console.Clear();
            ConsoleStage = ConsoleStage.PersonSelected;
            HelpText();
            Console.WriteLine("\n\n");

            User selectedUser = AllUsers?.FirstOrDefault(user => user.Name.ToLower() == input.ToLower());

            if (selectedUser == null) // Hvis brukernavnet ikke finnes 
            {
                Console.WriteLine("User not found.");
                return;
            }

            if (!LoggedInUser.Friends.Contains(selectedUser)) // Hvis brukeren ikke er vennen din kan du ikke fjerne brukeren
            {
                Console.WriteLine("You can't unfriend someone that isn't already your friend.");
                return;
            }

            else     // Hvis brukeren finnes, er vennen din og du velger å fjerne denne personen
            { 
                LoggedInUser.Friends.Remove(selectedUser);

                Console.WriteLine("You and " + selectedUser.Name + " are now no longer friends. :(");
                Console.WriteLine("You now have " + LoggedInUser.Friends.Count + " friend(s).");
                Console.WriteLine("Friends: " + selectedUser.Name);
            }
            
        }
    }

    enum ConsoleStage
    {
        Start,
        List,
        PersonSelected
    }
}