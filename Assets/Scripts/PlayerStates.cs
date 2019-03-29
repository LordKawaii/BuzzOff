using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour {
    public bool isFlying = false;
    public bool isHit = false;
    public bool isInvulnerable = false;
    public const float INVULERABLE_TIME = .5f;

    float invTimeLeft = INVULERABLE_TIME;

    GameController gameCon;

    void Start()
    {
        gameCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (!gameCon.gameOver && gameCon.readyToPlay)
        { 
            if (isHit)
            {
                isInvulnerable = true;
            }
            invulerableTimer();
        }
    }

    void invulerableTimer()
    {
        if (invTimeLeft >= 0 && isInvulnerable)
        {
            invTimeLeft -= Time.deltaTime;
        }
        else
        { 
            isInvulnerable = false;
            invTimeLeft = INVULERABLE_TIME;
        }
    }
}
