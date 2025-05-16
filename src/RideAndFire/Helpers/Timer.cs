using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace RideAndFire.Helpers;

public class Timer
{
    public bool IsRunning { get; private set; }
    
    private static readonly ConditionalWeakTable<Timer, Dummy?> Timers = [];

    private TimeSpan _elapsed;
    private TimeSpan _duration;
    private Action? _callback;
    
    public Timer()
    {
        Timers.Add(this, null);
    }
    
    public static void TriggerUpdates(GameTime gameTime)
    {
        foreach (var (timer, _) in Timers)
        {
            timer.Update(gameTime);
        }
    }

    public void Start(TimeSpan duration, Action? callback = null)
    {
        _elapsed = TimeSpan.Zero;
        _duration = duration;
        _callback = callback;
        IsRunning = true;
    }

    private void Update(GameTime gameTime)
    {
        if (!IsRunning)
            return;
        
        _elapsed += gameTime.ElapsedGameTime;

        if (_elapsed >= _duration)
        {
            IsRunning = false;
            _callback?.Invoke();
        }
    }
}

public abstract record Dummy;