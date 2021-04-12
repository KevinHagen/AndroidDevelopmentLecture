using System;
using System.Collections.Generic;
using System.IO;
using SaveSystem.Interfaces;
using SaveSystem.Serializers;
using UnityEngine;

namespace SaveSystem
{
	/// <summary>
	/// 	Manages save games.
	/// </summary>
	public static class SaveManager
	{
		#region Static Stuff

		private const string SaveFilePath = "save.json";
		private static JSONSerializer _serializer = new JSONSerializer();
		private static SaveData _saveData = new SaveData();
		private static List<ISaveable> _saveables = new List<ISaveable>();
		private static List<ILoadable> _loadables = new List<ILoadable>();
		private static string FullPath => Path.Combine(Application.persistentDataPath, SaveFilePath);
		public static bool HasLoadables => _loadables.Count > 0;

		/// <summary>
		/// 	Add a new saveable object to the savemanager for when the game is being saved.
		/// </summary>
		/// <param name="saveable">Saveable to add.</param>
		public static void RegisterSaveable(ISaveable saveable)
		{
			if (_saveables.Contains(saveable))
			{
				return;
			}

			_saveables.Add(saveable);
		}

		/// <summary>
		/// 	Removes a new saveable object to the savemanager for when the game is being saved.
		/// </summary>
		/// <param name="saveable">Saveable to remove.</param>
		public static void UnregisterSaveable(ISaveable saveable)
		{
			if (!_saveables.Contains(saveable))
			{
				return;
			}

			_saveables.Remove(saveable);
		}

		/// <summary>
		/// 	Add a new loadable object to the savemanager for when the game is being loaded.
		/// </summary>
		/// <param name="saveable">Loadable to add.</param>

		public static void RegisterLoadable(ILoadable loadable)
		{
			if (_loadables.Contains(loadable))
			{
				return;
			}

			_loadables.Add(loadable);
		}
		
		/// <summary>
		/// 	Removes a new loadable object to the savemanager for when the game is being loaded.
		/// </summary>
		/// <param name="saveable">Loadable to remove.</param>
		public static void UnregisterLoadable(ILoadable loadable)
		{
			if (!_loadables.Contains(loadable))
			{
				return;
			}

			_loadables.Remove(loadable);
		}

		/// <summary>
		/// 	Add save data for the given key to the save game.
		/// </summary>
		/// <param name="key">Key to store save data with</param>
		/// <param name="saveData">Save Data to store</param>
		/// <typeparam name="T">Type of the save data to store</typeparam>
		public static void Put<T>(string key, T saveData) where T : ISaveData
		{
			_saveData.Put(key, saveData);
		}

		/// <summary>
		/// 	Retrieve save data for the given key form the save game.
		/// </summary>
		/// <param name="key">Key to look for</param>
		/// <param name="defaultValue">Default in case there is no data to retrieve</param>
		/// <typeparam name="T">Type of the save data to store</typeparam>
		/// <returns>The loaded save data or a default value if there is none</returns>
		public static ISaveData Get<T>(string key, T defaultValue) where T : ISaveData
		{
			return _saveData.Get(key, defaultValue);
		}

		/// <summary>
		/// 	Saves the game, using all registered <see cref="ISaveable"/>.
		/// </summary>
		public static void Save()
		{
			foreach (ISaveable saveable in _saveables)
			{
				saveable.Save();
			}

			_serializer.Serialize(FullPath, _saveData);
		}

		/// <summary>
		/// 	Loads the game, using all registered <see cref="ILoadable"/>.
		/// </summary>
		public static void Load()
		{
			if (HasSaveData())
			{
				_saveData = _serializer.Deserialize(FullPath);
				_saveData.Initialize();

				foreach (ILoadable loadable in _loadables)
				{
					loadable.Load();
				}
			}

			OnLoadingDone?.Invoke();
		}

		/// <summary>
		/// 	Checks if save data exists.
		/// </summary>
		/// <returns>True if there is save data</returns>
		private static bool HasSaveData()
		{
			return File.Exists(FullPath);
		}

		/// <summary>
		/// 	Invoked when the game has finished loading.
		/// </summary>
		public static event Action OnLoadingDone;

		#endregion
	}
}