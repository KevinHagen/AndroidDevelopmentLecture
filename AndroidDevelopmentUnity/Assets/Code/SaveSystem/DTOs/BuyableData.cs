using System;

namespace SaveSystem.DTOs
{
	/// <summary>
	/// 	Data object for the a concrete buyable. Uses its name as a unique key and stores its state.
	/// </summary>
	[Serializable]
	public class BuyableData
	{
		#region Public Fields

		public bool Bought;
		public string Name;
		public bool Selected;

		#endregion

		#region Constructors

		public BuyableData(string baseBuyableName, bool baseBuyableBought, bool baseBuyableSelected)
		{
			Name = baseBuyableName;
			Bought = baseBuyableBought;
			Selected = baseBuyableSelected;
		}

		#endregion
	}
}