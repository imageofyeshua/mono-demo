using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

public class Painter : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D balloon, background;
    Vector2 balloonPosition, balloonOrigin;
    double opposite, adjacent;
    bool calculateAngle;
    bool mouseButtonClicked;

    Cannon cannon;
    InputHelper inputHelper;

    public Painter()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        calculateAngle = false;
        inputHelper = new InputHelper();
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
        inputHelper.Update();
        mouseButtonClicked = inputHelper.MouseLeftButtonPressed();
        // TODO: Add your update logic here

        if (mouseButtonClicked) calculateAngle = !calculateAngle;
        if (calculateAngle)
        {
            opposite = inputHelper.MousePosition.Y - cannon.Position.Y;
            adjacent = inputHelper.MousePosition.X - cannon.Position.X;
            cannon.Angle = (float)Math.Atan2(opposite, adjacent);
        }
        else
        {
            cannon.Angle = 0.0f;
        }

        if (inputHelper.KeyPressed(Keys.R))
        {
            cannon.Color = Color.Red;
        }
        else if (inputHelper.KeyPressed(Keys.G))
        {
            cannon.Color = Color.Green;
        }
        else if (inputHelper.KeyPressed(Keys.B))
        {
            cannon.Color = Color.Blue;
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
