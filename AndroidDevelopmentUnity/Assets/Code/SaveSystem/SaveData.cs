using System;
using System.Collections.Generic;
using SaveSystem.DTOs;
using SaveSystem.Interfaces;
using UnityEngine;

namespace SaveSystem
{
	/// <summary>
	/// 	Class representing the actual save data of the game.
	/// </summary>
	[Serializable]
	public class SaveData
	{
		#region Serialize Fields

		[SerializeField] private ScoreDataDto _scoreData = new ScoreDataDto();
		[SerializeField] private CurrencyDto _currencyData = new CurrencyDto();
		[SerializeField] private BuyableDto _buyableData = new BuyableDto();

		#endregion

		#region Private Fields

		private Dictionary<string, ISaveData> _save = new Dictionary<string, ISaveData>();

		#endregion

		#region Constructors

		public SaveData()
		{
			Initialize();
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	Reinitialize the save game.
		/// </summary>
		public void Initialize()
		{
			_save = new Dictionary<string, ISaveData>
			        {
				        { SaveKeys.ScoreDataKey, _scoreData },
				        { SaveKeys.CurrencyDataKey, _currencyData },
				        { SaveKeys.BuyableDataKey, _buyableData },
			        };
		}

		/// <summary>
		/// 	Updates the data for the given key in the save game.
		/// </summary>
		/// <param name="key">Key to update</param>
		/// <param name="saveData">Save Data object</param>
		/// <typeparam name="T">Data type of updated save data</typeparam>
		public void Put<T>(string key, T saveData) where T : ISaveData
		{
			if (_save.ContainsKey(key))
			{
				_save[key].Save(saveData);
			}
			else
			{
				Debug.LogError("Trying to save data for a non-existing key!");
			}
		}

		/// <summary>
		/// 	Gets the save data for the given key in the save game.
		/// </summary>
		/// <param name="key">Key to update</param>
		/// <param name="saveData">Save Data object</param>
		/// <typeparam name="T">Data type of updated save data</typeparam>
		/// <returns>A <see cref="ISaveData"/> object containing the save data or a default if there is none.</returns>
		public ISaveData Get<T>(string key, T defaultValue) where T : ISaveData
		{
			if (_save.ContainsKey(key))
			{
				return _save[key];
			}

			Debug.LogWarning("Trying to load data for a non-existing key!");
			return defaultValue;
		}

		#endregion
	}
}