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
/// The Bullet class defines a bullet being shot with a certain linear speed.
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
