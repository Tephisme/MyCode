using System;

public interface IStatus
{
    Guid UniqueID { get; }
    string Name { get; }
    StatusManager StatusManagerReference { get; set; }
    bool IsOver { get; }
    float EndTime { get; }

    void OnEnter();
    void Tick();
    void OnExit();
}