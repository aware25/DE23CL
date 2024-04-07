using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    
    void Start()
    { 
        InputDecoder.readScript("Script/script");       
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

        if (InputDecoder.PausedHere && Input.GetMouseButtonDown(0) && InputDecoder.CommandLine < InputDecoder.Commands.Count - 1)
        {
            InputDecoder.CommandLine++;
        }

    }
}
