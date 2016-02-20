using UnityEngine;
using System.Collections;

public class LaserBullet : Bullet {

    public float angleOffset = 90.0f;

	public override void Update ()
    {
        base.Update();

        float angle = Mathf.Atan2(linearSpeed.y, linearSpeed.x) * Mathf.Rad2Deg;
        angle += angleOffset;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
	}
}
