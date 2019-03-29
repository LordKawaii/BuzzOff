using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour {
    public float speed;

    PlayerStates playerStates;
    bool canFly = true;
    Vector3 target;
    GameController gameCon;
    AudioSource audioSource;
    public AudioClip hitPlayerSound;
    public AudioClip flightSound;

	// Use this for initialization
	void Start () {
        playerStates = GetComponent<PlayerStates>();
        audioSource = GetComponent<AudioSource>();
        gameCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        if (!gameCon.gameOver && gameCon.readyToPlay)
        { 
            InputManager();
            SoundManager();
        }
    }


    void InputManager()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if(Physics.Raycast(ray, out hit, 1000))
             {
                 target = hit.point + Vector3.up;
                 target = new Vector3(target.x, transform.position.y, target.z);
                 Vector3 dir = (target - transform.position).normalized;
                 Ray wallCheckRay = new Ray(transform.position, dir);
                 if (Physics.Raycast(wallCheckRay, out hit, speed * Time.deltaTime))
                 {
                     if (hit.collider.tag == "Wall")
                         canFly = false;
                     else
                         canFly = true;
                 }
                 else
                     canFly = true;

                 if (canFly)
                 { 
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                 }
                 transform.LookAt(target);
                 playerStates.isFlying = true;

             }

        }

        if (Input.GetMouseButtonUp(0))
        {
            playerStates.isFlying = false;
        }
    }

    void SoundManager()
    {
        if (playerStates.isFlying && !audioSource.isPlaying)
        {
            audioSource.clip = flightSound;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (!playerStates.isFlying||(playerStates.isInvulnerable && audioSource.clip == flightSound))
            audioSource.Stop();

        if (playerStates.isInvulnerable && !audioSource.isPlaying)
        {
            audioSource.loop = false;
            audioSource.clip = hitPlayerSound;
            audioSource.Play();
        }
    }
}
