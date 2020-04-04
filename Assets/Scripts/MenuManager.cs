using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public int controllers;
    public Text text;
    UnityEngine.UI.Button startButton;
    void Start()
    {
        startButton = GameObject.Find("Start Button").GetComponent<UnityEngine.UI.Button>();
    }
    void Update()
    {
        int temp = 0;
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    temp++;
                }
            }
            controllers = temp;
            if(controllers > 0)
            {
                text.GetComponent<Text>().text = "Found " + controllers + " controllers";
                startButton.interactable = true;
            }
            else
            {
                text.GetComponent<Text>().text = "Please insert 1 or more controllers";
                startButton.interactable = false;
            }
        }
    }

    public void StartGame()
    {
        if(controllers > 0)
        {
            SceneManager.LoadScene("TestLevel", LoadSceneMode.Single);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
