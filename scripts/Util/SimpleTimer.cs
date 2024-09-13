using Godot;

namespace RossoSkies1.scripts.Util
{
    internal partial class SimpleTimer : Node
    {
        [Signal]
        public delegate void TimerElapsedEventHandler();

        public double Time;

        private double _timer;
        private bool _started;

        public override void _Process(double delta)
        {
            if (!_started)
                return;

            _timer -= delta;

            if (_timer > 0)
                return;

            Stop();

            EmitSignal(SignalName.TimerElapsed);
        }

        public void Start()
        {
            if (_started)
                return;

            ResetTimer();
            _started = true;
        }

        public void Stop()
        { 
            _started = false;
            ResetTimer();
        }

        public void Pause() =>
            _started = false;

        public bool IsRunning() => 
            _started;

        private void ResetTimer() =>
            _timer = Time;
    }
}
