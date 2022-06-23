using System;

public abstract class Status : IStatus
{
    public Guid UniqueID => _uniqueID;
    public string Name => _name;
    public StatusManager StatusManagerReference { get; set; }
    public bool IsOver => _isOver;
    public float EndTime => _endTime;

    protected Guid _uniqueID = new Guid();
    protected string _name = "NameNotSet";
    protected bool _isOver = false;
    protected float _endTime = 0;

    public abstract void OnEnter();

    public abstract void Tick();

    public abstract void OnExit();
}