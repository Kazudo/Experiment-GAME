using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class ComboHandler : Game
{
    String comboName;
    AnimationHandler[] Combo;
    int comboPart;
    int comboStart;
    Boolean inCombo;
    int clicked;

	public ComboHandler(String name, AnimationHandler[] arr)
	{
        comboName = name;
        Combo = arr;
        comboPart = 1;
        inCombo = false;
        comboStart = 0;
        clicked = -1;
	}
    
    //Getters and Setters
    public AnimationHandler GetCurrentPart()
    {
        return Combo[comboPart];
    }
    public int GetComboLength()
    {
        return Combo.Length;
    }
    public int GetComboPart()
    {
        return comboPart;
    }
    public void SetComboPart(int val)
    {
        comboPart = val;
    }
    public Boolean GetInCombo()
    {
        return inCombo;
    }
    public void SetInCombo(Boolean b)
    {
        inCombo = b;
    }
    public int GetComboStart()
    {
        return comboStart;
    }
    public void SetComboStart(int val)
    {
        comboStart = val;
    }
    public void SetComboStart(GameTime gameTime)
    {
        comboStart = gameTime.TotalGameTime.Seconds;
    }
    public int GetClicked()
    {
        return clicked;
    }
    public void SetClicked(int val)
    {
        clicked = val;
    }

    public void LoadCombo(Microsoft.Xna.Framework.Content.ContentManager c, String name)
    {
        for (int i = 0; i < Combo.Length; i++)
        {
            String s = name.Insert(name.Length, "/");
            s = s.Insert(s.Length, comboName);
            s = s.Insert(s.Length, i.ToString());
            Combo[i].SetTex(c.Load<Texture2D>(s));
        }
    }
    public void Reset()
    {
        Combo[comboPart].SetIndex(0);
        comboPart = 0;
        inCombo = false;
        comboStart = 0;
        clicked = -1;
    }
}
    

