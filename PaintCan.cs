using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class PaintCan
{
    Texture2D colorRed, colorGreen, colorBlue;
    Vector2 position, origin, velocity;
    Color color, targetColor;
    float minSpeed;

    public PaintCan(ContentManager Content, float positionOffset, Color target)
    {
        colorRed = Content.Load<Texture2D>("spr_can_red");
        colorGreen = Content.Load<Texture2D>("spr_can_green");
        colorBlue = Content.Load<Texture2D>("spr_can_blue");
        origin = new Vector2(colorRed.Width, colorRed.Height) / 2;

        targetColor = target;
        minSpeed = 30;
        position = new Vector2(positionOffset, -origin.Y);

        Reset();
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        minSpeed += 0.01f * dt;

        if (velocity != Vector2.Zero)
        {
            position += velocity * dt;

            // reset the can if it leaves the screen
            if (Painter.GameWorld.IsOutsideWorld(position - origin))
            {
                Reset();
            }
        }

        else if (Painter.Random.NextDouble() < 0.01)
        {
            velocity = CalculateRandomVelocity();
            color = CalculateRandomColor();
        }

        if (BoundingBox.Intersects(Painter.GameWorld.Ball.BoundingBox))
        {
            Color = Painter.GameWorld.Ball.Color;
            Painter.GameWorld.Ball.Reset();
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

    public void Reset()
    {
        color = Color.Blue;
        position.Y = -origin.Y;
        velocity = Vector2.Zero;
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

    public Vector2 Position
    {
        get { return position; }
    }

    public Color Color
    {
        get { return color; }
        set
        {
            if (value != Color.Red && value != Color.Green && value != Color.Blue)
            {
                return;
            }
            color = value;
        }
    }

    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = colorRed.Bounds;
            spriteBounds.Offset(position - origin);
            return spriteBounds;
        }
    }
}