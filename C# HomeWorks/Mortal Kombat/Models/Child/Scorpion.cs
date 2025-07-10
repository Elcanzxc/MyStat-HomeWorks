using Mortal_Kombat.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortal_Kombat.Models.Child
{
    class Scorpion : Fighter
    {

        public override string Name => "Scorpion";
        public override int MaxHealth => 110;
        public override int Attack => 30;
        public override int Defense => 12;

         public override string AsciiArt => @"
  ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣤⣤⣤⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⠿⠿⣿⣿⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⡷⣶⠗⡹⢻⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠿⡒⣽⣮⣿⣿⣧⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠀⠀⠀⢸⣥⣻⣾⣿⣿⣿⠛⠛⠳⢶⣄⣠⠄⣀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⠖⠋⢩⣿⣿⣿⣿⣿⣿⢁⠀⣀⢀⣶⢟⡷⢾⠏⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠠⣿⣤⣮⣾⣿⣿⣿⣿⡟⠙⠛⠷⣶⣿⡵⠋⠙⡆⠆⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢃⡏⠀⠠⣿⣿⣿⣿⣿⠀⠀⠀⣴⣟⣮⣀⠀⣸⢣⡥⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠈⢷⢀⣰⣿⣿⣿⣿⡷⢧⣶⣨⣯⣾⡇⠋⠙⠳⡟⡇⠆⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⣦⠸⠛⠉⣿⣿⣿⣿⠁⠀⠀⣿⣽⡯⣇⢁⠀⠀⠘⣿⠆⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢁⠀⡆⠀⣿⣿⣿⣿⠀⠀⠀⢻⡯⣄⠝⢇⠆⡀⠠⣿⣞⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠠⠺⣫⣇⠂⢿⣿⣿⣿⠈⠀⢠⣿⣿⡇⠀⠈⠱⣵⢾⠏⣞⡃⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⢀⣟⣮⣽⣸⡮⠿⠿⠿⠿⠦⠭⠾⢿⠛⡇⠀⠀⢀⣻⢿⡶⠶⢿⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠈⣿⣾⣶⣽⢿⣿⣿⣿⣿⣿⣿⣿⣿⡐⣵⠀⠀⠈⣾⡞⠀⣄⡡⡃⠀⠀
  ⠀⠀⠀⠀⠀⠀⢼⣿⣿⡿⢫⣮⢻⡝⠉⠉⢹⡏⢩⣵⣾⡣⢆⠀⠀⠸⣿⢄⡟⡗⡇⠀⠀
  ⠀⠀⠀⠀⠀⠀⢿⣻⣯⠇⢸⣿⣾⡇⠀⠊⢌⣇⣿⣿⣿⣧⢡⢁⠀⠀⣿⢨⡇⣧⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⣿⡉⣹⣆⣿⣿⣿⡇⠀⠀⠠⣿⣿⣿⣿⣿⣆⢻⠀⣠⣿⣿⣷⡛⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠿⣷⡿⣿⣿⣿⣿⡇⠀⠀⠀⣿⣿⣿⣿⣿⣿⡄⡖⣑⢿⠉⣹⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⡇⠀⠀⠈⢹⣿⣿⣿⣿⣿⣷⡘⡡⠿⠖⠋⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⠧⠀⢀⠀⢸⣿⣿⣿⣿⣿⣿⣖⡁⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣠⣄⡂⠐⣺⣿⣿⣿⣿⣿⣿⣷⡆⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣿⣿⣿⣿⣿⠃⠀⠀⢿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⠃⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⡟⠀⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠸⣿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠈⣍⣿⣿⣿⣇⠀⠀⠀⠀⠀⠀⠀⠀⠙⣿⢿⣉⣀⠻⣦⠀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⢸⠁⠘⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⢠⢹⣿⠈⠘⡆⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠀⠃⢀⢈⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣏⡄⠙⣏⠀⡀⠀⠀⠀
  ⠀⠀⠀⠀⠀⠀⠀⠠⣇⢦⣿⣿⠟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢦⠝⣟⣧⠈⠀⠀⠀
  "; 
     
        public Scorpion() : base()
        {

        }


        public override void PerformAttack(Fighter enemy)
        {
            Console.WriteLine($"{this.Name} throws a fiery spear at {enemy.Name}!");
            enemy.TakeDamage(Attack);

            this.XRayCharge += 15;
            if (this.XRayCharge > 100)
                this.XRayCharge = 100;
        }

        public override void PerformXRay(Fighter enemy)
        {
            if (!this.CanUseXRay)
            {
                Console.WriteLine($"{this.Name} unleashes a brutal Hellfire X-Ray on {enemy.Name}!");
                return;
            }

            Console.WriteLine($"{this.Name} unleashes a devastating Thunder X-Ray!");
            enemy.TakeDamage(this.Attack * 2);

            this.XRayCharge = 0;
        }

        public override void PerformFatality(Fighter enemy)
        {
            if (enemy.IsAlive)
            {
                Console.WriteLine($"Cannot perform Fatality! {enemy.Name} is still standing.");
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{this.Name} performs FATALITY on {enemy.Name}!");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥");
            Console.WriteLine("   🔥  GET OVER HERE! 🔥");
            Console.WriteLine("   🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   █████████");       
            Console.WriteLine("   ██ 😱 ██ 😱 ██");
            Console.WriteLine("   █████████");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("   🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥");
            Console.WriteLine("   🔥   BURN!!!   🔥");
            Console.WriteLine("   🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("   (Ashes falling...)");
            Console.WriteLine("   ☁️  ☁️  ☁️");
            Console.WriteLine("   ⚫   ⚫   ⚫");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            Console.WriteLine("   🔥 SCORPION WINS 🔥");
            Console.WriteLine("   ☠ Fatality ☠");
            Console.ResetColor();
        }


    }
}
