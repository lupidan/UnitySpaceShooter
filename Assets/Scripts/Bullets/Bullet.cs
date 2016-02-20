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

/// <summary>
/// The IBulletHittable defines an interface for something that can be hittable by a bullet
/// </summary>
public interface IBulletHittable
{
    /// <summary>
    /// Method called when a Bullet did hit another IBulletHittable object.
    /// </summary>
    /// <param name="bullet">The bullet that hit the object.</param>
    void BulletDidHit(Bullet bullet);
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
    /// The direction of the bullet in degrees.
    /// </summary>
    public float direction = 270.0f;

    /// <summary>
    /// The speed of the bullet.
    /// </summary>
    public float speed = 10.0f;

    /// <summary>
    /// The acceleration of the bullet.
    /// </summary>
    public float acceleration = 0.0f;

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
        float directionInRadians = direction * Mathf.Deg2Rad;
        speed += acceleration * Time.deltaTime;
        Vector3 velocity = new Vector3(Mathf.Cos(directionInRadians) * speed, Mathf.Sin(directionInRadians) * speed);
        transform.position += velocity * Time.deltaTime;

        if (!activeArea.Contains(transform.position))
        {
            poolManager.RecycleGameObject(gameObject.name, gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        activeArea.DrawGizmo();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionLayerMask.ContainsLayerWithIndex(other.gameObject.layer))
        {
            IBulletHittable[] bulletHittables = other.gameObject.GetComponents<IBulletHittable>();
            foreach (IBulletHittable bulletHittable in bulletHittables)
            {
                bulletHittable.BulletDidHit(this);
            }
            poolManager.RecycleGameObject(gameObject.name, gameObject);
        }
    }

    public void OnSpawn()
    {
        direction = 270.0f;
        speed = 10.0f;
        acceleration = 0.0f;
        collisionLayerMask = 0;
    }

    public void OnDespawn()
    {
        collisionLayerMask = 0;
    }
}
