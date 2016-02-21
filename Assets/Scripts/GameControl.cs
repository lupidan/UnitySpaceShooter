﻿///
/// The MIT License(MIT)
/// 
/// Copyright(c) 2016 Daniel Lupiañez Casares
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
///

using UnityEngine;

public class GameControl : MonoBehaviour {

    /// <summary>
    /// The pool manager to use to Spawn GameObjects
    /// </summary>
    public GameObjectPoolManager poolManager = null;

    /// <summary>
    /// The initial position for the player.
    /// </summary>
    public Transform playerStartPosition = null;

   

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

    /// <summary>
    /// Spawns a player on the screen
    /// </summary>
    public void SpawnPlayer()
    {
        GameObject playerShipGameObject = poolManager.SpawnPrefabNamed("PlayerShip");
        playerShipGameObject.transform.position = playerStartPosition.position;
        PlayerShip playerShip = playerShipGameObject.GetComponent<PlayerShip>();
        if (playerShip != null)
        {
            playerShip.SetInvincibleForTime(1.0f);
            playerShip.poolManager = poolManager;
        }
    }

    void Start ()
    {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void StartGame()
    {
        SpawnPlayer();
    }

}
