using System.IO;
using UnityEngine;

namespace SaveSystem.Serializers
{
	/// <summary>
	/// 	Used to serialize the save game as json.
	/// </summary>
	public class JSONSerializer
	{
		#region Public methods

		/// <summary>
		/// 	write the save game as .json to disk.
		/// </summary>
		/// <param name="path">Path where the save game is being stored.</param>
		/// <param name="saveData">Actual save data</param>
		public void Serialize(string path, SaveData saveData)
		{
			string serializedJSON = JsonUtility.ToJson(saveData);
			File.WriteAllText(path, serializedJSON);
		}


		/// <summary>
		/// 	Reads save data from disk, must be in .json format.
		/// </summary>
		/// <param name="path">Path where the save game is being stored.</param>
		/// <returns>The loaded save game.</returns>
		public SaveData Deserialize(string path)
		{
			string serializedJSON = File.ReadAllText(path);
			return JsonUtility.FromJson<SaveData>(serializedJSON);
		}

		#endregion
	}
}