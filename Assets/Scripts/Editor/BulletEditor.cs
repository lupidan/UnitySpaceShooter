using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Bullet))]
public class BulletEditor : Editor {

    [DrawGizmo(GizmoType.Selected)]
    static void DrawBulletArea(Bullet bullet, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;
        bullet.activeArea.DrawGizmo();
    }

}

