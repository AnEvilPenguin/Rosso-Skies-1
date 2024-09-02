using Godot;

public partial class GUI : CanvasLayer
{
	private Label _layerLabel;
	private Label _healthLabel;

	public override void _Ready()
	{
		_layerLabel = GetNode<Label>("LayerLabel");
		_healthLabel = GetNode<Label>("HealthLabel");
	}

	public void UpdateHealth(int hitPoints) =>
		_healthLabel.Text = $"Health: {hitPoints}";

	public void UpdateHealth(string text) =>
		_healthLabel.Text = $"Health: {text}";

	public void UpdateLayer(int layer) =>
		_layerLabel.Text = $"Layer: {layer}";

}
