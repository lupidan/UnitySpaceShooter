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

/// <summary>
/// The IBulletHittable defines an interface for something that can be hittable by a bullet
/// </summary>
public interface IBulletHittable
{
    /// <summary>
    /// Methods called in the component when the GameObject collides with a bullet.
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    void ApplyBulletDamage(float damage);
}

/// <summary>
/// The Bullet class defines a bullet being shot with a certain linear speed.
/// </summary>
public class Bullet : MonoBehaviour, IPooledObject {

    /// <summary>
    /// The area in where this bullet can live. Getting outside this area will return the bullet to the pool.
    /// </summary>
    public Rect activeArea = new Rect();

    /// <summary>
    /// The amount of damage the bullet can do.
    /// </summary>
    public float damage = 1.0f;

    /// <summary>
    /// The linear speed of the bullet
    /// </summary>
    public Vector3 linearSpeed = Vector3.zero;

    /// <summary>
    /// The mask to check for collisions
    /// </summary>
    public LayerMask collisionLayerMask;

    /// <summary>
    /// The pool manager this game object should return once it's done
    /// </summary>
    public GameObjectPoolManager poolManager;

    public virtual void Update()
    {
        transform.position += linearSpeed * Time.deltaTime;
        if (!activeArea.Contains(transform.position))
        {
            poolManager.RecycleGameObject(gameObject.name, gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(activeArea.xMin, activeArea.yMin, 0.0f), new Vector3(activeArea.xMax, activeArea.yMin, 0.0f));
        Gizmos.DrawLine(new Vector3(activeArea.xMax, activeArea.yMin, 0.0f), new Vector3(activeArea.xMax, activeArea.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(activeArea.xMax, activeArea.yMax, 0.0f), new Vector3(activeArea.xMin, activeArea.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(activeArea.xMin, activeArea.yMax, 0.0f), new Vector3(activeArea.xMin, activeArea.yMin, 0.0f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((collisionLayerMask.value & other.gameObject.layer) != 0)
        {
            IBulletHittable[] bulletHittables = other.gameObject.GetComponents<IBulletHittable>();
            foreach (IBulletHittable bulletHittable in bulletHittables)
            {
                bulletHittable.ApplyBulletDamage(damage);
            }

            poolManager.RecycleGameObject(gameObject.name, gameObject);
        }
    }

    public void OnSpawn()
    {
        linearSpeed = Vector3.zero;
        collisionLayerMask = 0;
    }

    public void OnDespawn()
    {
        collisionLayerMask = 0;
    }
}
