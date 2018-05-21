using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathMovement))]
[CanEditMultipleObjects]
public class PathMovementInspector : Editor
{

	private const float handleSize = 0.06f;
	private const float pickSize = 0.09f;

	private int m_selected = -1;

	private void OnSceneGUI()
	{
		PathMovement t = target as PathMovement;
		Transform handleTransform = t.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

		Handles.color = Color.green;
		
        for (int i=1; i < t.m_points.Length; ++i)
		{
			Vector3 a = handleTransform.TransformPoint(t.m_points[i-1]);
			Vector3 b = handleTransform.TransformPoint(t.m_points[i]);
			Handles.DrawLine(a, b);
        }

		if (t.m_way.mode == MovementMode.Loop && t.m_points.Length > 1)
		{
			Vector3 a = handleTransform.TransformPoint(t.m_points[0]);
			Vector3 b = handleTransform.TransformPoint(t.m_points[t.m_points.Length - 1]);
			Handles.color = Color.grey;
            Handles.DrawLine(a, b);
		}

		Handles.color = Color.red;
		foreach (PathMovementAnchor anchor in t.m_items)
		{
			if (anchor.m_target == null) { continue;  }
			Vector3 v = t.get_pos_on_path_local(anchor.m_offset);
			Handles.DrawLine(handleTransform.TransformPoint(v), anchor.m_target.transform.position);
		}

		for (int i = 0; i < t.m_points.Length; ++i)
		{
			Vector3 a = handleTransform.TransformPoint(t.m_points[i]);

			float size = HandleUtility.GetHandleSize(a);
			if (Handles.Button(a, handleRotation, size * handleSize, size * pickSize, Handles.DotCap))
			{
				m_selected = i;
            }
		}
		if (m_selected >= t.m_points.Length)
		{
			m_selected = -1;
		}
		if (m_selected != -1)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 a = handleTransform.TransformPoint(t.m_points[m_selected]);
			a = Handles.DoPositionHandle(a, handleRotation);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(t, "Move Point");
				EditorUtility.SetDirty(t);
				t.m_points[m_selected] = handleTransform.InverseTransformPoint(a);
			}
		}

		if (GUILayout.Button("Add Point"))
		{
			Undo.RecordObject(t, "Add Point");

			Vector3 point = t.m_points[t.m_points.Length - 1];
			System.Array.Resize(ref t.m_points, t.m_points.Length + 1);
			t.m_points[t.m_points.Length - 1] = point;

			EditorUtility.SetDirty(t);
		}
	}
}
