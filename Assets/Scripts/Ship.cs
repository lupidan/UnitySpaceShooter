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

public class Ship : MonoBehaviour {

    public GameObjectPoolManager poolManager = null;

	void Start ()
    {
	}

    // Update is called once per frame
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

    private void UpdateKeyboardControls()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector3 newPosition = transform.position + (inputVector * 5.0f * Time.deltaTime);
        this.transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    private void UpdateMouseControls()
    {
        Vector3 positionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionVector.z = this.transform.position.z;
        this.transform.position = positionVector;

        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        GameObject bulletGameObject = poolManager.SpawnPrefabNamed("LaserBulletBlue");
        Debug.Log(bulletGameObject.transform.position);
        bulletGameObject.transform.position = transform.position;
        Debug.Log(bulletGameObject.transform.position);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.linearSpeed = new Vector3(Random.Range(-3.0f,3.0f), 10.0f, 0.0f);
            bullet.poolManager = poolManager;
        }
    }

    
}
