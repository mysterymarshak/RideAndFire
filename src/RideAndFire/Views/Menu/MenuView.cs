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

        var screenQuarter = new Vector2(0, Constants.ScreenHeight / 4f);
        var gameNameMessage = "Ride and Fire";
        var gameNameMessageSize = ViewResources.ComicSansFont.MeasureString(gameNameMessage);
        SpriteBatch.DrawString(ViewResources.ComicSansFont, gameNameMessage,
            Constants.ScreenCenter - (gameNameMessageSize / 2) - screenQuarter, Color.White);

        _startButton.Draw();
    }

    public override void Dispose()
    {
        _startButton.Click -= StartButtonClicked;
        _particlesView.Dispose();
    }
}