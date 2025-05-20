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
    bool shooting;
    Vector2 velocity;

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
        velocity = Vector2.Zero;
        shooting = false;
        color = Color.Blue;
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed() && !shooting)
        {
            shooting = true;
            velocity = (inputHelper.MousePosition - Painter.GameWorld.Cannon.Position) * 1.2f;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (shooting)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity.Y += 400.0f * dt;
            position += velocity * dt;
        }
        else
        {
            color = Painter.GameWorld.Cannon.Color;
            position = Painter.GameWorld.Cannon.BallPosition;
        }
        if (Painter.GameWorld.IsOutsideWorld(position))
        {
            Reset();
        }
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