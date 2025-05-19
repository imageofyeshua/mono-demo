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
    Texture2D colorRed, colorGreen, colorBlue;
    Color currentColor;
    Vector2 balloonPosition, balloonOrigin;
    Vector2 barrelPosition, barrelOrigin;
    Vector2 colorOrigin;
    MouseState currentMouseState, previousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;
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
        balloonPosition = new Vector2(300, 100);
        colorRed = Content.Load<Texture2D>("spr_cannon_red");
        colorGreen = Content.Load<Texture2D>("spr_cannon_green");
        colorBlue = Content.Load<Texture2D>("spr_cannon_blue");
        colorOrigin = new Vector2(colorRed.Width, colorRed.Height) / 2;
        currentColor = Color.Blue;
    }

    protected override void Update(GameTime gameTime)
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();

        mouseButtonClicked = currentMouseState.LeftButton == ButtonState.Pressed
            && previousMouseState.LeftButton == ButtonState.Released;

        // TODO: Add your update logic here

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

        if (currentKeyboardState.IsKeyDown(Keys.R) && previousKeyboardState.IsKeyUp(Keys.R))
        {
            currentColor = Color.Red;
        }
        else if (currentKeyboardState.IsKeyDown(Keys.G) && previousKeyboardState.IsKeyUp(Keys.G))
        {
            currentColor = Color.Green;
        }
        else if (currentKeyboardState.IsKeyDown(Keys.B) && previousKeyboardState.IsKeyUp(Keys.B))
        {
            currentColor = Color.Blue;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Texture2D currentSprite;
        GraphicsDevice.Clear(Color.White);

        if (currentColor == Color.Red)
            currentSprite = colorRed;
        else if (currentColor == Color.Green)
            currentSprite = colorGreen;
        else
            currentSprite = colorBlue;
        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1.0f, SpriteEffects.None, 0);
        _spriteBatch.Draw(currentSprite, barrelPosition, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
        _spriteBatch.Draw(balloon, balloonPosition - balloonOrigin, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
