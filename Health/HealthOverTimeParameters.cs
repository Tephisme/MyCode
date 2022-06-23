using System;

public class HealthOverTimeParameters
{
    public float Timer = 0f;
    public float Delay = 0f;
    public float EndTime = 1f;

    public int AmountPerTick
    {
        get => _amountPerTick;
        set => _amountPerTick = value;
    }

    public float TickFrequency => _tickFrequency;
    public float DurationInSecond => _durationInSecond;
    public Guid UniqueId => _uniqueID;

    private float _durationInSecond = 1;
    private int _amountPerTick = 0;
    private float _tickFrequency = 1f;
    private Guid _uniqueID = new Guid();

    public HealthOverTimeParameters(int amountPerTick, float tickFrequency, float durationInSecond)
    {
        if (durationInSecond == 0)
            durationInSecond = float.MaxValue / 2;

        _amountPerTick = amountPerTick;
        _tickFrequency = tickFrequency;
        _durationInSecond = durationInSecond;
    }
}