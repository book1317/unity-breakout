using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public enum GameState
    {
        MainMenu, Playing, Win, GameOver
    }
    public GameState gameState = GameState.MainMenu;
    public PaddleController thePaddle;
    public BallController theBall;
    public List<BrickController> allBrick;
    public List<BrickController> enableBrick;
    public GameObject winText;
    public GameObject gameOverText;

    void Start()
    {
        thePaddle = FindObjectOfType<PaddleController>();
        theBall = FindObjectOfType<BallController>();
        for (int i = 0; i < allBrick.Count; i++)
            enableBrick.Add(allBrick[i]);
    }

    public void Reset()
    {
        gameState = GameState.MainMenu;
        thePaddle.ResetPosition();
        theBall.ResetPosition();
        ResetAllBrick();
        winText.SetActive(false);
        gameOverText.SetActive(false);
    }

    public void CheckWin()
    {
        if (enableBrick.Count <= 0)
        {
            winText.SetActive(true);
            gameState = GameState.Win;
            theBall.myRigidbody.velocity = new Vector2(0, 0);
        }
    }

    void ResetAllBrick()
    {
        for (int i = 0; i < allBrick.Count; i++)
            allBrick[i].gameObject.SetActive(true);

        for (int i = 0; i < allBrick.Count; i++)
            enableBrick.Add(allBrick[i]);
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        gameState = GameState.GameOver;
        theBall.myRigidbody.velocity = new Vector2(0, 0);
    }
}
