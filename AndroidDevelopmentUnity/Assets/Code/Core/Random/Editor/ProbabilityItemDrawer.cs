using System.Reflection;
using System.Text.RegularExpressions;
using ObstacleSystem;
using UnityEditor;
using UnityEngine;

namespace Core.Random.Editor
{
	/// <summary>
	/// 	Editor to render Probability items of type <see cref="BaseObstacle"/> to the inspector.
	/// </summary>
	[CustomPropertyDrawer(typeof(ProbabilityItem<BaseObstacle>))]
	public class ProbabilityItemDrawer : PropertyDrawer
	{
		#region Static Stuff

		private const float ColumnSpacing = 15f;

		#endregion

		#region Unity methods

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			// find all serialized properties - it starts at the "baseObstacle" and then goes relative from there. Has to match the naming of field sin ProbabilityItem<BaseObstacle> class. 
			SerializedProperty amountProperty = property.FindPropertyRelative("_amount");
			SerializedProperty itemProperty = property.FindPropertyRelative("_item");
			SerializedProperty percentageProperty = property.FindPropertyRelative("_probability");

			// poll current values
			int amount = amountProperty.intValue;
			float percentage = percentageProperty.floatValue;
			BaseObstacle obst = itemProperty.objectReferenceValue as BaseObstacle;

			// Draw all rects on the inspector to reserve space
			Rect percentageLabelRect = new Rect(position.x, position.y, 15f, position.height);
			Rect percentageRect = new Rect(percentageLabelRect.x + percentageLabelRect.width, position.y, 0.15f * EditorGUIUtility.currentViewWidth, position.height);
			Rect amountLabelRect = new Rect(percentageRect.x + percentageRect.width + ColumnSpacing, position.y, 50f, position.height);
			Rect amountRect = new Rect(amountLabelRect.x + amountLabelRect.width, position.y, 0.15f * EditorGUIUtility.currentViewWidth, position.height);
			Rect itemLabelRect = new Rect(amountRect.x + amountRect.width + ColumnSpacing, position.y, 50f, position.height);
			Rect itemRect = new Rect(itemLabelRect.x + itemLabelRect.width, position.y, 0.4f * EditorGUIUtility.currentViewWidth, position.height);

			// add a small % text so its more clear what unit we are referring to 
			EditorGUI.LabelField(percentageLabelRect, "%");
			// making a disabled group so the % field is read only and can not be set with the inspector
			EditorGUI.BeginDisabledGroup(true);
			percentageProperty.floatValue = EditorGUI.FloatField(percentageRect, percentage);
			EditorGUI.EndDisabledGroup();

			// draw amount, pretty much as if we were not using a custom inspector
			EditorGUI.LabelField(amountLabelRect, "Amount");
			amountProperty.intValue = EditorGUI.IntField(amountRect, amount);
			// same for the actual item - we do not want scene objects, just prefabs
			EditorGUI.LabelField(itemLabelRect, "Item");
			itemProperty.objectReferenceValue = EditorGUI.ObjectField(itemRect, obst, typeof(BaseObstacle), false);

			if (amount != amountProperty.intValue)
			{
				// property path is given like: ObstacleProbabilityItems._items.Array.data[0]
				// so we want to filter out the last part until we get the name of the field of type ProbabilityItemList
				Match match = Regex.Match(property.propertyPath, "(.*?)\\._items\\.Array");

				// retrieve field info of type ProbabilityItemList - field always has to be a private one. serializedObject.targetObject is the instance of the type containing the list
				FieldInfo field = property.serializedObject.targetObject.GetType().GetField(match.Groups[1].Value, BindingFlags.Instance | BindingFlags.NonPublic);
				ProbabilityItemList<BaseObstacle> oList = (ProbabilityItemList<BaseObstacle>) field.GetValue(property.serializedObject.targetObject);
				EditorApplication.delayCall += () => oList.UpdateProbabilities();
			}
		}

		#endregion
	}
}