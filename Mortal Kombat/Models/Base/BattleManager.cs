

namespace Mortal_Kombat.Models.Base
{
    public class BattleManager
    {
        private Fighter player1;
        private Fighter player2;

        public BattleManager(Fighter player1, Fighter player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public void RunBattle()
        {
            Console.Clear();
            Console.WriteLine("== Battle Start ==");
            Console.WriteLine($"{this.player1.Name} VS {this.player2.Name}\n");
            Console.WriteLine("Press any key to begin the fight...");
            Console.ReadKey();

            while (this.player1.IsAlive && this.player2.IsAlive)
            {
                Console.Clear();
                DrawFight(player1.AsciiArt, player2.AsciiArt);
                DisplayStats();

                Console.WriteLine($"\n{this.player1.Name}'s turn!");
                PlayerTurn(this.player1, this.player2);

                if (!this.player2.IsAlive)
                {
                    break;
                }

                Console.Clear();
                DrawFight(player1.AsciiArt, player2.AsciiArt);
                DisplayStats();

                Console.WriteLine($"\n{this.player2.Name}'s turn!");
                PlayerTurn(this.player2, this.player1);
            }

            Console.Clear();
            DrawFight(player1.AsciiArt, player2.AsciiArt);
            Console.WriteLine($"\n== GAME OVER ==");
            Console.WriteLine($"{(this.player1.IsAlive ? this.player1.Name : this.player2.Name)} wins!");

            Fighter winner = this.player1.IsAlive ? this.player1 : this.player2;
            Fighter loser = this.player1.IsAlive ? this.player2 : this.player1;

            winner.PerformFatality(loser);
        }

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




        private void DisplayStats()
        {
            Console.WriteLine("=== Fighters Stats ===\n");

            DisplaySingleStats(player1);
            Console.WriteLine();
            DisplaySingleStats(player2);
        }

        private void DisplaySingleStats(Fighter fighter)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; 
            int hpBarLength = 20;     
            int xrayBarLength = 10;  

          
            int filledHP = (int)Math.Round((double)fighter.CurrentHP / fighter.MaxHealth * hpBarLength);
            int emptyHP = hpBarLength - filledHP;

          
            int filledXRay = (int)Math.Round((double)fighter.XRayCharge / 100 * xrayBarLength);
            int emptyXRay = xrayBarLength - filledXRay;

            
            Console.WriteLine($"Name   : {fighter.Name}");

            Console.WriteLine($"HP     : {fighter.CurrentHP} / {fighter.MaxHealth}");
            Console.Write("         ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(new string('■', filledHP));
            Console.ResetColor();
            Console.WriteLine(new string('□', emptyHP));

            Console.WriteLine($"X-Ray  : {fighter.XRayCharge}%");
            Console.Write("         ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(new string('■', filledXRay));
            Console.ResetColor();
            Console.WriteLine(new string('□', emptyXRay));

            Console.WriteLine($"Status : {(fighter.IsAlive ? "Alive" : "Dead")}");
        }


        private void PlayerTurn(Fighter attacker, Fighter defender)
        {
            Console.WriteLine($"{attacker.Name}, it's your turn!");
            Console.WriteLine("Choose an action:");

            Console.WriteLine("1 - Basic Attack");

            if (attacker.XRayCharge >= 100)
            {
                Console.WriteLine("2 - X-Ray Attack");
            }

            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    attacker.PerformAttack(defender);
                    break;

                case "2":
                    if (attacker.XRayCharge >= 100)
                    {
                        attacker.PerformXRay(defender);
                        attacker.XRayCharge = 0;
                    }
                    else
                    {
                        Console.WriteLine("X-Ray not ready! Performing basic attack instead.");
                        attacker.PerformAttack(defender);
                    }
                    break;

                default:
                    Console.WriteLine("Invalid input. Performing basic attack.");
                    attacker.PerformAttack(defender);
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


    }

}