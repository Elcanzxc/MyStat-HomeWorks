using Mortal_Kombat.Models.Base;


namespace Mortal_Kombat.Models.Child
{
    class Raiden : Fighter
    {

        public override string Name => "Raiden";
        public override int MaxHealth => 120;
        public override int Attack => 25;
        public override int Defense => 10;

        public override string AsciiArt => @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠱⣬⡆⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠀⠀⠀⢠⢺⠃⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢃⣄⠀⣰⡏⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⢾⣿⡀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣗⣿⣷⡀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣴⣶⣶⣶⣶⣶⣶⣶⣤⣤⣀⠀⠀⠀⠀⠀⠀⣤⣿⣿⣧⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⣾⣿⣿⣿⣿⣿⣿⠿⣿⣿⣿⣿⣿⣿⣿⣶⣤⡀⠀⠀⣿⣿⣿⣿⡆
⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⠏⢐⣒⠪⢿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠻⢿⣛⣻⠇
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⣻⣿⣿⣿⣿⣿⡇⣷⣷⠂⣾⣿⣿⣿⣿⣿⣿⣿⠇⠀⢮⠈⠀⢀⡅
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⢿⣿⣿⣧⣨⣬⣴⣿⣿⣿⣿⣿⣿⣿⡻⠀⠂⠀⢀⣤⠞⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣹⣿⣿⣿⣿⣿⣿⡿⢹⣿⣿⣿⠇⡆⠆⡆⡏⠁⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡄⠈⠏⠏⡉⡟⠹⡿⢉⠟⠁⠎⠉⡏⣹⣆⢀⢻⠋⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠓⡂⠀⣐⠹⡄⠈⢄⢄⢄⠎⠂⢌⠈⣤⠇⢹⠇⠈⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⣤⣏⠇⣿⠄⢧⠀⢂⢡⡞⠀⢀⢀⢢⣽⠌⠈⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢽⣿⣿⣇⡀⠄⢢⢳⣨⣎⠔⡴⢃⣮⣾⠏⠀⠀⡈⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠔⣾⣷⢹⠏⠎⢣⠁⢿⣄⠙⣱⢿⢿⡇⠀⠄⡐⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢲⣿⣿⡇⡗⠄⠀⠁⠈⣻⣄⣇⣾⣾⠈⠰⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠰⣄⠀⠀⠀⠀⠈⣪⣿⣟⣷⢼⠁⠀⠀⠀⡟⣿⣿⣿⡏⠠⠢⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀
⢄⠀⡀⠘⣁⣄⣤⣱⣶⣯⣫⡷⢫⡝⡗⣤⣴⣤⣿⣿⣿⣿⢧⠔⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠈⠹⢧⣾⣩⣏⣨⣻⠿⠛⠉⠀⠈⠝⠪⢿⡿⢟⠿⣻⣿⣶⣗⢨⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡴⣀⣬⢤⣰⣝⣛⣿⣭⠳⠚⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⢰⠟⡔⠭⠙⠛⣿⣿⣿⡇⠀⠀⠀⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠣⢺⢰⠉⠂⠘⠛⢱⡏⢸⡇⠀⠀⣀⡡⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⢨⢿⠘⠀⠀⠀⠀⣿⠁⣿⣇⠀⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢁⠥⣚⠀⠀⠀⠀⢀⡟⢰⢻⣷⠕⠌⠀⣀⠠⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⠀⠒⡟⠀⠀⠀⠀⢸⡇⠆⢸⣿⣄⡑⠀⡠⠀⠁⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⠐⡇⢀⠄⠀⠀⣾⠁⠀⢸⢻⡏⠂⠐⠢⠄⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⠀⠀⡇⣴⡄⠀⢀⡇⡀⢀⣿⣺⣷⠁⠀⠈⢢⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⣺⣆⠀⣇⣿⡁⠀⠀⠁⠁⣸⣿⣿⠟⠀⠀⠀⢈⠛⠀⠀⠀⠀⠀⠀⠀
"; 

        public Raiden() : base()
        {

        }


        public override void PerformAttack(Fighter enemy)
        {
            Console.WriteLine($"{this.Name} strikes with lightning!");
            enemy.TakeDamage(this.Attack);

            this.XRayCharge += 15;
            if (this.XRayCharge > 100)
                this.XRayCharge = 100;
        }

        public override void PerformXRay(Fighter enemy)
        {
            if (!this.CanUseXRay)
            {
                Console.WriteLine($"{this.Name} tried to unleash X-Ray, but energy is insufficient.");
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

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{this.Name} performs FATALITY: Lightning Storm on {enemy.Name}!");
            Console.ResetColor();
            Console.WriteLine();

            Thread.Sleep(800);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    ⚡⚡⚡⚡⚡⚡⚡⚡⚡⚡⚡");
            Console.WriteLine("    ⚡  FEEL THE THUNDER! ⚡");
            Console.WriteLine("    ⚡⚡⚡⚡⚡⚡⚡⚡⚡⚡⚡");
            Console.ResetColor();

            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("        ⚡");
            Thread.Sleep(200);
            Console.WriteLine("        ⚡");
            Thread.Sleep(200);
            Console.WriteLine("        ⚡");
            Thread.Sleep(200);
            Console.WriteLine("     █████████");
            Console.WriteLine("     ██ ⚡🔥⚡ ██ ⚡🔥⚡ ██");
            Console.WriteLine("     █████████");
            Console.ResetColor();

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("     ☁️ Smoke rises... ☁️");
            Console.WriteLine("     ⚫ Burned remains ⚫");
            Console.ResetColor();

            Thread.Sleep(800);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("   ⚡ RAIDEN WINS ⚡");
            Console.WriteLine("   ☠ Fatality ☠");
            Console.ResetColor();
        }



    }
}
