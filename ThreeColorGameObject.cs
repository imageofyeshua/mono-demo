using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class ThreeColorGameObject
{
    protected Texture2D colorRed, colorGreen, colorBlue;
    Color color;
    protected Vector2 position, origin, velocity;
    protected float rotation;

    public virtual void Reset()
    {
        Color = Color.Blue;
    }

    public Vector2 Position
    {
        get { return position; }
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

    public Color Color
    {
        get { return color; }
        protected set
        {
            if (value != Color.Red && value != Color.Green && value != Color.Blue)
            {
                return;
            }
            color = value;
        }
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {

    }

    public virtual void Update(GameTime gameTime)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Texture2D currentSprite;
        if (Color == Color.Red)
            currentSprite = colorRed;
        else if (Color == Color.Green)
            currentSprite = colorGreen;
        else
            currentSprite = colorBlue;

        spriteBatch.Draw(currentSprite, position, null, Color.White,
            rotation, origin, 1.0f, SpriteEffects.None, 0);
    }

    protected ThreeColorGameObject(ContentManager content,
        string redSprite, string greenSprite, string blueSprite)
    {
        colorRed = content.Load<Texture2D>(redSprite);
        colorGreen = content.Load<Texture2D>(greenSprite);
        colorBlue = content.Load<Texture2D>(blueSprite);

        origin = new Vector2(colorRed.Width / 2.0f, colorRed.Height / 2.0f);

        position = Vector2.Zero;
        velocity = Vector2.Zero;
        rotation = 0;

        Reset();
    }
}