using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace breakout
{
    public class Bonus : MovingSprite
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private BonusType type;

        public BonusType Type
        {
            get { return type; }
            set { type = value; }
        }

        private bool activated = false;

        public bool Activated
        {
            get { return activated; }
            set { activated = value; }
        }

        public Rectangle hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public Bonus(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        public Bonus(int screenWidth, int screenHeight, BonusType type)
            : base(screenWidth, screenHeight)
        {
            this.Type = type;
            Speed = 0.2f;
        }

        public void Update(GameTime gameTime, Rectangle batHitBox, GameLevel gameLevel, Brick brick)
        {
            Direction = new Vector2(0, 1);
            if (this.hitbox.Intersects(batHitBox))
            {
                switch (this.Type)
                {
                    case BonusType.HIGH_SPEED:
                        foreach (Ball b in gameLevel.Balls)
                        {
                            b.Speed *= 2;
                        }
                        break;
                    case BonusType.HIGH_RESISTANCE:
                        foreach (Brick b in gameLevel.BricksMap)
                        {
                            if (b.Resistance != 0 && b.Resistance != 4)
                            {
                                b.Resistance = 3;
                                b.UpdateTexture(gameLevel.BrickTexture);
                            }
                        }
                        break;
                    case BonusType.BAT_REDUCED:
                        gameLevel.Bat.Texture = gameLevel.BatTexture.Reduced;
                        break;
                    case BonusType.BAT_EXTENDED:
                        gameLevel.Bat.Texture = gameLevel.BatTexture.Extended;
                        break;
                    case BonusType.DOWN_LIFE:
                        if (gameLevel.Lives > 0)
                        {
                            gameLevel.Lives--;
                        }
                        break;
                    case BonusType.LOW_RESISTANCE:
                        foreach (Brick b in gameLevel.BricksMap)
                        {
                            if (b.Resistance != 0 && b.Resistance != 4)
                            {
                                b.Resistance = 1;
                                b.UpdateTexture(gameLevel.BrickTexture);
                            }
                        }
                        break;
                    case BonusType.LOW_SPEED:
                        foreach (Ball b in gameLevel.Balls)
                        {
                            b.Speed /= 2;
                        }
                        break;
                    case BonusType.UP_LIFE:
                        if (gameLevel.Lives < 4)
                        {
                            gameLevel.Lives++;
                        }
                        break;
                }

                Type = BonusType.NONE;
                Activated = false;
            } else if(Position.Y >= screenHeight/0.6){
                Activated = false;
            }

            base.Update(gameTime);
        }
    }
}
