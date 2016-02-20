///
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
using System.Collections;

public class Ship : MonoBehaviour, IBulletHittable, IPooledObject {

    /// <summary>
    /// The rectangular area the ship is limited to play in.
    /// </summary>
    public Rect gameArea = new Rect();

    /// <summary>
    /// A Transform object indicating from where to shoot the bullets.
    /// </summary>
    public Transform laserPosition = null;

    /// <summary>
    /// The main GameObject pool manager from where to retrieve bullets.
    /// </summary>
    public GameObjectPoolManager poolManager = null;

    /// <summary>
    /// The ship's health
    /// </summary>
    public float health = 100.0f;


    private bool _invincible = false;
    /// <summary>
    /// Whether the ship is invincible or not.
    /// </summary>
    public bool Invincible
    {
        get
        {
            return _invincible;
        }
        set
        {
            _invincible = value;
            gameObject.layer = LayerMask.NameToLayer(_invincible ? "PlayerInvincible" : "Player");
            UpdateShipAlpha();
        }
    }

	void Start ()
    {
	}

    void Update()
    {
        if (GamePlayerPrefs.IsMouseControlEnabled)
        {
            UpdateMouseControls();
        }
        else
        {
            UpdateKeyboardControls();
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(gameArea.xMin, gameArea.yMin, 0.0f), new Vector3(gameArea.xMax, gameArea.yMin, 0.0f));
        Gizmos.DrawLine(new Vector3(gameArea.xMax, gameArea.yMin, 0.0f), new Vector3(gameArea.xMax, gameArea.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(gameArea.xMax, gameArea.yMax, 0.0f), new Vector3(gameArea.xMin, gameArea.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(gameArea.xMin, gameArea.yMax, 0.0f), new Vector3(gameArea.xMin, gameArea.yMin, 0.0f));
    }

    /// <summary>
    /// This method updates the ship's position checking the Keyboard input.
    /// </summary>
    private void UpdateKeyboardControls()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector3 newPosition = transform.position + (inputVector * 5.0f * Time.deltaTime);
        this.transform.position = gameArea.ClampPosition(newPosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    /// <summary>
    /// This method updates the ship's position checking the Mouse inputs.
    /// </summary>
    private void UpdateMouseControls()
    {
        Vector3 positionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionVector.z = this.transform.position.z;
        this.transform.position = gameArea.ClampPosition(positionVector);

        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    /// <summary>
    /// Shoots a bullet from the laser position.
    /// </summary>
    private void ShootBullet()
    {
        GameObject bulletGameObject = poolManager.SpawnPrefabNamed("LaserBulletGreen");
        bulletGameObject.transform.position = (laserPosition != null) ? laserPosition.position : transform.position;
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.linearSpeed = new Vector3(0.0f, 10.0f, 0.0f);
            bullet.poolManager = poolManager;
        }
    }

    /// <summary>
    /// Updates ship alpha depending on the Invincible status.
    /// </summary>
    private void UpdateShipAlpha()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = Invincible ? 0.5f : 1.0f;
        spriteRenderer.color = color;
    }

    private void SetVulnerable()
    {
        Invincible = false;
    }

    public void BulletDidHit(Bullet bullet)
    {
        health -= bullet.damage;
        if (health < 0.0)
        {
            Destroy(gameObject);
        }
        else
        {
            Invincible = true;
            Invoke("SetVulnerable", 1.0f);
        }
    }

    public void OnSpawn()
    {
        health = 100.0f;
    }

    public void OnDespawn()
    {

    }
}
