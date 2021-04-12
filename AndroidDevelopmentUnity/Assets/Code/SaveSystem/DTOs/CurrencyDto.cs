using System;
using SaveSystem.Interfaces;

namespace SaveSystem.DTOs
{
	/// <summary>
	/// 	Data transfer object for the currency data - stores amount of coins.
	/// </summary>
	[Serializable]
	public class CurrencyDto : ISaveData
	{
		#region Public Fields

		public int TotalCoins;

		#endregion

		#region ISaveData Members

		public void Save(ISaveData data)
		{
			TotalCoins = (data as CurrencyDto).TotalCoins;
		}

		#endregion
	}
}