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
    Texture2D balloon, background;
    Vector2 balloonPosition;

    public BasicGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
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
    }

    protected override void Update(GameTime gameTime)
    {
        MouseState currentMouseState = Mouse.GetState();

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
        _spriteBatch.Draw(balloon, balloonPosition, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
