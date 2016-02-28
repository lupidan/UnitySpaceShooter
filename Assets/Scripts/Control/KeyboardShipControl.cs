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
/// The KeyboardShipControl class implements a ship controller that is controlled using the keyboard.
/// </summary>
public class KeyboardShipControl : IShipControl
{
    /// <summary>
    /// The speed factor for the normalized movement ([-1.0f - 1.0f])
    /// </summary>
    public float speedFactor = 5.0f;
     
    /// <summary>
    /// Creates a KeyboardShipControl object
    /// </summary>
    /// <param name="speedFactor">The speed factor for the normalized movement. Default value is 5.0f.</param>
    public KeyboardShipControl(float speedFactor = 5.0f) : base()
    {
        this.speedFactor = speedFactor;
    }

    public bool ShootButtonPressed
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    public Vector3 UpdatePosition(Vector3 originalPosition, float deltaTime)
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector3 newPosition = originalPosition + (inputVector * speedFactor * deltaTime);
        return newPosition;
    }
}
