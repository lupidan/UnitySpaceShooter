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

public class Ship : MonoBehaviour, IPooledObject, IDamageInflictable {

    /// <summary>
    /// Delegate for events related to a Ship
    /// </summary>
    /// <param name="ship">The ship components where the event happened.</param>
    public delegate void ShipEvent(Ship ship);



    /// <summary>
    /// Event when the ship's health changes
    /// </summary>
    public event ShipEvent OnHealthChange;



    /// <summary>
    /// The number of health points for the ship when spawned.
    /// </summary>
    public float initialHealthPoints = 100.0f;

    private float healthPoints = 100.0f;

    /// <summary>
    /// The ship's health
    /// </summary>
    public float HealthPoints
    {
        get
        {
            return healthPoints;
        }
        set
        {
            healthPoints = value;

            if (OnHealthChange != null)
            {
                OnHealthChange(this);
            }
        }
    }

    /// <summary>
    /// (Read only) returns the normalized health for this ship (from 0.0f to 1.0f)
    /// </summary>
    public float normalizedHealthPoints
    {
        get
        {
            if (initialHealthPoints > 0.0f)
            {
                return healthPoints / initialHealthPoints;
            }
            return 0;
        }
    }

    /// <summary>
    /// The layer mask for our bullets. This indicates to what layers our bullets will affect.
    /// </summary>
    public LayerMask bulletsLayerMask = 0;

    /// <summary>
    /// The bullet prefab to retrieve from the main GameObjectPoolManager when shooting
    /// </summary>
    public GameObject bulletPrefab = null;

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
            GameObject bulletGameObject = Toolbox.PoolManager.SpawnGameObject(bulletPrefab);
            bulletGameObject.transform.position = laserPosition.position;
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.direction = transform.rotation.eulerAngles.z + bulletDirectionOffset;
                bullet.speed = bulletSpeed;
                bullet.acceleration = 0.0f;
            }

            DamageInflictor damageInflictor = bulletGameObject.GetComponent<DamageInflictor>();
            if (damageInflictor != null)
            {
                damageInflictor.damage = bulletDamage;
                damageInflictor.collisionLayerMask = bulletsLayerMask;
            }
        }
    }

    public virtual void OnSpawn()
    {
        HealthPoints = initialHealthPoints;
    }

    public virtual void OnDespawn()
    {
        if (OnHealthChange != null)
        {
            OnHealthChange = null;
        }
    }

    public virtual void InflictDamage(float damage)
    {
        HealthPoints -= damage;
        if (HealthPoints <= 0.0f)
        {
            Toolbox.PoolManager.RecycleGameObject(gameObject);
        }
    }

}
