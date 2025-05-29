using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RideAndFire.Controllers;
using RideAndFire.Models;
using RideAndFire.Views;
using RideAndFire.Views.Game;
using RideAndFire.Views.Menu;

namespace RideAndFire;

public class Game : Microsoft.Xna.Framework.Game
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly StringBuilder _titleBuilder;

    private Controller _controller;
    private SpriteBatch _spriteBatch;

    public Game()
    {
        _graphics = new GraphicsDeviceManager(this);
        _titleBuilder = new StringBuilder();
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
        _graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
        // _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        var targetFps = 144;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / targetFps);

        Content.RootDirectory = "Content";

        IsMouseVisible = true;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        ViewResources.LoadContent(Content);
    }

    protected override void BeginRun()
    {
        InitializeMenu();
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        _controller.OnUpdate(gameTime);

        UpdateWindowTitle(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        _controller.OnDraw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void InitializeMenu()
    {
        var view = new MenuView(_spriteBatch);
        var controller = new MenuController(view);

        controller.GameStart += OnGameStart;
        controller.Initialize();
        view.Initialize();

        _controller = controller;
    }

    private void OnGameStart()
    {
        ((MenuController)_controller).GameStart -= OnGameStart;

        InitializeGame();
    }

    private void InitializeGame()
    {
        var model = new GameModel();
        var view = new GameView(model, _spriteBatch);
        _controller = new GameController(view, model);
        
        _controller.Initialize();
        view.Initialize();
    }

    private void UpdateWindowTitle(GameTime gameTime)
    {
        _titleBuilder.Clear();

        _titleBuilder.Append(nameof(RideAndFire));
        _titleBuilder.Append(": ");
        _titleBuilder.Append(Math.Round(1000 / gameTime.ElapsedGameTime.TotalMilliseconds, 2));
        _titleBuilder.Append(" FPS");

        if (!_titleBuilder.Equals(Window.Title))
        {
            Window.Title = _titleBuilder.ToString();
        }
    }
}