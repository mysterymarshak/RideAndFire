using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RideAndFire.Configuration;
using RideAndFire.Controllers;
using RideAndFire.Models;
using RideAndFire.Views.Game;
using RideAndFire.Views.Menu;

namespace RideAndFire;

public class Game : Microsoft.Xna.Framework.Game
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly StringBuilder _titleBuilder;
    private readonly ConfigurationController _configurationController;

    private GameStateController _controller;
    private SpriteBatch _spriteBatch;
    private KeyboardState _previousKeyboardState;

    public Game()
    {
        _graphics = new GraphicsDeviceManager(this);
        _titleBuilder = new StringBuilder();
        _configurationController = new ConfigurationController();
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
        InitializeConfiguration();
        InitializeMenu();
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();
        if (_previousKeyboardState[Keys.Escape] == KeyState.Up && keyboardState[Keys.Escape] == KeyState.Down)
        {
            if (_controller is GameController)
            {
                _controller.Dispose();
                InitializeMenu();
            }
            else
            {
                Exit();
            }
        }

        _previousKeyboardState = Keyboard.GetState();

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

    private void InitializeConfiguration()
    {
        _configurationController.Initialize();
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
        _controller.Dispose();

        InitializeGame();
    }

    private void InitializeGame()
    {
        var model = new GameModel();
        var view = new GameView(model, _spriteBatch);
        var mapGenerationStrategy = new DefaultMapGenerationStrategy();
        _controller = new GameController(view, model, mapGenerationStrategy, _configurationController);

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