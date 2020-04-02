using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CharacterStats : MonoBehaviour
{
    public bool stunned = false;
    public GameObject boostImage;
    public GameObject attackImage;
    public Item currentItem;
    private CharacterController controller;
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.players.Add(gameObject);
        playerIndex = gameManager.GetCurrentPlayerIndex();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
            return;

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
                    //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                   // playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        //prevState = state;
        state = GamePad.GetState(playerIndex);

        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
           // Debug.Log("Pressed A button + " + playerIndex);
            UseItem();
        }


        prevState = state;
    }


    public void Stun(float duration)
    {
        Debug.Log("STUN CALLED");
        if (!stunned)
        {
            stunned = true;
            Invoke("Recover", duration);
        }
    }

    void Recover()
    {
        stunned = false;
    }

    void UseItem()
    {
        if (currentItem)
        {
            Debug.Log("ITEM Activated");
            currentItem.Activate();
            currentItem = null;
            boostImage.SetActive(false);
            attackImage.SetActive(false);
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
            other.GetComponent<Item>().RandomItemSelector(gameObject, 5);
        }
    }
}
