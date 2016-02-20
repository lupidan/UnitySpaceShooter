using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

    public GameObjectPoolManager poolManager = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.frameCount % 20 == 0)
        {
            GameObject bulletGameObject = poolManager.SpawnPrefabNamed("RoundBulletRed");
            bulletGameObject.transform.position = transform.position;
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.linearSpeed = new Vector3(0.0f, -10.0f, 0.0f);
                bullet.poolManager = poolManager;
            }
        }
	}
}
