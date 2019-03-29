using UnityEngine;
using System.Collections;

public class AttackerMovement : MonoBehaviour {

    public Transform player;
    public float speed;
    public AttackerAttack attckerAtk;

    bool canMove = true;
    GameController gameCon;
	Rigidbody parentRB;
	// Use this for initialization
	void Start () {
		parentRB = transform.parent.GetComponent<Rigidbody>();
        attckerAtk = GetComponent<AttackerAttack>();
        gameCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        speed = speed * gameCon.GetEnemySpeed();

    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (!gameCon.gameOver && gameCon.readyToPlay)
        { 
            FollowPlayer();
        }
	}

    void FollowPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        Debug.DrawRay(transform.position, dir);
        Ray wallCheckRay = new Ray(transform.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(wallCheckRay, out hit, speed * Time.deltaTime))
        {
			if (hit.collider.tag == "Wall" || hit.collider.tag == "AttackerWall")
			{
                if (Vector2.Distance(transform.position, hit.transform.position) < 1.5f)
				    canMove = false;
			}
			else
			{
				canMove = true;
				parentRB.constraints = RigidbodyConstraints.None;
            }
        }
        else
            canMove = true;

        if (Vector3.Distance(player.position, transform.position) >= .5f && !attckerAtk.isAttacking)
        {
            transform.parent.LookAt(player.position);
            if (canMove)
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, player.position, speed * Time.deltaTime);
        }

    }
}
