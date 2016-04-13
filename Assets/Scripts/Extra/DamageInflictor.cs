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
/// The IDamageable interface defines something that has the ability of being inflicted with damage
/// </summary>
public interface IDamageInflictable {

    /// <summary>
    /// Method called to inflict damage.
    /// </summary>
    /// <param name="damage">The amount of inflicted damage.</param>
    void InflictDamage(float damage);

}

/// <summary>
/// Damage inflictor represents a GameObject component that reacts to other objects when colliding and applies damage to them if they are damageable
/// </summary>
public class DamageInflictor: MonoBehaviour {

    /// <summary>
    /// The amount of damage this game object can do.
    /// </summary>
    public float damage = 1.0f;

    /// <summary>
    /// The LayerMask for objects we can inflict damage to
    /// </summary>
    public LayerMask collisionLayerMask;

    /// <summary>
    /// Whether this game object should be destroyed once the damage has been inflicted after a collision
    /// </summary>
    public bool shouldBeDestroyedOnDamageInflicted = true;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionLayerMask.ContainsLayerWithIndex(other.gameObject.layer))
        {
            IDamageInflictable[] damageInflictables = other.gameObject.GetComponents<IDamageInflictable>();
            foreach (IDamageInflictable damageInflictable in damageInflictables)
            {
                damageInflictable.InflictDamage(damage);
            }

            if (shouldBeDestroyedOnDamageInflicted)
            {
                Toolbox.PoolManager.RecycleGameObject(gameObject);
            }
        }
    }

}
