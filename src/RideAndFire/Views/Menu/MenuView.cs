using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Views.Menu.Components;

namespace RideAndFire.Views.Menu;

public class MenuView : View
{
    public event Action? StartButtonClicked;

    private ParticlesView _particlesView;
    private ButtonView _startButton;

    public MenuView(SpriteBatch spriteBatch) : base(spriteBatch)
    {
    }

    public override void Initialize()
    {
        _particlesView = new ParticlesView(SpriteBatch);
        _particlesView.Initialize();

        _startButton = new ButtonView(SpriteBatch);
        _startButton.Click += StartButtonClicked;
    }

    public override void DrawTimed(GameTime gameTime)
    {
        SpriteBatch.GraphicsDevice.Clear(Color.Black);

        _particlesView.DrawTimed(gameTime);

        SpriteBatch.DrawString(ViewResources.ComicSansFont, "Ride and Fire",
            Constants.ScreenCenter - ViewResources.ComicSansFont.MeasureString("Ride and Fire") / 2 +
            new Vector2(0, -Constants.ScreenHeight / 4f), Color.White);

        _startButton.Draw();
    }
}