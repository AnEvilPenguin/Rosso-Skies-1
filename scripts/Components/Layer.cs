using Godot;

public partial class Layer : Node
{
    [Signal]
    public delegate void LayerChangedEventHandler(int newLayer, int previousLayer);

    [Export]
    public double Cooldown = 10;
    public int Ceiling = 0;
    public int Floor = 0;

    public int CurrentLayer { 
        get => _currentLayer;
        set // allows override of timer if necessary
        { 
            EmitSignal(SignalName.LayerChanged, value, _currentLayer);
            _currentLayer = value;
        } 
    }

    private double _timer;
    private int _currentLayer = 0;

    public override void _Ready() =>
        ResetTimer();

    public override void _Process(double delta) =>
        _timer -= delta;

    public void LowerLayer() =>
        ChangeLayer(CurrentLayer - 1);


    public void RaiseLayer() =>
        ChangeLayer(CurrentLayer + 1);

    private void ResetTimer() =>
        _timer = Cooldown;

    private void ChangeLayer(int targetLayer)
    {
        if (isOnCooldown())
            return;

        if (Ceiling < targetLayer || targetLayer < Floor)
            return;

        ResetTimer();

        CurrentLayer = targetLayer;
    }

    private bool isOnCooldown() =>
        _timer > 0;
}
