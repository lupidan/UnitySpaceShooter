using UnityEngine;
using System.Collections;

/// <summary>
/// A Bullet class
/// </summary>
public class Bullet : MonoBehaviour, IPooledObject {

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
        if (transform.position.y > 20.0f)
        {
            poolManager.RecycleGameObject(gameObject.name, gameObject);
        }
    }

    public void OnSpawn()
    {
        linearSpeed = Vector3.zero;
    }

    public void OnDespawn()
    {
    }
}
