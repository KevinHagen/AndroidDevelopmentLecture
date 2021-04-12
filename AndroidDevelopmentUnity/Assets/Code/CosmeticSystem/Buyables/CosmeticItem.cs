using ShopSystem;
using UnityEngine;

namespace CosmeticSystem.Buyables
{
	/// <summary>
	/// 	A cosmetic item for the player - changes its appearance by adding purely cosmetic meshes.
	/// 	Scriptable Object Data representation. 
	/// </summary>
	[CreateAssetMenu(fileName = "New Cosmetic Item", menuName = "Dodgey/Shop/Cosmetics/Item", order = 0)]
	public class CosmeticItem : BaseBuyable
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("Which slot to occupy using this item")]
		private CosmeticSlot _occupiedSlot = CosmeticSlot.Hat;
		[SerializeField] [Tooltip("Visual prefab of the cosmetic object.")]
		private GameObject _cosmeticPrefab;

		#endregion

		#region Properties

		public GameObject CosmeticPrefab => _cosmeticPrefab;
		public CosmeticSlot OccupiedSlot => _occupiedSlot;

		#endregion

		#region Public methods

		/// <summary>
		/// 	Applies the cosmetic item upon selection.
		/// </summary>
		public override void Select()
		{
			base.Select();
			CosmeticController.Instance.ApplyCosmeticItem(this);
		}


		/// <summary>
		/// 	Deselect current cosmetic item.
		/// </summary>
		/// <param name="activeSelection">If set to true, an active deselection has been made.</param>
		public override void Deselect(bool activeSelection)
		{
			base.Deselect(activeSelection);
			if (!activeSelection)
			{
				return;
			}

			CosmeticController.Instance.ApplyCosmeticItem(null);
		}

		#endregion
	}
}