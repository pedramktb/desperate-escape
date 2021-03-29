public class GrenadeGuyData : NPCData
{
    public int GrenadeGenerationCooldown;
    public GrenadeGuyData(int Value, int HP, int MoveSpeed, int GrenadeGenerationCooldown) : base(Value, HP, MoveSpeed)
    {
        this.GrenadeGenerationCooldown = GrenadeGenerationCooldown;
    }
}
