using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private LevelController theLevel;
    public float speed = 1.0f;
    private string prevDirecton = "RightUp";
    private Vector3 startPosition;

    void Start()
    {
        theLevel = FindObjectOfType<LevelController>();
        myRigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (theLevel.gameState == LevelController.GameState.Playing)
            {
                if (Input.GetButton("Left"))
                {
                    myRigidbody.velocity = new Vector2(-speed, 0);
                }
                else if (Input.GetButton("Right"))
                {
                    myRigidbody.velocity = new Vector2(speed, 0);
                }
            }
            else if (theLevel.gameState == LevelController.GameState.MainMenu)
            {
                if (Input.GetButton("Left"))
                {
                    myRigidbody.velocity = new Vector2(-speed, 0);
                    prevDirecton = "LeftUp";

                }
                else if (Input.GetButton("Right"))
                {
                    myRigidbody.velocity = new Vector2(speed, 0);
                    prevDirecton = "RightUp";
                }
                if (Input.GetButtonDown("Shoot"))
                {
                    theLevel.gameState = LevelController.GameState.Playing;
                    FindObjectOfType<BallController>().Bound(prevDirecton);
                }
            }
            else if (Input.GetButtonDown("Shoot") && (theLevel.gameState == LevelController.GameState.Win || theLevel.gameState == LevelController.GameState.GameOver))
            {
                theLevel.gameState = LevelController.GameState.MainMenu;
                theLevel.Reset();
            }
        }
        else
        {
            myRigidbody.velocity = new Vector2(0, 0);
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

}
