using System;
using System.Collections.Generic;
using SaveSystem.Interfaces;

namespace SaveSystem.DTOs
{
	/// <summary>
	/// 	Data transfer object for the buyable data - stores which items have been bought.
	/// </summary>
	[Serializable]
	public class BuyableDto : ISaveData
	{
		#region Public Fields

		public List<BuyableData> BuyableData;

		#endregion

		#region ISaveData Members

		public void Save(ISaveData data)
		{
			BuyableData = (data as BuyableDto).BuyableData;
		}

		#endregion
	}
}