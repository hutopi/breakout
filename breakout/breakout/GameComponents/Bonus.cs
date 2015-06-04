using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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

        private SoundManager sm = new SoundManager();

        public Rectangle Hitbox
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

        public override void LoadContent(ContentManager content, String assetName)
        {
            sm.LoadContent(content);
            base.LoadContent(content, assetName);
        }

        public void Update(GameTime gameTime, Rectangle batHitBox, GameLevel gameLevel, Brick brick)
        {
            Direction = new Vector2(0, 1);
            if (this.Hitbox.Intersects(batHitBox))
            {
                switch (this.Type)
                {
                    case BonusType.HIGH_SPEED:
                        this.sm.powerDown.Play();
                        foreach (Ball b in gameLevel.Balls)
                        {
                            b.Speed *= 1.5f;
                        }
                        break;
                    case BonusType.HIGH_RESISTANCE:
                        this.sm.powerDown.Play();
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
                        this.sm.powerDown.Play();
                        gameLevel.Bat.Texture = gameLevel.BatTexture.Reduced;
                        break;
                    case BonusType.BAT_EXTENDED:
                        this.sm.powerUp.Play();
                        gameLevel.Bat.Texture = gameLevel.BatTexture.Extended;
                        break;
                    case BonusType.DOWN_LIFE:
                        this.sm.powerDown.Play();
                        if (gameLevel.Lives > 0)
                        {
                            gameLevel.Lives--;
                        }
                        break;
                    case BonusType.LOW_RESISTANCE:
                        this.sm.powerUp.Play();
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
                        this.sm.powerUp.Play();
                        foreach (Ball b in gameLevel.Balls)
                        {
                            b.Speed /= 1.5f;
                        }
                        break;
                    case BonusType.UP_LIFE:
                        this.sm.powerUp.Play();
                        if (gameLevel.Lives < 4)
                        {
                            gameLevel.Lives++;
                        }
                        break;
                    case BonusType.MULTIPLICATE_BALL:
                        Ball newBall = null;
                        foreach (Ball b in gameLevel.Balls)
                        {
                            newBall = new Ball(b);
                        }
                        gameLevel.Balls.Add(newBall);
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
