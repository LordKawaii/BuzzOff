using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    PlayerMovment playerMove;
    SpriteRenderer spriteRend;
    PlayerStates playerStates;
    GameController gameCon;
    
    public Sprite atRestSprite;
    public Sprite movingSprite1;
    public Sprite movingSprite2;

	// Use this for initialization
	void Start () {
        playerMove = GetComponent<PlayerMovment>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerStates = transform.parent.GetComponent<PlayerStates>();
        gameCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameCon.gameOver && gameCon.readyToPlay)
        { 
            SetSptite();
        }
	}

    void SetSptite()
    {
        if (playerStates.isFlying)
        {
            if (!spriteRend.sprite == movingSprite1)
                spriteRend.sprite = movingSprite1;
            else
                spriteRend.sprite = movingSprite2;
        }
        else
            spriteRend.sprite = atRestSprite;
    }
}
