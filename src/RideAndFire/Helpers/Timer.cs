using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace RideAndFire.Helpers;

public class Timer
{
    public bool IsRunning { get; private set; }
    
    private TimeSpan _elapsed;
    private TimeSpan _duration;
    private Action? _callback;

    public void Start(TimeSpan duration, Action? callback = null)
    {
        _elapsed = TimeSpan.Zero;
        _duration = duration;
        _callback = callback;
        IsRunning = true;
    }

    public void Update(GameTime gameTime)
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