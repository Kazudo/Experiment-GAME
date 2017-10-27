using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Game1;


namespace Game1
{
    public class Player : Game
    {
        String name;
        AnimationHandler Walk;
        AnimationHandler Run;
        AnimationHandler Crouch;
        ComboHandler Attack;
        AnimationHandler Special;
        AnimationHandler Stance;
        Vector2 Location;
        char LastMove;
        int SpecialAttTimer;

        public Player(String sN, GraphicsDeviceManager graphics)
        {
            if (sN.Equals("Naruto") || sN.Equals("naruto"))
            {
                name = sN;
                Run = new AnimationHandler(new Rectangle[] { new Rectangle(1, 0, 49, 59), new Rectangle(57, 0, 63, 59), new Rectangle(132, 0, 55, 59),
                                        new Rectangle(196, 0, 45, 59), new Rectangle(247, 0, 60, 59), new Rectangle(318, 0, 56, 59) });

                Walk = new AnimationHandler(new Rectangle[] { new Rectangle(4, 0, 26, 71), new Rectangle(39, 0, 43, 71), new Rectangle(93, 0, 42, 71),
                                         new Rectangle(144, 0, 31, 71), new Rectangle(183, 0, 40, 71), new Rectangle(233, 0, 36, 71) });

                Crouch = new AnimationHandler(new Rectangle[] { new Rectangle(0, 0, 36, 65), new Rectangle(50, 0, 35, 65), new Rectangle(98, 0, 34, 65) });

                Special = new AnimationHandler(new Rectangle[] {  new Rectangle(2, 0, 79, 94), new Rectangle(90, 0, 105, 94), new Rectangle(206, 0, 90, 94), new Rectangle(305, 0, 113, 94),
                                            new Rectangle(427, 0, 132, 94), new Rectangle(574, 0, 155, 94)});

                Attack = new ComboHandler("BasicCombo", new AnimationHandler[] {new AnimationHandler(new Rectangle[] { new Rectangle(0, 0, 48, 66), new Rectangle(49, 0, 56, 66), new Rectangle(107, 0, 75, 66),
                                                                                                    new Rectangle(185, 0, 61, 66) }),
                                                                                new AnimationHandler(new Rectangle[] { new Rectangle(7, 0, 51, 60), new Rectangle(66, 0, 58, 60), new Rectangle(133, 0, 59, 60),
                                                                                                    new Rectangle(199 ,0 ,63 ,60) }),
                                                                                new AnimationHandler(new Rectangle[] { new Rectangle(3, 0, 46, 66), new Rectangle(59, 0, 60, 66) , new Rectangle(126, 0, 63, 66),
                                                                                                    new Rectangle(198, 0, 54, 66)}) });

                Stance = new AnimationHandler(new Rectangle[] { new Rectangle(0, 0, 56, 64) });

                Location = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight - 80);
                SpecialAttTimer = -11;
            }
        }

        //Getters and Setters
        public String GetName()
        {
            return name;
        }
        public AnimationHandler GetWalk()
        {
            return Walk;
        }
        public AnimationHandler GetRun()
        {
            return Run;
        }
        public AnimationHandler GetCrouch()
        {
            return Crouch;
        }
        public ComboHandler GetAttack()
        {
            return Attack;
        }
        public AnimationHandler GetSpecial()
        {
            return Special;
        }
        public AnimationHandler GetStance()
        {
            return Stance;
        }
        public Vector2 GetLocation()
        {
            return Location;
        }
        public void SetLocation(Vector2 v)
        {
            Location = v;
        }
        public char GetLastMove()
        {
            return LastMove;
        }
        public void SetLastMove(char c)
        {
            LastMove = c;
        }
        public int GetSpecialAttTimer()
        {
            return SpecialAttTimer;
        }
        public void SetSpecialAttTimer(int time)
        {
            SpecialAttTimer = time;
        }

        public void LoadPlayer(ContentManager c)
        {
            String sN = name.ToUpper();
            sN = sN.Remove(1);
            sN = sN.Insert(1, name.Substring(1));
            Walk.SetTex(c.Load<Texture2D>(sN.Insert(sN.Length, "/walk")));
            Run.SetTex(c.Load<Texture2D>(sN.Insert(sN.Length, "/run")));
            GetCrouch().SetTex(c.Load<Texture2D>(sN.Insert(sN.Length, "/crouch")));
            Special.SetTex(c.Load<Texture2D>(sN.Insert(sN.Length, "/Big_Rasengan")));
            Stance.SetTex(c.Load<Texture2D>(sN.Insert(sN.Length, "/stance")));
            Attack.LoadCombo(c, sN);
        }

        public void WalkLeft(SpriteBatch spriteBatch)
        {
            int currIndex;

            if (LastMove == 'r')
                ResetAll();
            else
            {
                Run.SetIndex(0);
                Crouch.SetIndex(0);
                Stance.SetIndex(0);
                Attack.Reset();
                Special.SetIndex(0);
            }

            currIndex = Walk.GetIndex();
            Location = new Vector2(Location.X - 20, Location.Y);
            if (currIndex < Walk.GetAnimationLength())
            {
                spriteBatch.Draw(Walk.GetTex(), Location, Walk.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                Walk.SetIndex(++currIndex);
            }
            else
            {
                currIndex = 0;
                Walk.SetIndex(currIndex);
                spriteBatch.Draw(Walk.GetTex(), Location, Walk.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                Walk.SetIndex(++currIndex);
            }
            LastMove = ('l');
        }
        public void RunLeft(SpriteBatch spriteBatch)
        {
            int currIndex;

            if (LastMove == 'r')
                ResetAll();
            else
            {
                Walk.SetIndex(0);
                Crouch.SetIndex(0);
                Stance.SetIndex(0);
                Attack.Reset();
                Special.SetIndex(0);
            }

            currIndex = Run.GetIndex();
            Location = new Vector2(Location.X - 40, Location.Y);
            if (currIndex < Run.GetAnimationLength())
            {
                spriteBatch.Draw(Run.GetTex(), Location, Run.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                Run.SetIndex(++currIndex);
            }
            else
            {
                currIndex = 0;
                Run.SetIndex(currIndex);
                spriteBatch.Draw(Run.GetTex(), Location, Run.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                Run.SetIndex(++currIndex);
            }
            LastMove =('l');
        }
        public void CrouchLeft(SpriteBatch spriteBatch)
        {
            int currIndex;

            Walk.SetIndex(0);
            Run.SetIndex(0);
            Stance.SetIndex(0);
            Attack.Reset();
            Special.SetIndex(0);

            currIndex = Crouch.GetIndex();
            if (currIndex < Crouch.GetAnimationLength() - 1)
            {
                spriteBatch.Draw(Crouch.GetTex(), Location, Crouch.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                Crouch.SetIndex(++currIndex);
            }
            else
                spriteBatch.Draw(Crouch.GetTex(), Location, Crouch.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
        }
        public void StanceLeft(SpriteBatch spriteBatch)
        {
            ResetAll();
            spriteBatch.Draw(Stance.GetTex(), Location, Stance.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
        }

        public void WalkRight(SpriteBatch spriteBatch)
        {
            int currIndex;

            if (LastMove == 'l')
                ResetAll();
            else
            {
                Run.SetIndex(0);
                Crouch.SetIndex(0);
                Stance.SetIndex(0);
                Attack.Reset();
                Special.SetIndex(0);
            }

            currIndex = Walk.GetIndex();
            Location = new Vector2(Location.X + 20, Location.Y);
            if (currIndex < Walk.GetAnimationLength())
            {
                spriteBatch.Draw(Walk.GetTex(), Location, Walk.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                Walk.SetIndex(++currIndex);
            }
            else
            {
                currIndex = 0;
                Walk.SetIndex(currIndex);
                spriteBatch.Draw(Walk.GetTex(), Location, Walk.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                Walk.SetIndex(++currIndex);
            }
            LastMove = ('r');
        }
        public void RunRight(SpriteBatch spriteBatch)
        {
            int currIndex;

            if (LastMove == 'l')
                ResetAll();
            else
            {
                Walk.SetIndex(0);
                Crouch.SetIndex(0);
                Stance.SetIndex(0);
                Attack.Reset();
                Special.SetIndex(0);
            }

            currIndex = Run.GetIndex();
            Location = new Vector2(Location.X + 40, Location.Y);
            if (currIndex < Run.GetAnimationLength())
            {
                spriteBatch.Draw(Run.GetTex(), Location, Run.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                Run.SetIndex(++currIndex);
            }
            else
            {
                currIndex = 0;
                Run.SetIndex(currIndex);
                spriteBatch.Draw(Run.GetTex(), Location, Run.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                Run.SetIndex(++currIndex);
            }
            LastMove = ('r');
        }
        public void CrouchRight(SpriteBatch spriteBatch)
        {
            int currIndex;


            Walk.SetIndex(0);
            Run.SetIndex(0);
            Stance.SetIndex(0);
            Attack.Reset();
            Special.SetIndex(0);

            currIndex = Crouch.GetIndex();
            if (currIndex < Crouch.GetAnimationLength() - 1)
            {
                spriteBatch.Draw(Crouch.GetTex(), Location, Crouch.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                Crouch.SetIndex(++currIndex);
            }
            else
                spriteBatch.Draw(Crouch.GetTex(), Location, Crouch.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
        }
        public void StanceRight(SpriteBatch spriteBatch)
        {
            ResetAll();
            spriteBatch.Draw(Stance.GetTex(), Location, Stance.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
        }

        public void BasicCombo(SpriteBatch spriteBatch, GameTime gameTime, int clicks)
        {
            AnimationHandler currentComboPart;
            int currIndexCombo, currIndex;

            Walk.SetIndex(0);
            Run.SetIndex(0);
            Crouch.SetIndex(0);
            Stance.SetIndex(0);
            Special.SetIndex(0);

            currIndexCombo = Attack.GetComboPart();
            if (currIndexCombo <= clicks && currIndexCombo < Attack.GetComboLength() - 1)
            {
                currentComboPart = Attack.GetCurrentPart();
                currIndex = currentComboPart.GetIndex();
                if (currIndex < currentComboPart.GetAnimationLength())
                {
                    if (LastMove == 'r')
                        spriteBatch.Draw(currentComboPart.GetTex(), Location, currentComboPart.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                    else
                        spriteBatch.Draw(currentComboPart.GetTex(), Location, currentComboPart.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                    currentComboPart.SetIndex(++currIndex);
                    Attack.SetInCombo(true);
                    if (currIndex - 1 == 0)
                        Attack.SetComboStart(gameTime);
                }
                else
                {
                    currIndex = 0;
                    currentComboPart.SetIndex(currIndex);
                    if (currIndexCombo < clicks)
                    {
                        Attack.SetComboPart(++currIndexCombo);
                        currentComboPart = Attack.GetCurrentPart();
                        if (LastMove == 'r')
                            spriteBatch.Draw(currentComboPart.GetTex(), Location, currentComboPart.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                        else
                            spriteBatch.Draw(currentComboPart.GetTex(), Location, currentComboPart.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                        currentComboPart.SetIndex(++currIndex);
                        if (currIndex - 1 == 0)
                            Attack.SetComboStart(gameTime);
                    }
                    else
                    {
                        if (LastMove == 'r')
                            StanceRight(spriteBatch);
                        else
                            StanceLeft(spriteBatch);
                    }
                }
            }
            else//Happens only on last part, and only when it needs to happen
            {
                currentComboPart = Attack.GetCurrentPart();
                currIndex = currentComboPart.GetIndex();
                if (currIndex < currentComboPart.GetAnimationLength())
                {
                    if (LastMove == 'r')
                        spriteBatch.Draw(currentComboPart.GetTex(), Location, currentComboPart.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                    else
                        spriteBatch.Draw(currentComboPart.GetTex(), Location, currentComboPart.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                    currentComboPart.SetIndex(++currIndex);
                }
                else
                {
                    if (LastMove == 'r')
                        StanceRight(spriteBatch);
                    else
                        StanceLeft(spriteBatch);
                }
            }
        }
        public void SpecialAttack(SpriteBatch spriteBatch, GameTime gametime)
        {
            int currIndex;

            Walk.SetIndex(0);
            Run.SetIndex(0);
            Crouch.SetIndex(0);
            Stance.SetIndex(0);
            Attack.Reset();

            currIndex = Special.GetIndex();
            if (currIndex < Special.GetAnimationLength() - 1)
            {
                if (LastMove == 'r')
                    spriteBatch.Draw(Special.GetTex(), new Vector2(Location.X, Location.Y - 30), Special.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(Special.GetTex(), new Vector2(Location.X - (Special.GetRect().Width - 56), Location.Y - 30), Special.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                Special.SetIndex(++currIndex);
            }
            else
            {
                if (LastMove == 'r')
                    spriteBatch.Draw(Special.GetTex(), new Vector2(Location.X, Location.Y - 30), Special.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(Special.GetTex(), new Vector2(Location.X - (Special.GetRect().Width - 56), Location.Y - 30), Special.GetRect(), Color.White, 0, new Vector2(0), new Vector2(1), SpriteEffects.FlipHorizontally, 0);
                SpecialAttTimer = gametime.TotalGameTime.Seconds;
            }
        }

        public Boolean IsInBasicCombo(GameTime gameTime)
        {
            if (Attack.GetInCombo() && Attack.GetComboStart() + 5 > gameTime.TotalGameTime.Seconds)
                return true;
            if (Attack.GetInCombo())
                Attack.Reset();
            return false;
        }
        private void ResetAll()
        {
            Run.SetIndex(0);
            Walk.SetIndex(0);
            Crouch.SetIndex(0);
            Special.SetIndex(0);
            Attack.Reset();
        }
    }
}
