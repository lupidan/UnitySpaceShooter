using UnityEngine;
using System.Collections;

public class ParallaxScroll : MonoBehaviour {

    /// <summary>
    /// Private texture renderer. Used to move the offset of the texture.
    /// </summary>
    private Renderer textureRenderer = null;

    /// <summary>
    /// The speed in which to increase the texture offset by second. Default value is Vector2.zero.
    /// </summary>
    public Vector2 materialSpeedPerSecond = Vector2.zero;

    /// <summary>
    /// The speed multiplier for the materialSpeedPerSecond. Default value is 1.0f
    /// </summary>
    public float speedMultiplier = 1.0f;

	void Start ()
    {
        this.textureRenderer = GetComponent<Renderer>();
	}
	
	void Update ()
    {
        textureRenderer.material.mainTextureOffset += materialSpeedPerSecond * Time.deltaTime * speedMultiplier;
	}

}
