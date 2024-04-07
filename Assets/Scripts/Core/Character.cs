using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string shortName;
    public string fullName;
    public Color color;
    public string sideImage;

    public Character(string shortNameInput, string fullNameInput, Color colorInput, string sideImageInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.sideImage = sideImageInput;

        CheckNames();
    }

    public Character(string shortNameInput, string fullNameInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = Color.white;
        this.sideImage = null;

        CheckNames();
    }

    public Character(string shortNameInput, string fullNameInput, Color colorInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.sideImage = null;

        CheckNames();
    }

    public void CheckNames()
    {
        if (this.shortName == null)
        {
            Debug.Log(this.shortName);
            throw new InvalidPropertyException("Short Name must contain a string");
        }

        if (this.fullName == null)
        {
            throw new InvalidPropertyException("Full Name must contain a string");
        }
    }
}
