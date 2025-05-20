using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

public class Painter : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    static GameWorld gameWorld;
    public static Vector2 ScreenSize { get; private set; }
    InputHelper inputHelper;


    public Painter()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        inputHelper = new InputHelper();
    }

    public static GameWorld GameWorld
    {
        get { return gameWorld; }
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        gameWorld = new GameWorld(Content);
        ScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        // MediaPlayer.Play(Content.Load<Song>("snd_music"));
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update();
        gameWorld.HandleInput(inputHelper);
        gameWorld.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        gameWorld.Draw(gameTime, spriteBatch);
    }
}
