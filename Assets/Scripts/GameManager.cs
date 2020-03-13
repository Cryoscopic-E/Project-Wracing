using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players;
    public GameObject playerPrefab;
    public Transform startPositionObject;
    Vector3 startPosition;
    public SplitScreenManager splitScreenManager;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    // Start is called before the first frame update
    void Start()
    {
        if (startPositionObject != null)
        {
            startPosition = startPositionObject.position;
        }
        else
        {
            startPosition = Vector3.zero;
        }

        if (!playerIndexSet || !prevState.IsConnected)
        {
            int x = 0;
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    GameObject temp = Instantiate(playerPrefab, startPosition + new Vector3(x,0,0), Quaternion.identity);
                    temp.GetComponent<MovementController>().player = testPlayerIndex;
                    x += 5;
                }
            }
        }
        
        splitScreenManager.CheckForPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
