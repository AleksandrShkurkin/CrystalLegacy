public class CooldownManager
{
    private float cooldownTime;
    private float lastUseTime = 0;
    
    public CooldownManager(float cooldownDuration)
    {
        cooldownTime = cooldownDuration;
        lastUseTime = -cooldownTime;
    }

    public bool IsCooldownFinished(float currentTime)
    {
        return currentTime >= lastUseTime + cooldownTime;
    }

    public void InitiateCooldown(float currentTime)
    {
        lastUseTime = currentTime;
    }
}
