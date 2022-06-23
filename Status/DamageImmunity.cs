using System;
using UnityEngine;

public class DamageImmunity : Status
{
    private float _endTime;

        public DamageImmunity()
    {
        _name = "DamageImmunity";
        _uniqueID = new Guid();
        _endTime = Time.time + 5f;
    }
        
        public DamageImmunity(float duration)
    {
        _name = "DamageImmunity";
        _uniqueID = new Guid();
        _endTime = Time.time + duration;
    }
    
    public override void OnEnter()
    {
        StatusManagerReference.SetDamageImmunity();
        Debug.Log(StatusManagerReference.HealthComponent.IsImmuneToDamage);
        Debug.Log(_endTime);
    }

    public override void Tick()
    {
        Debug.Log(_endTime);
        if (Time.time >= _endTime)
            _isOver = true;
    }

    public override void OnExit()
    {
        StatusManagerReference.SetDamageImmunity();
        Debug.Log(StatusManagerReference.HealthComponent.IsImmuneToDamage);
    }
}
