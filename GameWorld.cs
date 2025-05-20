using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GameWorld
{
    Texture2D background;
    Cannon cannon;
    Ball ball;

    public GameWorld(ContentManager Content)
    {
        background = Content.Load<Texture2D>("spr_background");
        cannon = new Cannon(Content);
        ball = new Ball(Content);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        cannon.HandleInput(inputHelper);
        ball.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        cannon.Update(gameTime);
        ball.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        cannon.Draw(gameTime, spriteBatch);
        ball.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public Cannon Cannon
    {
        get { return cannon; }
    }

    public bool IsOutsideWorld(Vector2 position)
    {
        return position.X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
    }

}