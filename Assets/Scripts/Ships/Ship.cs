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
using System.Collections.Generic;

public class Ship : MonoBehaviour, IPooledObject, IBulletHittable {

    /// <summary>
    /// The main GameObject pool manager. Used to retrieve bullets, and recycle Ships.
    /// </summary>
    public GameObjectPoolManager poolManager = null;

    /// <summary>
    /// The number of health points for the ship when spawned.
    /// </summary>
    public float initialHealthPoints = 100.0f;

    /// <summary>
    /// The ship's health
    /// </summary>
    protected float healthPoints = 100.0f;

    /// <summary>
    /// The layer mask for our bullets. This indicates to what layers our bullets will affect.
    /// </summary>
    public LayerMask bulletsLayerMask = 0;

    /// <summary>
    /// The bullet prefab name to retrieve from the main GameObjectPoolManager when shooting
    /// </summary>
    public string bulletPrefabName = "";

    /// <summary>
    /// The damage to set for the shot bullets.
    /// </summary>
    public float bulletDamage = 20.0f;

    /// <summary>
    /// A list of transform objects indicating from where lasers are shot.
    /// </summary>
    public List<Transform> laserPositions = new List<Transform>();

    /// <summary>
    /// The offset to add to the direction when shooting.
    /// </summary>
    public float bulletDirectionOffset = 270.0f;

    /// <summary>
    /// The bullet speed.
    /// </summary>
    public float bulletSpeed = 10.0f;

    /// <summary>
    /// Shoots bullets from the laser positions.
    /// The bullet direction is determined by the GameObject rotation and the bulletOffset
    /// </summary>
    protected void Shoot()
    {
        foreach (Transform laserPosition in laserPositions)
        {
            GameObject bulletGameObject = poolManager.SpawnPrefabNamed(bulletPrefabName);
            bulletGameObject.transform.position = laserPosition.position;
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.damage = bulletDamage;
                bullet.direction = transform.rotation.eulerAngles.z + bulletDirectionOffset;
                bullet.speed = bulletSpeed;
                bullet.acceleration = 0.0f;
                bullet.collisionLayerMask = bulletsLayerMask;
                bullet.poolManager = poolManager;
            }
        }
    }

    protected virtual void Start ()
    {
	
	}

    protected virtual void Update ()
    {
	
	}

    public virtual void OnSpawn()
    {
        healthPoints = initialHealthPoints;
    }

    public virtual void OnDespawn()
    {
        poolManager = null;
    }

    public virtual void BulletDidHit(Bullet bullet)
    {
        healthPoints -= bullet.damage;
        if (healthPoints <= 0.0f)
        {
            poolManager.RecycleGameObject(gameObject.name, gameObject);
        }
    }

}
