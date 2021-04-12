using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShopSystem.Model
{
	[CreateAssetMenu(fileName = "New Shop Category", menuName = "Dodgey/Shop/Category")]
	public class ShopCategory : ScriptableObject
	{
		#region Serialize Fields

		[SerializeField] private List<BaseBuyable> _itemsInCategory = new List<BaseBuyable>();

		#endregion

		#region Properties

		public List<BaseBuyable> ItemsInCategory => _itemsInCategory.OrderBy(b => b.CurrencyType).ThenBy(b => b.Cost).ToList();

		#endregion
	}
}