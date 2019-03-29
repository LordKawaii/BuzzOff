using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackerAttack : MonoBehaviour {
    public GameObject player;
    public GameObject attackAnimation;
    public GameController gameCon;
    public Transform hitDetector;
    public float attackSpeed = 100;
    public const float ATTACK_RANGE = .5f;

    public AudioClip missSound;
    public List<AudioClip> smashSounds;

    AudioSource audSource;

    public bool isAttacking = false;

    Animator animationController;
    SpriteRenderer attackRend;

    SpriteRenderer spriteRend;

    PlayerStates playerStates;



	// Use this for initialization
	void Start () {
        spriteRend = GetComponent<SpriteRenderer>();
        
        animationController = attackAnimation.GetComponent<Animator>();
        attackRend = attackAnimation.GetComponent<SpriteRenderer>();
        animationController.speed = attackSpeed;
        audSource = GetComponent<AudioSource>();

        playerStates = player.GetComponent<PlayerStates>();
        gameCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameCon.gameOver && gameCon.readyToPlay)
        { 
            if (CheckToAttack())
            {
                Attack();
            }
            else if (animationController.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                attackRend.enabled = false;
                animationController.ResetTrigger("isAttacking");
                isAttacking = false;
            }

            CheckIfHitBreakable();
        }
	}


    bool CheckToAttack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= ATTACK_RANGE)
        {
            return true;
        }

        return false;
    }

    void Attack()
    {
        isAttacking = true;
        attackRend.enabled = true;
        animationController.SetTrigger("isAttacking");

        

        if (Vector3.Distance(hitDetector.position, player.transform.position) <= ATTACK_RANGE && animationController.GetCurrentAnimatorStateInfo(0).IsName("Attack Ani"))
        {

            if (!playerStates.isInvulnerable)
            {
                playerStates.isHit = true;
            }
        }
    }

    void CheckIfHitBreakable()
    {
        if (animationController.GetCurrentAnimatorStateInfo(0).IsName("Attack Ani") && isAttacking)
        {
            List<GameObject> brokenItems = new List<GameObject>();
            foreach (GameObject breakable in gameCon.breakableObjs)
            {
                BreakableItemController breakControler = breakable.GetComponent<BreakableItemController>();
                if (Vector3.Distance(hitDetector.position, breakable.transform.position) <= ATTACK_RANGE && !breakControler.isBroken)
                {
                    breakControler.isBroken = true;
                    //gameCon.breakableObjs.Remove(breakable);
                    brokenItems.Add(breakable);
                        
                    int rand = Random.Range(0, smashSounds.Count - 1);
                    audSource.clip = smashSounds[rand];
                    audSource.Play();

                }
            }
            foreach (GameObject obj in brokenItems)
            {
                Debug.Log("Num Items Broken: " + brokenItems.Count);
                gameCon.breakableObjs.Remove(obj);
            }
        }
    }

}
