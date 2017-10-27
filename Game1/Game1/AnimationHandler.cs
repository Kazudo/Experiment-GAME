using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class AnimationHandler : Game
{
    protected Texture2D tex;
    protected Rectangle[] AnimationPics;
    protected int currIndex;

    public AnimationHandler(Rectangle[] arr)
    {
        AnimationPics = arr;
        currIndex = 0;
    }

    public void SetTex(Texture2D t)
    {
        tex = t;
    }
    public Texture2D GetTex()
    {
        return tex;
    }
    public void SetIndex(int si)
    {
        currIndex = si;
    }
    public int GetIndex()
    {
        return currIndex;
    }
    public Rectangle GetRect()
    {
        return AnimationPics[currIndex];
    }
    public int GetAnimationLength()
    {
        return AnimationPics.Length;
    }
}



