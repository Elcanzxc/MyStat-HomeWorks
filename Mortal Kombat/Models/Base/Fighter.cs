

namespace Mortal_Kombat.Models.Base
{
    abstract public class Fighter
    {
        public abstract string Name { get; }
        public abstract string AsciiArt { get; }

        public abstract int MaxHealth { get; }
        public abstract int Attack { get; }
        public abstract int Defense { get; }


        public int CurrentHP { get;  set; }
        public int XRayCharge { get;  set; }


        public bool IsAlive => CurrentHP > 0;
        public bool CanUseXRay => XRayCharge >= 100;


        public Fighter()
        {
            this.CurrentHP = this.MaxHealth;
            this.XRayCharge = 0;
        }


        public virtual void TakeDamage(int amount)
        {
  
            int damage = amount - Defense;

            CurrentHP -= damage;

           
            if (CurrentHP < 0)
                CurrentHP = 0;

            XRayCharge += 10;

      
            if (XRayCharge > 100)
                XRayCharge = 100;
        }

        public abstract void PerformAttack(Fighter enemy);

        public abstract void PerformXRay(Fighter enemy);

        public abstract void PerformFatality(Fighter enemy);


    }
}
