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
