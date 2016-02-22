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
/// The IDamageable interface defines something that can be damaged.
/// </summary>
public interface IDamageable {

    /// <summary>
    /// Method called to apply damage.
    /// </summary>
    /// <param name="damage">The amount of applied damage.</param>
    void DidDamage(float damage);

}

/// <summary>
/// Damage inflictor represents a GameObject component that reacts to other objects when colliding and applies damage to them if they are damageable
/// </summary>
public class DamageInflictor: MonoBehaviour, IPooledObject
{

    /// <summary>
    /// The pool manager this game object should return once it has collided and applied damage.
    /// </summary>
    public GameObjectPoolManager poolManager;

    /// <summary>
    /// The amount of damage this game object can do.
    /// </summary>
    public float damage = 1.0f;

    /// <summary>
    /// The LayerMask for objects we can inflict damage to
    /// </summary>
    public LayerMask collisionLayerMask;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionLayerMask.ContainsLayerWithIndex(other.gameObject.layer))
        {
            IDamageable[] damageables = other.gameObject.GetComponents<IDamageable>();
            foreach (IDamageable damageable in damageables)
            {
                damageable.DidDamage(damage);
            }
            poolManager.RecycleGameObject(gameObject.name, gameObject);
        }
    }

    public void OnSpawn()
    {
        //Nothing here for now
    }

    public void OnDespawn()
    {
        poolManager = null;
    }
}
