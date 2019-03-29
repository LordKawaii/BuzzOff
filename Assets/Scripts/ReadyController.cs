using UnityEngine;
using System.Collections;

public class ReadyController : MonoBehaviour {
    Animator readyAni;
    GameController GameCon;

	// Use this for initialization
	void Start () {
        readyAni = GetComponent<Animator>();
        GameCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (readyAni.GetCurrentAnimatorStateInfo(0).IsName("end"))
        {
            GameCon.readyToPlay = true;
            Destroy(gameObject);
        }
	
	}
}
