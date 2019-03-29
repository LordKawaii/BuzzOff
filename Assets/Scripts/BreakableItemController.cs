using UnityEngine;
using System.Collections;

public class BreakableItemController : MonoBehaviour {

    public bool isBroken = false;
    public int pointAmount = 0;
    public Sprite brokenSprite;
    public PlayerStates playerStates;

    SpriteRenderer spriteRend;
    bool hasGivenPoints = false;
    GameController gameCon;

    // Use this for initialization
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        gameCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerStates = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStates>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBroken && !gameCon.gameOver)
            BrakeObject();
    }

    void BrakeObject()
    {
        spriteRend.sprite = brokenSprite;
        if (!hasGivenPoints)
        {
            gameCon.score += pointAmount;
            hasGivenPoints = true;

            Debug.Log("Num Breakable left: " + gameCon.breakableObjs.Count);
        }
    }
    
}
