using System;
using UnityEngine;

public class Burning : Status
{
    public HealthOverTimeParameters BurningHealthOverTimeParameters => _burning;

    private HealthOverTimeParameters _burning = new HealthOverTimeParameters(5, 1, 5);

    public Burning()
    {
        _name = "Burning";
        _uniqueID = new Guid();
    }

    public override void OnEnter()
    {
        StatusManagerReference.AddDamageOverTime(_burning);
        _endTime = _burning.EndTime;
    }

    public override void Tick()
    {
        if (Time.time >= _burning.EndTime)
            _isOver = true;
    }

    public override void OnExit()
    {
        
    }
}