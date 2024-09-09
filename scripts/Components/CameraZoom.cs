using Godot;

public partial class CameraZoom : Node
{
    private Camera2D _camera;

    [Export]
    public float BaseZoom = 1.5f;

    [Export]
    public float Step = 0.25f;

    [Export]
    public int multiplier = 1;

    private float _currentZoom;

    public override void _Ready()
    {
        _camera = GetParent<Node>()
            .GetNode<Camera2D>("%Camera2D");

        _currentZoom = BaseZoom;
    }

    public void ZoomFromSpeedPercent(float percent)
    {
        var inverse = 100 - percent;
        var reduced = inverse / 100;
        var multiplied = reduced * multiplier;
        var value = multiplied + _currentZoom;

        _camera.Zoom = new Vector2(value, value);
    }

    public void SetLayer(int layer, int previousLayer)
    {
        if (layer > previousLayer)
        {
            _currentZoom -= Step;
            return;
        }

        _currentZoom += Step;
    }
}
