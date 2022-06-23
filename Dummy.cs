using UnityEngine;

public class Dummy : HealthComponent
{
    private bool Paused = false;

    [ContextMenu("Pause Unpause")]
    public void PauseUnpause()
    {
        if (Paused)
            Time.timeScale = 1;
        else
        {
            Time.timeScale = 0;
            Paused = true;
        }
    }
}
