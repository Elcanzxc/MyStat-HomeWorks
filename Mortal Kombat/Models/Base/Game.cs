using Mortal_Kombat.Models.Child;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortal_Kombat.Models.Base
{
    class Game
    {

        private Fighter player1;
        private Fighter player2;

        private BattleManager battleManager;


        private Fighter[] allFighters;

        public static void DrawFight(string leftArt, string rightArt, int spacing = 10)
        {
            Console.SetBufferSize(Console.BufferWidth, 100);
            var leftLines = leftArt.Split('\n');
            var rightLines = rightArt.Split('\n');

            int maxLines = Math.Max(leftLines.Length, rightLines.Length);
            int leftX = 0;
            int topY = Console.CursorTop;
            int rightX = leftLines.Max(line => line.Length) + spacing;

            for (int i = 0; i < maxLines; i++)
            {
               
                if (i < leftLines.Length)
                {
                    Console.SetCursorPosition(leftX, topY + i);
                    Console.Write(leftLines[i]);
                }

             
                if (i < rightLines.Length)
                {
                    Console.SetCursorPosition(rightX, topY + i);
                    Console.Write(rightLines[i]);
                }
            }

            Console.SetCursorPosition(0, topY + maxLines); 
        }
        public void LoadFighters()
        {
            allFighters = new Fighter[3];
            allFighters[0] = new Scorpion();
            allFighters[1] = new SubZero();
            allFighters[2] = new Raiden();
        } 
        public void ShowMainMenu()
        {
            Console.WriteLine(@"
                                  _           _                            _             _   
                       /\/\    ___   _ __ | |_   __ _ | |   /\ /\  ___   _ __ ___  | |__    __ _ | |_ 
                      /    \  / _ \ | '__|| __| / _` || |  / //_/ / _ \ | '_ ` _ \ | '_ \  / _` || __|
                     / /\/\ \| (_) || |   | |_ | (_| || | / __ \ | (_) || | | | | || |_) || (_| || |_ 
                     \/    \/ \___/ |_|    \__| \__,_||_| \/  \/  \___/ |_| |_| |_||_.__/  \__,_| \__|
                                                                                 
            ");

            Console.WriteLine();
            Console.WriteLine("\t\t\t\t Welcome to Elcan's Console Version Mortal Kombat!");
            Console.WriteLine("\t\t\t\t Prepare for battle... Only one will survive!");
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t Controls will be available during the fight.");
            Console.WriteLine("\t\t\t\t Use numbers to choose attacks and activate X-Ray.");
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t Press any key to start the game...");

            Console.ReadKey();
        }
        public void SelectFighters()
        {
            Console.Clear();
            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t\t\t\t ==== Fighter Selection ====\n");

            for (int i = 0; i < allFighters.Length; i++)
            {
                Console.WriteLine($"\t\t\t\t\t {i + 1}) {allFighters[i].Name}");
            }

            Console.Write($"\n\t\t\t\t\t Choose your fighter (1-{allFighters.Length}): ");

            int choice = 0;
            while (choice < 1 || choice > allFighters.Length)
            {
                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice) || choice < 1 || choice > allFighters.Length)
                {
                    Console.WriteLine("\t\t\t\t\t Invalid choice! Please try again");
                }
            }

            player1 = allFighters[choice - 1];

            Random rnd = new Random();
            int enemyIndex;
            do
            {
                enemyIndex = rnd.Next(0, allFighters.Length);
            } while (enemyIndex == choice - 1);

            player2 = allFighters[enemyIndex];

            Console.OutputEncoding = Encoding.UTF8;

            Console.Clear();
            Console.WriteLine("Your Fighter: \t\t\t VS \t\t Enemy Fighter:");
            Console.WriteLine($"{player1.Name} \t\t\t\t\t {player2.Name} ");
            DrawFight(player1.AsciiArt, player2.AsciiArt);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        public void Run()
        {
            LoadFighters();
            ShowMainMenu();
            SelectFighters();  

            battleManager = new BattleManager(player1, player2);
            battleManager.RunBattle();

            Console.WriteLine("Game Over! Thanks for playing.");
        }




    }
}
