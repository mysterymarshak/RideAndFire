using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using RideAndFire.Views.Menu.Components;

namespace RideAndFire.Views.Menu;

public class MenuView : View
{
    public event Action? StartButtonClicked;
    
    private Vector2 ScreenCenter => new Vector2(Constants.ScreenWidth, Constants.ScreenHeight) / 2f;

    private ParticleEffect _particleEffect;
    private Texture2D _particleTexture;
    private ButtonView _startButton;

    public MenuView(SpriteBatch spriteBatch) : base(spriteBatch)
    {
    }

    public override void Initialize()
    {
        _startButton = new ButtonView(SpriteBatch);
        _startButton.Click += StartButtonClicked;
        
        _particleTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
        _particleTexture.SetData([Color.White]);

        var textureRegion = new Texture2DRegion(_particleTexture);
        _particleEffect = new ParticleEffect
        {
            Position = ScreenCenter,
            Emitters =
            [
                new ParticleEmitter(textureRegion, 500, TimeSpan.FromSeconds(2),
                    Profile.BoxFill(Constants.ScreenWidth, Constants.ScreenHeight))
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(0f, 1f),
                        Quantity = 1,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(3.0f, 4.0f)
                    },
                    Modifiers =
                    {
                        new RotationModifier { RotationRate = -2.1f },
                        new LinearGravityModifier { Direction = -Vector2.UnitY, Strength = 30f }
                    }
                }
            ]
        };
    }

    public override void Draw()
    {
        SpriteBatch.GraphicsDevice.Clear(Color.Black);
        
        SpriteBatch.DrawString(ViewResources.ComicSansFont, "Ride and Fire", ScreenCenter - ViewResources.ComicSansFont.MeasureString("Ride and Fire") / 2 + new Vector2(0, -Constants.ScreenHeight / 4f), Color.White);
        
        _startButton.Draw();
    }

    public override void DrawTimed(GameTime gameTime)
    {
        _particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        SpriteBatch.Draw(_particleEffect);
    }
}