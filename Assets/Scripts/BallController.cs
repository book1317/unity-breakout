using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private LevelController theLevel;
    private PaddleController thePaddle;
    public Rigidbody2D myRigidbody;
    private Vector3 startPosition;
    public float speed = 5.0f;
    private string currentDirection;
    void Start()
    {
        theLevel = FindObjectOfType<LevelController>();
        thePaddle = FindObjectOfType<PaddleController>();
        myRigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (theLevel.gameState == LevelController.GameState.MainMenu)
        {
            transform.position = new Vector3(thePaddle.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    public void Bound(string direction)
    {
        switch (direction)
        {
            case "LeftUp":
                myRigidbody.velocity = new Vector2(-speed, speed);
                break;
            case "LeftDown":
                myRigidbody.velocity = new Vector2(-speed, -speed);
                break;
            case "RightUp":
                myRigidbody.velocity = new Vector2(speed, speed);
                break;
            case "RightDown":
                myRigidbody.velocity = new Vector2(speed, -speed);
                break;
        }
        currentDirection = direction;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        myRigidbody.velocity = new Vector2(0, 0);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (currentDirection.Equals("LeftUp") || currentDirection.Equals("LeftDown"))
        {
            if (currentDirection.Equals("LeftUp"))
            {
                if (other.contacts[0].point.y > transform.position.y)
                    Bound("LeftDown");
                else
                    Bound("RightUp");
            }
            else
            {
                if (other.contacts[0].point.y < transform.position.y)
                    Bound("LeftUp");
                else
                    Bound("RightDown");
            }
        }
        else if (currentDirection.Equals("RightUp") || currentDirection.Equals("RightDown"))
        {
            if (currentDirection.Equals("RightUp"))
            {
                if (other.contacts[0].point.y > transform.position.y)
                    Bound("RightDown");
                else
                    Bound("LeftUp");
            }
            else
            {
                if (other.contacts[0].point.y < transform.position.y)
                    Bound("RightUp");
                else
                    Bound("LeftDown");
            }
        }

        if (other.transform.CompareTag("Brick"))
        {
            other.transform.GetComponent<BrickController>().RemoveBrick();
        }
        else if (other.transform.CompareTag("KillPlane"))
        {
            theLevel.GameOver();
        }
    }
}
