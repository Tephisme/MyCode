using UnityEngine;

public class Stats
{
    public int Attack => _attack;
    public int Defense => _defense;
    public int DamageResistance => _damageResistance;
    public int HealResistance => _healResistance;

    private int _attack = 1;
    private int _defense = 1;
    private int _damageResistance = 0;
    private int _healResistance = 0;

    public Stats() { }

    public Stats(int attack, int defense, int damageResistance, int healResistance)
    {
        _attack = attack;
        _defense = defense;
        _damageResistance = damageResistance;
        _healResistance = healResistance;
    }
    
    public void AddDamageResistance(int newResistancePercentage)
    {
        newResistancePercentage = Mathf.Clamp(newResistancePercentage, 0, 50);
        _damageResistance = Mathf.Clamp(_damageResistance + newResistancePercentage, 0, 50);
    }
    
    public void RemoveDamageResistance(int newResistancePercentage)
    {
        newResistancePercentage = Mathf.Clamp(newResistancePercentage, 0, 50);
        _damageResistance = Mathf.Clamp(_damageResistance - newResistancePercentage, 0, 50);
    }
    
    public void AddHealResistance(int newHealResistancePercentage)
    {
        newHealResistancePercentage = Mathf.Clamp(newHealResistancePercentage, 0, 50);
        _healResistance = Mathf.Clamp(_healResistance + newHealResistancePercentage, 0, 50);
    }

    public void RemoveHealResistance(int newHealResistancePercentage)
    {
        newHealResistancePercentage = Mathf.Clamp(newHealResistancePercentage, 0, 50);
        _healResistance = Mathf.Clamp(_healResistance - newHealResistancePercentage, 0, 50);
    }
}