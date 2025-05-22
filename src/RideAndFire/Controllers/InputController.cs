using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RideAndFire.Extensions;
using RideAndFire.Models;

namespace RideAndFire.Controllers;

public class InputController
{
    private readonly PlayerModel _player;

    public InputController(PlayerModel player)
    {
        _player = player;
    }

    public void OnUpdate(GameTime gameTime)
    {
        HandleMovement(gameTime);
        HandleShooting();
        HandleDebug();
    }

    private void HandleMovement(GameTime gameTime)
    {
        var rotation = 0f;
        var velocity = Vector2.Zero;
        var deltaTime = gameTime.AsDeltaTime();

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            rotation -= Constants.RotationSpeedInRadians * deltaTime;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            rotation += Constants.RotationSpeedInRadians * deltaTime;
        }

        var newRotation = _player.Rotation + rotation;
        var newDirection = new Vector2(MathF.Sin(newRotation), -MathF.Cos(newRotation));

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            velocity += newDirection * Constants.PlayerSpeed * deltaTime;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            velocity -= newDirection * Constants.ReversePlayerSpeed * deltaTime;
        }

        _player.Velocity = velocity;
        _player.AngularVelocity = rotation;
    }

    private void HandleShooting()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && _player.CanShoot)
        {
            _player.IsShooting = true;
        }
    }

    private void HandleDebug()
    {
        if (Mouse.GetState().RightButton == ButtonState.Pressed)
        {
        }
    }
}