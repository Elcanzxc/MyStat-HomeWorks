using Mortal_Kombat.Models.Base;

namespace Mortal_Kombat.Models.Child
{
    class SubZero : Fighter
    {
        public override string Name => "SubZero";
        public override int MaxHealth => 140;
        public override int Attack => 20;
        public override int Defense => 15;

        public override string AsciiArt => @"
⠀⠀⠀⠀⠀⠀⢀⡀⠀⠀⠀⠀⠀⢠⣤⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣤⣄⡀⠀⠀⠀⠙⣿⡯⠛⠛⠛⠲⠤⢿⣆⣙⠻⡶⢤⣀⣠⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⢿⣯⠙⠛⠳⠦⠤⠼⣿⠛⠓⠲⠴⣶⠾⣿⠽⣧⣿⣤⡈⠻⣿⠦⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠘⣯⢣⡀⠈⠓⢦⣄⡈⠃⠀⠀⠀⠙⢧⡈⠻⣶⣭⣄⠙⢶⣌⠑⣾⣝⡳⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠈⣷⣝⠦⣀⠀⠀⠉⢙⣲⡶⠴⠶⠶⠿⠷⣤⡈⠛⠿⡶⢍⣠⡿⣿⠗⠿⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⣿⣟⠷⣌⡓⠦⠀⢯⡉⠉⠉⠉⠙⠒⠶⢴⣿⡟⠶⠦⠀⠘⠿⢿⣄⢀⣻⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠹⣟⢦⣀⣹⡧⠤⠍⠻⢦⣀⠀⠀⠀⠀⠸⣇⠙⣆⠀⡗⠓⣦⣄⣀⣿⡌⣧⢳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠘⠳⢿⣟⡒⠤⠤⠤⢄⠉⠳⣦⣤⠄⠀⣉⡤⠼⠞⠛⠋⠉⠉⠉⠙⠛⣿⡎⢧⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⢀⣴⠏⠙⠛⣲⣶⠶⢶⡏⠁⢀⡴⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣿⣷⠀⢳⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠉⠛⠛⢻⣿⡁⠀⠀⠸⠶⠞⠉⠀⠀⠀⠀⢀⣀⣠⣴⡶⠚⠋⢹⣿⢹⣿⡇⠘⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠈⣿⣇⠀⠀⢀⣀⣀⣠⡤⠶⠖⢛⣯⣵⠾⠿⠛⠓⠀⠀⢻⢘⣿⡇⠀⢷⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⠉⣉⣉⣀⣀⣄⡀⠀⠛⠋⢀⣴⣾⣿⠆⠀⠀⠈⢯⣿⠃⠀⠈⠻⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⢻⣿⣼⡌⠉⢩⣽⣽⠽⢹⠀⠀⠀⠈⠈⠛⠁⠀⠀⠀⣠⡾⠃⠀⠀⠀⠀⠈⢳⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⣯⣧⠀⠁⠈⠉⠀⣼⣤⡤⠶⣶⠒⠒⠒⢚⣉⣭⠟⠁⠀⠀⠀⣀⣤⣤⠀⠹⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣧⣀⡤⠖⣫⣽⠶⣷⠚⠉⠉⢿⠉⢉⡾⠁⠀⢀⡴⠞⠉⣁⣀⣤⠤⠤⢳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⠤⠖⠛⢹⠀⠀⢻⡀⠀⠀⢸⡶⠋⢀⡠⢞⣣⠴⠚⠋⠁⠀⠀⠀⠀⠈⢳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣿⣧⠀⠀⠈⣧⠀⠀⢧⣀⡴⠋⠀⠀⢉⡴⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⣿⣿⣇⠀⠀⠈⠂⢀⡼⠋⠀⠀⢀⡴⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢳⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⣿⣿⡿⣆⠀⠀⣰⠟⠁⠀⢀⡴⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣤⣤⣶⠶⠖⠚⠉⠉⠙⠓⠶⣤⡀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⣿⣿⡟⠀⠙⣦⡾⠁⠀⢀⡴⠋⠀⠀⠀⠀⠀⠀⠀⢀⡤⠶⠋⠉⣠⠞⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢶⡄⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⣴⣿⣿⣿⠟⠀⠀⣰⠟⠀⣠⡶⠋⠀⠀⠀⠀⠀⠀⢀⣤⠞⠉⠀⠀⠀⡼⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣆⠀
⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⡿⠃⠀⢀⡴⠃⢀⡀⠁⠀⠀⠀⠀⠀⢀⡤⠒⢿⡀⠀⠀⠀⢀⡾⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣇
⠀⠀⠀⠀⠀⠀⠀⠀⠹⣿⡁⠀⠀⣾⡁⣠⠞⠁⠀⠀⠀⠀⣀⡴⠋⠁⠀⠀⠻⠀⠀⢠⡞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣇⠀⢰⡏⠉⠁⠀⠀⠀⣀⡴⠚⠁⠀⢀⣴⠀⠀⠀⠀⢠⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸
⠀⠀⠀⠀⠀⠀⠀⢀⣤⠾⣿⠀⠀⠁⠀⠀⢀⣤⢾⡅⠀⠀⣠⡴⠋⠁⠀⠀⠀⢀⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸
⠀⠀⠀⠀⠀⠀⣠⠟⠁⠀⢸⡧⠤⠶⣺⣿⡉⠀⢸⣅⣴⠟⠁⠀⠀⠀⠀⠀⠀⣸⠟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸
⠀⠀⠀⠀⠀⢠⡟⠀⢀⣴⠛⡇⢸⡼⣿⣿⣿⣤⠾⡏⠁⠀⠀⠀⠀⠀⠀⠀⡼⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸
⠀⠀⠀⠀⠀⢸⠀⣠⠞⢁⣴⡇⠘⣧⣽⡇⡟⠀⠀⠈⠃⠀⠀⠀⠀⠀⢀⡾⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿
⠀⠀⠀⠀⠀⠸⠾⠃⠀⠾⠀⠻⠋⠉⠘⠿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠐⠟⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠃
          "; 


        public SubZero() : base()
        {

        }



        public override void PerformAttack(Fighter enemy)
        {
            Console.WriteLine($"{this.Name} launches an ice blast at {enemy.Name}!");
            enemy.TakeDamage(Attack);

            this.XRayCharge += 15;
            if (this.XRayCharge > 100)
                this.XRayCharge = 100;
        }

        public override void PerformXRay(Fighter enemy)
        {
            if (!this.CanUseXRay)
            {
                Console.WriteLine($"{this.Name} freezes {enemy.Name} and shatters them with a powerful X-Ray!");
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
            Console.WriteLine($"{this.Name} performs FATALITY on {enemy.Name}!");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   ❄❄❄❄❄❄❄❄❄❄❄❄❄❄❄");
            Console.WriteLine("   ❄   FREEZE!!!   ❄");
            Console.WriteLine("   ❄❄❄❄❄❄❄❄❄❄❄❄❄❄❄");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   █████████");  
            Console.WriteLine("   ██ ☠ ██ ☠ ██");  
            Console.WriteLine("   █████████");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("   💥 BOOM! 💥");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("   * CRACK *");
            Console.WriteLine("   * SHATTER *");
            Console.WriteLine("   * PIECES FLY *");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ❄ ICE WINS ❄");
            Console.WriteLine("   ☠ Fatality ☠");
            Console.ResetColor();
        }


    }
}
