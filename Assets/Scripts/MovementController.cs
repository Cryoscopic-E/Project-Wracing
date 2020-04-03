using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
public class MovementController : MonoBehaviour
{


    public float accellerationMultiplier = 2.0f;
    public float maxSpeed = 40.0f;
        
    private CharacterController controller;

    private Vector2 input;

    private float speed = 0.0f;

    //float rotationSpeedX = 25.0f;
    //float rotationSpeedY = 15.5f;
    float rotationSpeedX = 1.5f;
    float rotationSpeedY = .5f;

    public float acceleration;
    bool playerIndexSet = false;
    public PlayerIndex player;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    bool canBoost = false;
    float boostTime;
    float boostTimer = 0.0f;
    CharacterStats stats;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<CharacterStats>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.stunned)
        {
            canBoost = false;
            boostTimer = 0.0f;
            boostTime = 0.0f;
            return;
        }

        // boost timer
        if (canBoost)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= boostTime && canBoost)
            {
                print("BOOST DEACTIVATED");
                canBoost = false;
                boostTimer = 0.0f;
                boostTime = 0.0f;
            }
        }
            
        
        
        

        if(transform.position.y > 120.0f)
        {
            print("out");
        }

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
                  //  Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }



        // move

        prevState = state;
        state = GamePad.GetState(player);

        // check inputs and set direction
        CheckInputs();

        acceleration = state.Triggers.Right * accellerationMultiplier;

        if (acceleration > 0.0f)
        {
            speed += acceleration;
            speed = Mathf.Clamp(speed, 0.1f, canBoost ? maxSpeed * 2 : maxSpeed);
        }
        else
        {
            speed = Mathf.Lerp(speed, 0, 0.1f);
        }
        Vector3 movement = transform.forward;

        float rotationOverride = Mathf.Max(1.2f, (speed / maxSpeed));
        Vector3 yaw = input.x * transform.right * rotationSpeedX * rotationOverride * Time.deltaTime;
        Vector3 pitch = input.y * transform.up * rotationSpeedY * rotationOverride * Time.deltaTime;
        Vector3 direction = yaw + pitch;

        float X = Quaternion.LookRotation(direction + transform.forward).eulerAngles.x;

        if (!(X < 90 && X > 70 || X > 270 && X < 290))
        {
            movement += direction;

            transform.rotation = Quaternion.LookRotation(movement.normalized);
        }
        controller.Move(movement * speed * Time.deltaTime);

        //clamp y position
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, 0.0f, 120.0f);
        transform.position = position;
    }

    private void CheckInputs()
    {
        
        input.x = state.ThumbSticks.Left.X;
        input.y = -state.ThumbSticks.Left.Y;
    }

    public void GetBoost(float seconds)
    {
        canBoost = true;
        boostTime = seconds;
    }
}

