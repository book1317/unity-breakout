using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public void RemoveBrick()
    {
        LevelController theLevel = FindObjectOfType<LevelController>();
        theLevel.enableBrick.Remove(this);
        theLevel.CheckWin();
        gameObject.SetActive(false);
    }
}
