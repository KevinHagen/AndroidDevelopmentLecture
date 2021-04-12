using System;
using Core;
using CurrencySystem.Model;
using ObstacleSystem;
using ObstacleSystem.Models;
using SaveSystem;
using SaveSystem.DTOs;
using SaveSystem.Interfaces;
using ShopSystem;

namespace CurrencySystem
{
	/// <summary>
	/// 	Contains information on how many coins the player has of which <see cref="CurrencyType"/>.
	/// </summary>
	public class CoinPurse : Singleton<CoinPurse>, ISaveable, ILoadable
	{
		#region Static Stuff

		/// <summary>
		/// 	Event invoked when the coin amount changes during a run.
		/// </summary>
		public static event Action<int> UpdateCoinsDuringRun;
		/// <summary>
		/// 	Event invoked when the coin amount changes in general (e.g. spending coins)
		/// </summary>
		public static event Action<int, CurrencyType> OnCoinAmountChanged;

		#endregion

		#region Private Fields

		// todo split by currency type
		private int _totalCoins;
		private int _coinsCollectedThisRun;

		#endregion

		#region Properties

		public int CoinsCollectedThisRun => _coinsCollectedThisRun;
		public int TotalCoins => _totalCoins;

		#endregion

		#region Unity methods

		protected override void Awake()
		{
			base.Awake();

			_totalCoins = 0;
			_coinsCollectedThisRun = 0;

			DamageObstacle.OnObstacleHitGround += OnDamageObstacleHitGround;
			CoinObstacle.ObstacleHitPlayer += OnCoinObstacleHitPlayer;
		}

		private void Start()
		{
			// register to save manager
			SaveManager.RegisterSaveable(this);
			SaveManager.RegisterLoadable(this);
		}

		private void OnDestroy()
		{
			// unregister from save manager
			SaveManager.UnregisterSaveable(this);
			SaveManager.UnregisterLoadable(this);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	New coins received are added to the total.
		/// </summary>
		/// <param name="amount">Amount of coins received</param>
		public void AddCoinsToTotal(int amount)
		{
			_totalCoins += amount;
		}

		/// <summary>
		/// 	Player spent coins.
		/// </summary>
		/// <param name="amount">Amount of coins spent</param>
		/// <param name="type">Type of currency spent</param>
		public void SpendCoins(int amount, CurrencyType type)
		{
			_totalCoins -= amount;
			OnCoinAmountChanged?.Invoke(_totalCoins, type);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 	Called when the player collects a coin during the run
		/// </summary>
		/// <param name="obj"></param>
		private void OnCoinObstacleHitPlayer(CoinObstacle obj)
		{
			if (obj == null)
			{
				return;
			}

			// increase coins this run by value of the coin
			_coinsCollectedThisRun += obj.Value;
			UpdateCoinsDuringRun?.Invoke(_coinsCollectedThisRun);
		}

		/// <summary>
		/// 	Called when any hostile obstacle hits the ground
		/// </summary>
		/// <param name="obj"></param>
		private void OnDamageObstacleHitGround(DamageObstacle obj)
		{
			if (obj == null)
			{
				return;
			}

			// every hostile obstacle awards one coin
			_coinsCollectedThisRun++;
			UpdateCoinsDuringRun?.Invoke(_coinsCollectedThisRun);
		}

		#endregion

		#region ILoadable Members

		public void Load()
		{
			CurrencyDto dto = SaveManager.Get(SaveKeys.CurrencyDataKey, new CurrencyDto()) as CurrencyDto;
			if (dto == null)
			{
				return;
			}

			// set totalCoins to the save game value
			_totalCoins = dto.TotalCoins;
			OnCoinAmountChanged?.Invoke(_totalCoins, CurrencyType.Regular);
		}

		#endregion

		#region ISaveable Members

		public void Save()
		{
			// save current totalCoins to the dto and then store it in the save game
			CurrencyDto dto = new CurrencyDto();
			dto.TotalCoins = _totalCoins;

			SaveManager.Put(SaveKeys.CurrencyDataKey, dto);
		}

		#endregion
	}
}