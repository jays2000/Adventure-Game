using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace StarterGame
{
    public class GameClock
    {
        private System.Timers.Timer timer;
        private int _timeInGame;
        public int TimeInGame { get { return _timeInGame; } }

        public GameClock(int interval)
        {
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _timeInGame++;
            //Console.WriteLine("Tick!");
            Notification notification = new Notification("GameClockTick", this);
            NotificationCenter.Instance.PostNotification(notification);
        }
    }
}
