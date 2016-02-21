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

public class EnemyShip : Ship {

    /// <summary>
    /// Face the player if there is one.
    /// </summary>
    public bool facePlayer = false;

    /// <summary>
    /// The number of points destroying the ship will give the player.
    /// </summary>
    public int scoreIncrease = 100;

    /// <summary>
    /// Offset to apply the angle to face the player.
    /// </summary>
    public float facePlayerAngleOffset = 90.0f;

    /// <summary>
    /// The interval in seconds in which to attempt and shoot. The ship will only shoot if it's inside the shooting area.
    /// </summary>
    public float shootRepeatingInterval = 1.0f;

    /// <summary>
    /// The velocity of the enemy ship.
    /// </summary>
    public Vector3 velocity = Vector3.zero;

    /// <summary>
    /// The acceleration of the enemy ship.
    /// </summary>
    public Vector3 acceleration = Vector3.zero;

    /// <summary>
    /// The area where the ship is live. When exiting the area, the ship is pooled.
    /// </summary>
    public Rect activeArea = new Rect();

    /// <summary>
    /// The area where the ship shoots.
    /// </summary>
    public Rect shootingArea = new Rect();

    /// <summary>
    /// Update the rotation of the GameObject to face the specified PlayerShip
    /// </summary>
    /// <param name="playerShip">The player ship this ship wants to face to.</param>
    protected void FaceToPlayerShip(PlayerShip playerShip)
    {
        Vector3 directionVector = playerShip.transform.position - transform.position;
        float facingDirection = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        facingDirection += facePlayerAngleOffset;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, facingDirection);
    }

    /// <summary>
    /// Shoots only if the ship is inside the shooting area.
    /// </summary>
    protected void ShootIfInside()
    {
        if (shootingArea.Contains(transform.position))
        {
            Shoot();
        }
    }

    protected override void Update()
    {
        base.Update();

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if (facePlayer)
        {
            PlayerShip playerShip = FindObjectOfType<PlayerShip>();
            if (playerShip != null)
            {
                FaceToPlayerShip(playerShip);
            }
        }

        if (!activeArea.Contains(transform.position))
        {
            poolManager.RecycleGameObject(gameObject.name, gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        activeArea.DrawGizmo();
        Gizmos.color = Color.magenta;
        shootingArea.DrawGizmo();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        InvokeRepeating("ShootIfInside", shootRepeatingInterval, shootRepeatingInterval);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        CancelInvoke("ShootIfInside");
    }

    public override void BulletDidHit(Bullet bullet)
    {
        base.BulletDidHit(bullet);

        if (healthPoints <= 0.0f)
        {
            GameControl gameControl = FindObjectOfType<GameControl>();
            if (gameControl != null)
            {
                gameControl.score += scoreIncrease;
            }
        }
    }
}
