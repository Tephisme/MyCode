using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IHealthComponent))]
public class StatusManager : MonoBehaviour
{
    private IHealthComponent _healthComponent;
    private List<IStatus> _statuses = new List<IStatus>();
    private List<IStatus> _statusesToRemove = new List<IStatus>();

    public List<IStatus> Statuses => _statuses;
    public  IHealthComponent HealthComponent => _healthComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<IHealthComponent>();
    }

    public void AddStatus(Status newStatus)
    {
        _statuses.Add(newStatus);
        newStatus.StatusManagerReference = this;
        newStatus.OnEnter();
    }

    // Remove the status with the least time remaining.
    public void RemoveStatus(string statusName)
    {
        IStatus statusToRemove = null;
        float endTime = float.MaxValue;

        foreach (IStatus status in _statuses)
        {
            if (status.Name == statusName)
            {
                if (endTime >= status.EndTime)
                {
                    endTime = status.EndTime;
                    statusToRemove = status;
                }
            }
        }

        if (statusToRemove != null)
            _statuses.Remove(statusToRemove);
    }

    public void RemoveAllStatusByName(string statusName)
    {
        List<IStatus> statusToRemove = new List<IStatus>();

        foreach (IStatus status in _statuses)
        {
            if (status.Name == statusName)
                statusToRemove.Add(status);
        }

        if (statusToRemove != null)
        {
            foreach (IStatus status in statusToRemove)
            {
                _statuses.Remove(status);
            }
        }
    }

    public void RemoveStatus(Guid statusGuid)
    {
        IStatus statusToRemove = null;

        foreach (IStatus status in _statuses)
        {
            if (status.UniqueID == statusGuid)
                statusToRemove = status;
        }

        if (statusToRemove != null)
            _statuses.Remove(statusToRemove);
    }

    public void AddDamageOverTime(HealthOverTimeParameters healthOverTimeParameters)
    { 
        _healthComponent.TakeDamageOverTime(healthOverTimeParameters);
    }
    
    public void SetDamageImmunity()
    {
        _healthComponent.SetDamageImmunity();
    }

    private void Update()
    {
        foreach (IStatus status in _statuses)
        {
            status.Tick();
            if (status.IsOver)
            {
                _statusesToRemove.Add(status);
                status.OnExit();
            }
        }

        foreach (IStatus status in _statusesToRemove)
        {
            _statuses.Remove(status);
        }
        _statusesToRemove.Clear();
    }

    [ContextMenu("Add Poison")]
    private void AddPoison()
    {
        var poison = new Poison();
        AddStatus(poison);
    }
    
    [ContextMenu("Damage immunity for 5 seconds")]
    private void AddDamageImmunity()
    {
        var immunity = new DamageImmunity();
        AddStatus(immunity);
    }

}
