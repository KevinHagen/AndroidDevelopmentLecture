using ItemSystem;
using ItemSystem.View;
using PlayerSystem;
using UnityEngine;

namespace ShopSystem
{
	/// <summary>
	/// 	Implementation for the item buyable - represents the shop data for items.
	/// </summary>
	[CreateAssetMenu(fileName = "New Item", menuName = "Dodgey/Shop/Item")]
	public class ItemBuyable : BaseBuyable
	{
		#region Serialize Fields
		
		[SerializeField] private Item _itemPrefab;

		#endregion

		#region Public methods

		public override void Select()
		{
			base.Select();

			// find the item ui and assign current selected item
			ItemUI ui = FindObjectOfType<ItemUI>(true);
			ui.SetItemImage(_itemIcon);

			// give the player the item
			PlayerController controller = FindObjectOfType<PlayerController>(true);
			controller.SetItem(_itemPrefab);
		}

		public override void Deselect(bool activeSelection)
		{
			base.Deselect(activeSelection);
			if (!activeSelection)
			{
				return;
			}

			// explicit deselection - no more item, remove ui
			ItemUI ui = FindObjectOfType<ItemUI>(true);
			ui.SetItemImage(null);
			ui.TurnOff();

			// remove item from plyer ivnentory
			PlayerController controller = FindObjectOfType<PlayerController>(true);
			controller.SetItem(null);
		}

		#endregion
	}
}