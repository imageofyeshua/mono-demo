using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

public class PaintCan : ThreeColorGameObject
{
    Color targetColor;
    float minSpeed;
    SoundEffect soundCollect;

    public PaintCan(ContentManager Content, float positionOffset, Color targetcol)
        : base(Content, "spr_can_red", "spr_can_green", "spr_can_blue")
    {
        position = new Vector2(positionOffset, -origin.Y);
        targetColor = targetcol;
        minSpeed = 30;

        Reset();

        soundCollect = Content.Load<SoundEffect>("snd_collect_points");
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        minSpeed += 0.01f * dt;
        rotation = (float)Math.Sin(position.Y / 50.0) * 0.05f;

        base.Update(gameTime);

        if (velocity != Vector2.Zero)
        {
            position += velocity * dt;

            if (BoundingBox.Intersects(Painter.GameWorld.Ball.BoundingBox))
            {
                Color = Painter.GameWorld.Ball.Color;
                Painter.GameWorld.Ball.Reset();
            }

            // reset the can if it leaves the screen
            if (Painter.GameWorld.IsOutsideWorld(position - origin))
            {
                if (Color != targetColor)
                    Painter.GameWorld.LoseLife();
                else
                {
                    Painter.GameWorld.Score += 10;
                    soundCollect.Play();
                }
                Reset();
            }
        }

        else if (Painter.Random.NextDouble() < 0.01)
        {
            velocity = CalculateRandomVelocity();
            Color = CalculateRandomColor();
        }

    }

    public override void Reset()
    {
        base.Reset();
        position.Y = -origin.Y;
        velocity = Vector2.Zero;
    }

    public void ResetMinSpeed()
    {
        minSpeed = 30;
    }

    Vector2 CalculateRandomVelocity()
    {
        return new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + minSpeed);
    }

    Color CalculateRandomColor()
    {
        int randomVal = Painter.Random.Next(3);
        if (randomVal == 0)
            return Color.Red;
        else if (randomVal == 1)
            return Color.Green;
        else
            return Color.Blue;
    }
}