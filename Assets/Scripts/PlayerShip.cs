﻿///
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

public class PlayerShip : Ship
{
    /// <summary>
    /// The name of the layer where the Player's Ship takes damage.
    /// </summary>
    private const string NormalLayerName = "Player";

    /// <summary>
    /// The name of the layer where the Player's Ship is invulnerable to enemy bullets.
    /// </summary>
    private const string InvincibleLayerName = "PlayerInvincible";

    /// <summary>
    /// (Read Only) The layer index where the Player's Ship takes damage.
    /// </summary>
    private static int NormalLayer
    {
        get
        {
            return LayerMask.NameToLayer(NormalLayerName);
        }
    }

    /// <summary>
    /// (Read Only) The layer index where the Player's Ship is invulnerable to enemy bullets.
    /// </summary>
    private static int InvincibleLayer
    {
        get
        {
            return LayerMask.NameToLayer(InvincibleLayerName);
        }
    }

    /// <summary>
    /// The rectangular area the ship is limited to play in.
    /// </summary>
    public Rect gameArea = new Rect();

    /// <summary>
    /// (Read only) Whether the ship is invincible or not.
    /// To set the ship invincibility, use the method 'SetInvincibleForTime'
    /// </summary>
    public bool Invincible
    {
        get
        {
            return gameObject.layer == PlayerShip.InvincibleLayer;
        }
    }

	protected override void Start ()
    {
        base.Start();
	}

    protected override void Update()
    {
        base.Update();
        if (GamePlayerPrefs.IsMouseControlEnabled)
        {
            UpdateMouseControls();
        }
        else
        {
            UpdateKeyboardControls();
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(gameArea.xMin, gameArea.yMin, 0.0f), new Vector3(gameArea.xMax, gameArea.yMin, 0.0f));
        Gizmos.DrawLine(new Vector3(gameArea.xMax, gameArea.yMin, 0.0f), new Vector3(gameArea.xMax, gameArea.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(gameArea.xMax, gameArea.yMax, 0.0f), new Vector3(gameArea.xMin, gameArea.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(gameArea.xMin, gameArea.yMax, 0.0f), new Vector3(gameArea.xMin, gameArea.yMin, 0.0f));
    }

    /// <summary>
    /// This method updates the ship's position checking the Keyboard input.
    /// </summary>
    protected void UpdateKeyboardControls()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector3 newPosition = transform.position + (inputVector * 5.0f * Time.deltaTime);
        this.transform.position = gameArea.ClampPosition(newPosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    /// <summary>
    /// This method updates the ship's position checking the Mouse inputs.
    /// </summary>
    protected void UpdateMouseControls()
    {
        Vector3 positionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionVector.z = this.transform.position.z;
        this.transform.position = gameArea.ClampPosition(positionVector);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Updates ship alpha depending on the Invincible status.
    /// </summary>
    protected void UpdateShipAlpha()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = Invincible ? 0.5f : 1.0f;
        spriteRenderer.color = color;
    }

    public void SetInvincibleForTime(float numberOfSeconds)
    {
        gameObject.layer = PlayerShip.InvincibleLayer;
        Invoke("SetVulnerable", numberOfSeconds);
        UpdateShipAlpha();
    }

    private void SetVulnerable()
    {
        gameObject.layer = PlayerShip.NormalLayer;
        UpdateShipAlpha();
    }

    public override void BulletDidHit(Bullet bullet)
    {
        base.BulletDidHit(bullet);
        if (healthPoints > 0.0f)
        {
            SetInvincibleForTime(1.0f);
        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnDespawn()
    {
        base.OnSpawn();
    }
}
