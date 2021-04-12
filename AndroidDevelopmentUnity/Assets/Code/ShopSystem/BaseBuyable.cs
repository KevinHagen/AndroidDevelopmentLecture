using CurrencySystem.Model;
using UnityEngine;

namespace ShopSystem
{
	/// <summary>
	/// 	Base Class for buyable items. Can be used to display items in the shop.
	/// </summary>
	public abstract class BaseBuyable : ScriptableObject
	{
		#region Serialize Fields

		[SerializeField] protected Sprite _itemIcon;
		[SerializeField] private CurrencyType _currencyType;
		[SerializeField] private int _cost;

		#endregion

		#region Properties

		public int Cost => _cost;
		public CurrencyType CurrencyType => _currencyType;
		public Sprite ItemIcon => _itemIcon;
		public bool Bought { get; set; }
		public bool Selected { get; set; }

		#endregion

		#region Public methods

		/// <summary>
		/// 	Called when the player selects the buyable in the shop.
		/// </summary>
		public virtual void Select()
		{
			Selected = true;
		}

		/// <summary>
		/// 	Called when the player deselects this buyable - either explicitly or implicitly (by selecting another one)
		/// </summary>
		/// <param name="activeSelection">If true, the deselection was explicit</param>
		public virtual void Deselect(bool activeSelection)
		{
			Selected = false;
		}

		#endregion
	}
}