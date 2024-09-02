using Godot;

public partial class Health : Node
{
    [Signal]
    public delegate void DamageTakenEventHandler(int damage, int currentHealth);
    [Signal]
    public delegate void DestroyedEventHandler();
    [Signal]
    public delegate void HealingEventHandler(int healing, int currentHealth);

    [Export]
    public int MaxHealth = 100;

    private int _health;

    public override void _Ready()
    {
        _health = MaxHealth;
    }

    public void TakeDamage(Damage damage)
    {
        _health -= damage.Basic;

        if (_health <= 0)
            EmitSignal(SignalName.Destroyed);
        else
            EmitSignal(SignalName.DamageTaken, damage.Basic, _health);
    }

    public void Heal(int healing)
    {
        _health += healing;

        if (_health >= MaxHealth)
            _health = MaxHealth;

        EmitSignal(SignalName.Healing, healing, _health);
    }

}
