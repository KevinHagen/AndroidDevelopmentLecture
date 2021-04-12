using UnityEditor;
using UnityEngine;

namespace ObstacleSystem.Editor
{
	/// <summary>
	/// 	Custom Inspector for the <see cref="ObstacleSpawner"/>.
	/// </summary>
	[CustomEditor(typeof(ObstacleSpawner))]
	public class ObstacleSpawnerInspector : UnityEditor.Editor
	{
		#region Public methods

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			// allows us to draw a button in the inspector and invoke a method upon pressing it.
			if (GUILayout.Button("Test Spawn"))
			{
				(target as ObstacleSpawner).Regenerate();
			}
		}

		#endregion
	}
}