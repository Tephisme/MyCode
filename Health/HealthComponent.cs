using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IComponent, IHealthComponent
{
    public Action<int> OnHealthChanged { get; set; }

    [Range(1, 1000)]
    public int MaxHealth = 100;
    public Stats Stats => _stats;
    public Stats _stats = new Stats();
    public Entity Owner {get { return GetComponent<Entity>(); }}
    public bool IsImmuneToDamage => _isImmuneToDamage;
    public bool IsDead { get { return Health <= 0; }}

    private List<HealthOverTimeParameters> _healthOverTimeModifierList = new List<HealthOverTimeParameters>();
    private List<HealthOverTimeParameters> _healthOverTimeModifierToRemove = new List<HealthOverTimeParameters>();
    private int m_Health;
    private bool _isImmuneToDamage = false;

    public int Health
    {
        get
        {
            return m_Health;
        }

        private set
        {
            m_Health = Mathf.Clamp(value, 0, MaxHealth);

            if (IsDead)
            {
                Die();
            }
        }
    }


    public void TakeDamage(int amount)
    {
        if(IsImmuneToDamage)
            return;
        
        if (amount > 0)
            amount = -amount;
        
        amount =  amount * (100 - _stats.DamageResistance) / 100;
        
        ModifyHealth(amount);
    }

    public void HealSelf(int amount)
    {
        if (amount < 0)
            amount = -amount;
        
        amount =  amount * (100 - _stats.HealResistance) / 100;
        
        ModifyHealth(amount);
    }

    public void StopLastHealthModifierOverTime()
    {
        _healthOverTimeModifierList.Remove(_healthOverTimeModifierList.LastOrDefault());
    }

    public void StopDamageOverTime(Guid damageOverTimeGuid)
    {
        HealthOverTimeParameters healthOverTimeParametersToRemove = null;
        foreach (HealthOverTimeParameters damageOverTime in _healthOverTimeModifierList)
        {
            if (damageOverTime.UniqueId == damageOverTimeGuid)
                healthOverTimeParametersToRemove = damageOverTime;
        }

        if (healthOverTimeParametersToRemove != null)
            _healthOverTimeModifierList.Remove(healthOverTimeParametersToRemove);
    }
    
    public void TakeDamageOverTime(HealthOverTimeParameters healthOverTimeParameters)
    {
        healthOverTimeParameters.Timer = Time.time;
        healthOverTimeParameters.Delay += healthOverTimeParameters.TickFrequency;
        
        healthOverTimeParameters.EndTime = healthOverTimeParameters.Timer + healthOverTimeParameters.DurationInSecond;

        if (healthOverTimeParameters.AmountPerTick > 0)
            healthOverTimeParameters.AmountPerTick = -healthOverTimeParameters.AmountPerTick;
        
        _healthOverTimeModifierList.Add(healthOverTimeParameters);
    }
    
    public void HealSelfOverTime(HealthOverTimeParameters healthOverTimeParameters)
    {
        healthOverTimeParameters.Timer = Time.time;
        healthOverTimeParameters.Delay += healthOverTimeParameters.TickFrequency;
        
        healthOverTimeParameters.EndTime = healthOverTimeParameters.Timer + healthOverTimeParameters.DurationInSecond;

        if (healthOverTimeParameters.AmountPerTick < 0)
            healthOverTimeParameters.AmountPerTick = -healthOverTimeParameters.AmountPerTick;
        
        _healthOverTimeModifierList.Add(healthOverTimeParameters);
    }
    
    public void SetDamageImmunity() => _isImmuneToDamage = !_isImmuneToDamage;

    private void OnValidate()
    {
        m_Health = MaxHealth;
    }
    
    private void Update()
    {
        OverTimeTicks();
    }

    private void ModifyHealth(int amount)
    {
        Health += amount;
        OnHealthChanged?.Invoke(Health);
    }
    
    private void OverTimeTicks()
    {
        foreach (HealthOverTimeParameters HealthModifier in _healthOverTimeModifierList)
        {
            if (HealthModifier.Timer >= HealthModifier.EndTime)
            {
                _healthOverTimeModifierToRemove.Add(HealthModifier);
            }
            else
            {
                if (HealthModifier.Timer >= HealthModifier.Delay)
                {
                    ModifyHealth(HealthModifier.AmountPerTick);
                    HealthModifier.Delay = HealthModifier.Timer + HealthModifier.TickFrequency;
                }

                HealthModifier.Timer += Time.deltaTime;
            }
        }

        foreach (HealthOverTimeParameters dot in _healthOverTimeModifierToRemove)
        {
            _healthOverTimeModifierList.Remove(dot);
        }

        _healthOverTimeModifierToRemove.Clear();
    }

    private void Die()
    {
        Debug.Log("Entity is dead.");
    }
    
    
    // --------------------------------------------------------------------------------------------
    // Test functions
    [ContextMenu("Take Lethal Damage")]
    private void TakeLethalDamage()
    {
        TakeDamage(Health);
    }
    
    [ContextMenu("Take 10 Damage")]
    private void Take10Damage()
    {
        TakeDamage(10);
    }

    [ContextMenu("Heal for 10 life points")]
    private void Heal10()
    {
        HealSelf(10);
    }
    
    [ContextMenu("Heal to full life")]
    private void HealFull()
    {
        HealSelf(MaxHealth);
    }

    [ContextMenu("Take 1 Damage over 10 seconds")]
    private void Take1DamageOver10Seconds()
    {
        var dot = new HealthOverTimeParameters(1, 1, 10);
        TakeDamageOverTime(dot);
    }
    
    [ContextMenu("Stop last HealthModifierOverTime")]
    private void StopTake1DamageOver10Seconds()
    {
        StopLastHealthModifierOverTime();
    }

    [ContextMenu("Take 1 Damage indefinitely")]
    private void Take1DamageIndefinitely()
    {
        var dot = new HealthOverTimeParameters(1, 1, 0);
        TakeDamageOverTime(dot);
    }
    
    [ContextMenu("Take 1 HealSelf over 10 seconds")]
    private void Take1HealSelfOver10Seconds()
    {
        var hot = new HealthOverTimeParameters(1, 1, 10);
        HealSelfOverTime(hot);
    }
}