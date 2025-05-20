using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Ball
{
    Texture2D colorRed, colorGreen, colorBlue;
    Vector2 origin, position;
    Color color;

    public Ball(ContentManager Content)
    {
        colorRed = Content.Load<Texture2D>("spr_ball_red");
        colorGreen = Content.Load<Texture2D>("spr_ball_green");
        colorBlue = Content.Load<Texture2D>("spr_ball_blue");
        origin = new Vector2(colorRed.Width / 2.0f, colorRed.Height / 2.0f);
        Reset();
    }

    public void Reset()
    {
        position = new Vector2(72, 405);
        color = Color.Blue;
    }

    public void HandleInput(InputHelper inputHelper)
    {

    }

    public void Update(GameTime gameTime)
    {
        color = Painter.GameWorld.Cannon.Color;
        position = Painter.GameWorld.Cannon.BallPosition;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Texture2D currentSprite;
        if (color == Color.Red)
            currentSprite = colorRed;
        else if (color == Color.Green)
            currentSprite = colorGreen;
        else
            currentSprite = colorBlue;

        spriteBatch.Draw(currentSprite, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
    }
}