using System;

public interface IHealthComponent
{
   Action<int> OnHealthChanged { get; set; }
   
   int Health { get; }
   Stats Stats { get; }
   bool IsImmuneToDamage { get; }

   void TakeDamage(int amount);
   void TakeDamageOverTime(HealthOverTimeParameters healthOverTimeParameters);
   void HealSelf(int amount);
   void SetDamageImmunity();
}
