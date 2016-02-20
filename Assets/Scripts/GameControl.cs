using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    private int _lives = 0;
    public int Lives
    {
        set
        {
            _lives = value;
        }
        get
        {
            return _lives;
        }
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

   
}
