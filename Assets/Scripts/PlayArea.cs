using UnityEngine;
using System.Collections;

public class PlayArea : MonoBehaviour {

    /// <summary>
    /// The rectangular area the player is limited to play in
    /// </summary>
    public Rect playableWorldArea;

    /// <summary>
    /// Clamps a Vector 3 position X and Y coordinates inside the defined rectangle
    /// </summary>
    /// <param name="position">The position to clamp inside the rectangle</param>
    /// <returns></returns>
    private Vector3 ClampPosition(Vector3 position)
    {
        return playableWorldArea.ClampPosition(position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(playableWorldArea.xMin, playableWorldArea.yMin, 0.0f), new Vector3(playableWorldArea.xMax, playableWorldArea.yMin, 0.0f));
        Gizmos.DrawLine(new Vector3(playableWorldArea.xMax, playableWorldArea.yMin, 0.0f), new Vector3(playableWorldArea.xMax, playableWorldArea.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(playableWorldArea.xMax, playableWorldArea.yMax, 0.0f), new Vector3(playableWorldArea.xMin, playableWorldArea.yMax, 0.0f));
        Gizmos.DrawLine(new Vector3(playableWorldArea.xMin, playableWorldArea.yMax, 0.0f), new Vector3(playableWorldArea.xMin, playableWorldArea.yMin, 0.0f));
    }

}
