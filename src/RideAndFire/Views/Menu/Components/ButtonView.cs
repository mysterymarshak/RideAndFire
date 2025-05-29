using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RideAndFire.Extensions;

namespace RideAndFire.Views.Menu.Components;

public class ButtonView : View
{
    public event Action? Click;

    private Rectangle ButtonBounds => new(
        (Constants.ScreenCenter - ViewResources.Button.Bounds.Center.ToVector2()).ToPoint(),
        ViewResources.Button.Bounds.Size);

    private MouseState _previousMouseState;

    public ButtonView(SpriteBatch spriteBatch) : base(spriteBatch)
    {
    }

    public override void Draw()
    {
        var mouseState = Mouse.GetState();
        var cursorOverButton = ButtonBounds.Contains(mouseState.Position);
        var isButtonDown = mouseState.LeftButton == ButtonState.Pressed && cursorOverButton;

        if (cursorOverButton && _previousMouseState.LeftButton == ButtonState.Pressed &&
            mouseState.LeftButton == ButtonState.Released)
        {
            Click?.Invoke();
        }

        var buttonTexture = isButtonDown ? ViewResources.ButtonPressed : ViewResources.Button;
        SpriteBatch.Draw(buttonTexture, ButtonBounds, null, Color.White);

        SpriteBatch.DrawString(ViewResources.BasicFont, "START",
            Constants.ScreenCenter - ViewResources.BasicFont.MeasureString("START") / 2f, Color.Black);

        if (cursorOverButton)
        {
            var buttonBorder = ButtonBounds;
            buttonBorder.Inflate(1, 1);
            SpriteBatch.DrawBox(buttonBorder, Color.White);
        }

        _previousMouseState = mouseState;
    }
}