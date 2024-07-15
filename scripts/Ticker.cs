using Godot;
using System;
using System.Collections.Generic;

public partial class Ticker : Label
{
    [Export]
    public string[] NewsItems;
    [Export]
    public bool AllCapitals;
    [Export]
    public int MaxLength = 58;
    [Export]
    public string Separator = "  ⬤  ";

    private Timer _timer;
    private String _text;
    private int _index = 0;
    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");

        _text = NewsItems.Join(Separator) + Separator;

        if (AllCapitals)
            _text = _text.ToUpper();

        Text = GetText(0);

        _timer.Start();
    }

    public void OnTimerTimeout()
    {
        UpdateTicker();
    }

    private void UpdateTicker()
    {
        _index = _index == _text.Length ? 0 : _index + 1;

        Text = GetText(_index);
    }

    private string GetText(int index)
    {
        if (_index + MaxLength <= _text.Length)
            return _text.Substring(index, MaxLength);

        var overflow = index + MaxLength - _text.Length;

        var output = _text.Substring(index, MaxLength - overflow) + _text.Substring(0, overflow);
        return output;
    }
        
}
