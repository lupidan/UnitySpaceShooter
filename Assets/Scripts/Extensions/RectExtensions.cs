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

public static class RectExtensions
{
    /// <summary>
    /// This method clamps a Vector3 X and Y coordinates inside the receiver Rect. Z position is not modified.
    /// </summary>
    /// <param name="rect">The receiver Rect.</param>
    /// <param name="position">The position to clamp inside the Rect.</param>
    /// <returns>The clamped Vector3 position.</returns>
    public static Vector3 ClampPosition(this Rect rect, Vector3 position)
    {
        Vector3 fixedPosition = position;
        fixedPosition.x = Mathf.Clamp(fixedPosition.x, rect.xMin, rect.xMax);
        fixedPosition.y = Mathf.Clamp(fixedPosition.y, rect.yMin, rect.yMax);
        return fixedPosition;
    }

    /// <summary>
    /// This method clamps a Vector2 X and Y coordinates inside the receiver Rect.
    /// </summary>
    /// <param name="rect">The receiver Rect.</param>
    /// <param name="position">The position to clamp inside the Rect.</param>
    /// <returns>The clamped Vector2 position.</returns>
    public static Vector2 ClampPosition(this Rect rect, Vector2 position)
    {
        Vector2 fixedPosition = position;
        fixedPosition.x = Mathf.Clamp(fixedPosition.x, rect.xMin, rect.xMax);
        fixedPosition.y = Mathf.Clamp(fixedPosition.y, rect.yMin, rect.yMax);
        return fixedPosition;
    }

    /// <summary>
    /// Use Guizmos class to draw the Rect on the world. To use in OnDrawGizmos and OnDrawGizmosSelected
    /// </summary>
    /// <param name="rect">The receiver Rect to draw.</param>
    public static void DrawGizmo(this Rect rect)
    {
        Gizmos.DrawLine(new Vector3(rect.xMin, rect.yMin, 0.0f), new Vector3(rect.xMax, rect.yMin, 0.0f));
        Gizmos.DrawLine(new Vector3(rect.xMax, rect.yMin, 0.0f), new Vector3(rect.xMax, rect.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(rect.xMax, rect.yMax, 0.0f), new Vector3(rect.xMin, rect.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(rect.xMin, rect.yMax, 0.0f), new Vector3(rect.xMin, rect.yMin, 0.0f));
    }
}


