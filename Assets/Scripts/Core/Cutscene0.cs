using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene0 : MonoBehaviour
{

    void Start()
    {
        InputDecoder.readScript("Script/cutScene0_script");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            if (InputDecoder.InterfaceElements.activeInHierarchy)
            {
                InputDecoder.InterfaceElements.SetActive(false);
            }
            else
            {
                InputDecoder.InterfaceElements.SetActive(true);
            }
        }
        
        if (InputDecoder.Commands[InputDecoder.CommandLine] != InputDecoder.lastCommand)
        {
            InputDecoder.lastCommand = InputDecoder.Commands[InputDecoder.CommandLine];
            InputDecoder.PausedHere = false;
            InputDecoder.ParseInputLine(InputDecoder.Commands[InputDecoder.CommandLine]);
        }
        
        if (!InputDecoder.PausedHere && InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
        {
            InputDecoder.CommandLine++;
        }
        /*
        if (InputDecoder.PausedHere && Input.GetMouseButtonDown(0) && InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
        {
            InputDecoder.CommandLine++;
        }
        */
    }

    public static void GoodChoice()
    {
        if (InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
        {
            InputDecoder.CommandLine++;
        }
    }

    public static void BadChoice()
    {
        SceneManager.LoadScene(2);
        //InputDecoder.readScript("Script/badEnding0_script");
    }


    public static void OnClick()
    {
        if (InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
        {
            InputDecoder.CommandLine++;
        }
    }
}
