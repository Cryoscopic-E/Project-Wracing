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
    int currentPlayersListIndex = -1;
    public List<PlayerIndex> playersList = new List<PlayerIndex>();

    void Awake()
    {
        Screen.SetResolution(1024, 768, true);
    }

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
                playersList.Add((PlayerIndex)i);
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    GameObject temp = Instantiate(playerPrefab, startPosition + new Vector3(x,0,0), Quaternion.Euler(0.0f, 180.0f,0.0f));
                    temp.GetComponent<MovementController>().player = testPlayerIndex;
                    x += 5;
                }
            }
        }
        
        splitScreenManager.CheckForPlayers();
    }

    public PlayerIndex GetCurrentPlayerIndex()
    {
        currentPlayersListIndex++;
        return playersList[currentPlayersListIndex];
    }
}
