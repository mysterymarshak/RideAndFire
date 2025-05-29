using System;
using Microsoft.Xna.Framework;
using RideAndFire.Views.Menu;

namespace RideAndFire.Controllers;

public class MenuController : GameStateController
{
    public event Action? GameStart;

    private readonly MenuView _view;
    private GameTime _gameTime;

    public MenuController(MenuView view)
    {
        _view = view;
    }

    public override void Initialize()
    {
        _view.StartButtonClicked += GameStart;
    }

    public override void OnUpdate(GameTime gameTime)
    {
        _gameTime = gameTime;
    }

    public override void OnDraw()
    {
        _view.Draw();
        _view.DrawTimed(_gameTime);
    }

    public override void Dispose()
    {
        _view.StartButtonClicked -= GameStart;
        _view.Dispose();
    }
}