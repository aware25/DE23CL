using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class InputDecoder
{
    public static List<Character> CharacterList = new List<Character> ();

    public static GameObject InterfaceElements = GameObject.Find("UI_Elements");


    // find and define background image

    // main menu testing

    //private static GameObject mainMenu_canvas = GameObject.Find("MainMenu");
    //private static GameObject mainMenu_ImageInst = Resources.Load("Prefabs/MainMenu") as GameObject;


    // main menu testing




    private static GameObject canvas = GameObject.Find("ImageLayers");
    private static GameObject ImageInst = Resources.Load("Prefabs/Background") as GameObject;

    public static GameObject DialogueTextObject = GameObject.Find("Dialogue_Text");
    public static GameObject NamePlateTextObject = GameObject.Find("NamePlate_Text");

    public static GameObject MindMenu = GameObject.Find("MindMenu");



    public static bool PausedHere = false;

    public static List<Label> labels = new List<Label>();

    [NonSerialized]
    public static List<string> Commands = new List<string>();

    public string inputLine;
    public static int CommandLine = 0;
    public static string lastCommand = "";

    public static void ParseInputLine(string StringToParse)
    {
        string withOutTabs = StringToParse.Replace("\t", "");
        StringToParse = withOutTabs;

        if (StringToParse.StartsWith("\""))
        {
            Say(StringToParse);
        }

        string[] SeparatingString = { " ", "\"", "(", ")" };
        string[] args = StringToParse.Split(SeparatingString, StringSplitOptions.RemoveEmptyEntries);
        
        foreach(Character character in CharacterList)
        {
            if (args[0] == character.shortName)
            {
                SplitToSay(StringToParse, character);
            }
        }

        //Debug.Log(args[1]);
        if (args[0] == "C")
        {

            GameObject.Find("ContinueButton").SetActive(false);

        }
        if (args[0] == "show")
        {
            showImage(StringToParse);
        }

        if (args[0] == "clrscr")
        {
            ClearScreen();
        }

        if (args[0] == "Character")
        {
            CreateNewCharacter(StringToParse);
        }

        if (args[0] == "screen")
        {
            ScreenClear(StringToParse);
        }

        if (args[0] == "jump")
        {
            jumpTo(StringToParse);
        }

        if (args[0] == "no_mind")
        {
            MindMenu.SetActive(false);
        }
    }

    #region Say Stuff

    public static void SplitToSay(string StringToParse, Character character)
    {
        int toQuote = StringToParse.IndexOf("\"") + 1;
        int endQuote = StringToParse.Length - 1;
        string StringToOutput = StringToParse.Substring(toQuote, endQuote - toQuote);
        Say(character.fullName, StringToOutput);
    }

    public static void Say(string what)
    {        
        if (!InterfaceElements.activeInHierarchy) InterfaceElements.SetActive(true);
        DialogueTextObject.GetComponent<TextMeshProUGUI>().text = what;
        PausedHere = true;
    }

    public static void Say(string who, string what)
    {        
        if (!InterfaceElements.activeInHierarchy) InterfaceElements.SetActive(true);
        NamePlateTextObject.GetComponent<TextMeshProUGUI>().text = who;
        DialogueTextObject.GetComponent<TextMeshProUGUI>().text = what;
        PausedHere = true;
    }

    #endregion

    #region Image Stuff

    public static void showImage(string StringToParse)
    {
        string ImageToShow = null;
        bool fadeEffect = false;
        var ImageToUse = new Regex(@"show (?<ImageFileName>[^.]+)");
        var ImageToUseTransition = new Regex(@"show (?<ImageFileName>[^.]+) with (?<TransitionName>[^.]+)");

        var matches = ImageToUse.Match(StringToParse);
        var altMatches = ImageToUseTransition.Match(StringToParse);

        if (altMatches.Success)
        {
            ImageToShow = altMatches.Groups["ImageFileName"].ToString();
            fadeEffect = true;
        }
        else if (matches.Success)
        {
            ImageToShow = matches.Groups["ImageFileName"].ToString();
        }

        GameObject PictureInstance = GameObject.Instantiate(ImageInst);
        PictureInstance.transform.SetParent(canvas.transform, false);
        PictureInstance.GetComponent<ImageInstance>().FadeIn = fadeEffect;
        PictureInstance.GetComponent<Image>().color = Color.white;
        PictureInstance.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/" + ImageToShow);
    }

    public static void ClearScreen()
    {
        foreach (Transform t in canvas.transform)
        {
            MonoBehaviour.Destroy(t.gameObject);
        }
        InterfaceElements.SetActive(false);
    }

    public static void ScreenClear(string StringToParse)
    {
        
        string ImageToShow = null;
        bool fadeEffect = false;
        var ImageToUse = new Regex(@"screen (?<ImageFileName>[^.]+)");
        var ImageToUseTransition = new Regex(@"screen (?<ImageFileName>[^.]+) with (?<TransitionName>[^.]+)");

        var matches = ImageToUse.Match(StringToParse);
        var altMatches = ImageToUseTransition.Match(StringToParse);

        if (altMatches.Success)
        {
            ImageToShow = altMatches.Groups["ImageFileName"].ToString();
            fadeEffect = true;
        }
        else if (matches.Success)
        {
            ImageToShow = matches.Groups["ImageFileName"].ToString();
        }

        GameObject PictureInstance = GameObject.Instantiate(ImageInst);
        PictureInstance.transform.SetParent(canvas.transform, false);
        PictureInstance.GetComponent<ImageInstance>().FadeIn = fadeEffect;
        PictureInstance.GetComponent<Image>().color = Color.white;
        PictureInstance.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/" + ImageToShow);

        foreach (Transform t in canvas.transform)
        {
            if (t != PictureInstance.transform)
            {
                MonoBehaviour.Destroy(t.gameObject, 3f);
            }
            InterfaceElements.SetActive(false);
        }
        
    }

    #endregion

    #region New Character

    public static void CreateNewCharacter(string StringToParse)
    {
        Debug.Log("start createnewcharacter");
        string newCharShortName = null;
        string newCharFullName = null;
        Color newCharColor = Color.white;
        string newCharSideImage = null;
        
        var characterExpression = new Regex(@"Character\((?<shortName>[a-zA-Z0-9_]+), (?<fullName>[a-zA-Z0-9_]+), color=(?<characterColor>[a-zA-Z0-9_]+), image=(?<sideImage>[a-zA-Z0-9_]+)\)");
        var characterExpressionA = new Regex(@"Character\((?<shortName>[a-zA-Z0-9_]+), (?<fullName>[a-zA-Z0-9_]+), color=(?<characterColor>[a-zA-Z0-9_]+)\)");
        var characterExpressionB = new Regex(@"Character\((?<shortName>[a-zA-Z0-9_]+), (?<fullName>[a-zA-Z0-9_]+)\)");
        var characterExpressionC = new Regex(@"Character\((?<shortName>[a-zA-Z0-9_]+), (?<fullName>[a-zA-Z0-9_]+), image=(?<sideImage>[a-zA-Z0-9_]+)\)");
        Debug.Log("character expression: ");
        Debug.Log(characterExpression);
        Debug.Log(StringToParse);
        /*
        if (characterExpression.IsMatch(StringToParse))
        {
            Debug.Log("1");
        }
        else
        {
            Debug.Log("0");
        }
        */


        if (characterExpression.IsMatch(StringToParse))
        {
            var matches = characterExpression.Match(StringToParse);
            newCharShortName = matches.Groups["shortName"].ToString();
            newCharFullName = matches.Groups["fullName"].ToString();
            newCharColor = Color.clear; ColorUtility.TryParseHtmlString(matches.Groups["characterColor"].ToString(), out newCharColor);
            newCharSideImage = matches.Groups["sideImage"].ToString();
            Debug.Log("Char Done");
        }
        else if (characterExpressionA.IsMatch(StringToParse))
        {
            var matches = characterExpressionA.Match(StringToParse);
            newCharShortName = matches.Groups["shortName"].ToString();
            newCharFullName = matches.Groups["fullName"].ToString();
            newCharColor = Color.clear; ColorUtility.TryParseHtmlString(matches.Groups["characterColor"].ToString(), out newCharColor);
            Debug.Log("CharA Done");
        }
        else if (characterExpressionB.IsMatch(StringToParse))
        {
            var matches = characterExpressionB.Match(StringToParse);
            newCharShortName = matches.Groups["shortName"].ToString();
            newCharFullName = matches.Groups["fullName"].ToString();
            Debug.Log("CharB Done");
        }
        else if (characterExpressionC.IsMatch(StringToParse))
        {
            var matches = characterExpressionC.Match(StringToParse);
            newCharShortName = matches.Groups["shortName"].ToString();
            newCharFullName = matches.Groups["fullName"].ToString();
            newCharSideImage = matches.Groups["sideImage"].ToString();
            Debug.Log("CharC Done");
        }
        else 
        {
            Debug.Log("no match found");
        }

        Debug.Log(StringToParse);
        Debug.Log(newCharShortName);
        Debug.Log(newCharFullName);
        Debug.Log(newCharColor);
        Debug.Log(newCharSideImage);

        CharacterList.Add(new Character(newCharShortName, newCharFullName, newCharColor, newCharSideImage));

    }

    #endregion

    public static void jumpTo(string StringToParse)
    {
        var tempStringSplit = StringToParse.Split(' ');
        foreach (Label l in labels)
        {
            if (l.LabelName == tempStringSplit[1])
            {
                CommandLine = l.LabelIndex;
                PausedHere = false;
            }
        }
    }

    #region LoadingScript

    public static void readScript(string file_Path)
    {
        TextAsset commandFile = Resources.Load(file_Path) as TextAsset;
        var commandArray = commandFile.text.Split('\n');

        foreach (var line in commandArray)
        {
            Commands.Add(line);
        }

        for (int x = 0; x < Commands.Count; x++)
        {
            if (Commands[x].StartsWith("label"))
            {
                var labelSplit = Commands[x].Split(' ');
                labels.Add(new Label(labelSplit[1], x));
            }
        }
    }

    #endregion
}