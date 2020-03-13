﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CharacterStats : MonoBehaviour
{
    public float stunTime;
    public bool stunned;

    public Item currentItem;
    private CharacterController controller;
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                    CheckInputs();
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            UseItem();
        }


    }

    private void CheckInputs()
    {
        
    }

    void UseItem()
    {
        if (currentItem)
        {
            currentItem.Activate();
            currentItem = null;
        }
        else
        {
            Debug.Log("NO ITEM!!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && currentItem == null)
        {
            other.GetComponent<Item>().RandomItemSelector(gameObject, 10);
        }
    }
}