using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace mono_demo;

public class BasicGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D balloon, background, cannonBarrel;
    Vector2 balloonPosition, balloonOrigin;
    Vector2 barrelPosition, barrelOrigin;
    MouseState currentMouseState, previousMouseState;
    float angle;
    double opposite, adjacent;
    bool calculateAngle;
    bool mouseButtonClicked;

    public BasicGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        calculateAngle = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        balloon = Content.Load<Texture2D>("spr_lives");
        background = Content.Load<Texture2D>("spr_background");
        cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
        MediaPlayer.Play(Content.Load<Song>("snd_music"));
        balloonOrigin = new Vector2(balloon.Width / 2, balloon.Height);
        barrelPosition = new Vector2(72, 405);
        barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;
    }

    protected override void Update(GameTime gameTime)
    {
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();

        mouseButtonClicked = currentMouseState.LeftButton == ButtonState.Pressed
            && previousMouseState.LeftButton == ButtonState.Released;
        
        if (mouseButtonClicked) calculateAngle = !calculateAngle;
        if (calculateAngle)
        {
            opposite = currentMouseState.Y - barrelPosition.Y;
            adjacent = currentMouseState.X - barrelPosition.X;
            angle = (float)Math.Atan2(opposite, adjacent);
        }
        else
        {
            angle = 0.0f;
        }

        // TODO: Add your update logic here
        balloonPosition = new Vector2(currentMouseState.X, currentMouseState.Y);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1.0f, SpriteEffects.None, 0);
        _spriteBatch.Draw(balloon, balloonPosition - balloonOrigin, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
