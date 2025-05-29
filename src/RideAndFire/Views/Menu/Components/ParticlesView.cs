using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Profiles;

namespace RideAndFire.Views.Menu.Components;

public class ParticlesView : View
{
    private ParticleEffect _particleEffect;
    private Texture2D _particleTexture;

    public ParticlesView(SpriteBatch spriteBatch) : base(spriteBatch)
    {
    }

    public override void Initialize()
    {
        _particleTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
        _particleTexture.SetData([Color.White]);

        var textureRegion = new Texture2DRegion(_particleTexture);
        var particleEmitter = new ParticleEmitter(textureRegion, 500, TimeSpan.FromSeconds(2),
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
        };

        _particleEffect = new ParticleEffect
        {
            Position = Constants.ScreenCenter,
            Emitters = [particleEmitter]
        };
    }

    public override void DrawTimed(GameTime gameTime)
    {
        _particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        SpriteBatch.Draw(_particleEffect);
    }

    public override void Dispose()
    {
        _particleTexture.Dispose();
        _particleEffect.Dispose();
    }
}