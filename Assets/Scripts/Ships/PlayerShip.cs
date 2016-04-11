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

    /// <summary>
    /// The object responsible of controlling the ship position and shooting.
    /// </summary>
    private IShipControl shipControl;



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

    /// <summary>
    /// Sets the ship invincible for a specified number of seconds.
    /// </summary>
    /// <param name="numberOfSeconds">The number of seconds this ship should be invincible.</param>
    public void SetInvincibleForTime(float numberOfSeconds)
    {
        gameObject.layer = PlayerShip.InvincibleLayer;
        CancelInvoke("SetVulnerable");
        Invoke("SetVulnerable", numberOfSeconds);
        UpdateShipAlpha();
    }

    /// <summary>
    /// Sets the ship back to being vulnerable.
    /// </summary>
    private void SetVulnerable()
    {
        gameObject.layer = PlayerShip.NormalLayer;
        UpdateShipAlpha();
    }



    void Update()
    {
        if (Time.deltaTime > 0.0)
        {
            Vector3 newPosition = shipControl.UpdatePosition(transform.position, Time.deltaTime);
            transform.position = gameArea.ClampPosition(newPosition);

            if (shipControl.ShootButtonPressed)
            {
                Shoot();
            }
        }
    }



    public override void OnSpawn()
    {
        base.OnSpawn();

        Vector3 minPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        Vector3 maxPosition = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0.0f));
        Vector3 sizeVector = maxPosition - minPosition;
        gameArea = new Rect(minPosition.x, minPosition.y, sizeVector.x, sizeVector.y);

        if (GamePlayerPrefs.IsMouseControlEnabled)
        {
            this.shipControl = new MouseShipControl();
        }
        else
        {
            this.shipControl = new KeyboardShipControl();
        }
    }



    public override void InflictDamage(float damage)
    {
        base.InflictDamage(damage);
        if (HealthPoints > 0.0f)
        {
            SetInvincibleForTime(0.5f);
        }
        else
        {
            if (Toolbox.GameControl.Lives <= 0)
            {
                Toolbox.GameControl.FinishGame();
            }
            else
            {
                Toolbox.GameControl.Lives -= 1;
                Toolbox.GameControl.SpawnPlayerInTime(2.0f);
            }
        }
    }

}
