using System;
using UnityEngine;

public class Poison : Status
{
    public HealthOverTimeParameters PoisonParameters => _poison;

    private HealthOverTimeParameters _poison = new HealthOverTimeParameters(1, 2, 10);

    public Poison()
    {
        _name = "Poison";
        _uniqueID = new Guid();
    }

    public override void OnEnter()
    {
        StatusManagerReference.HealthComponent.Stats.AddHealResistance(10);
        StatusManagerReference.AddDamageOverTime(_poison);
        _endTime = _poison.EndTime;
    }

    public override void Tick()
    {
        if (Time.time >= _poison.EndTime)
            _isOver = true;
    }

    public override void OnExit()
    {
        StatusManagerReference.HealthComponent.Stats.RemoveHealResistance(10);
    }
}