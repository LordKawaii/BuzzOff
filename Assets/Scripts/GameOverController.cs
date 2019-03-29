﻿using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

    Animator ani;

	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("EndGame"))
        {
            Application.LoadLevel("Menu");
        }
	}
}
