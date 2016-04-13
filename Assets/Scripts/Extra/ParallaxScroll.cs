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
/// The ParallaxScroll component is responsible of scrolling a texture at a specific speed.
/// </summary>
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
    /// The speed multiplier for the materialSpeedPerSecond. Default value is 1.0f.
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
