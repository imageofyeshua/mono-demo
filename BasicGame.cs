using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

public class BasicGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D balloon, background;
    Texture2D colorRed, colorGreen, colorBlue;
    Vector2 balloonPosition, balloonOrigin;
    MouseState currentMouseState, previousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;
    double opposite, adjacent;
    bool calculateAngle;
    bool mouseButtonClicked;

    Cannon cannon;

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
        MediaPlayer.Play(Content.Load<Song>("snd_music"));
        balloonOrigin = new Vector2(balloon.Width / 2, balloon.Height);
        balloonPosition = new Vector2(300, 100);
        cannon = new Cannon(Content);
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
        GraphicsDevice.Clear(Color.White);
        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, Vector2.Zero, Color.White);
        cannon.Draw(gameTime, _spriteBatch);
        // _spriteBatch.Draw(balloon, balloonPosition - balloonOrigin, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
