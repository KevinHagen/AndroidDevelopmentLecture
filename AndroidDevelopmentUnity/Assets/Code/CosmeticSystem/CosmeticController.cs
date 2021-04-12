using Core;
using CosmeticSystem.Buyables;
using UnityEngine;

namespace CosmeticSystem
{
	/// <summary>
	/// 	Controller to manage current cosmetics of the player.
	/// </summary>
	public class CosmeticController : Singleton<CosmeticController>
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("Available cosmetic slots")] 
		private Transform[] _cosmeticSlots;
		[SerializeField] [Tooltip("The regular skin of the player")] 
		private CosmeticColor _regularSkin;

		#endregion

		#region Private Fields

		private GameObject _currentCosmetic;

		#endregion

		#region Public methods

		/// <summary>
		/// 	Applies the given skin to the player, swapping out its material.
		/// </summary>
		/// <param name="cosmeticColor"></param>
		public void ApplySkin(CosmeticColor cosmeticColor)
		{
			if (cosmeticColor == null)
			{
				GetComponentInParent<Renderer>().material = _regularSkin.Skin;
				return;
			}

			GetComponentInParent<Renderer>().material = cosmeticColor.Skin;
		}

		/// <summary>
		/// 	Destroy the current cosmetic and replaces it with the given one.
		/// </summary>
		/// <param name="item"></param>
		public void ApplyCosmeticItem(CosmeticItem item)
		{
			// we already have a cosmetic in the scene, remove it
			if (_currentCosmetic)
			{
				Destroy(_currentCosmetic.gameObject);
			}

			// if null, we "unequip" the current cosmetic but dont replace it
			if (item == null)
			{
				return;
			}

			// cast the slot index to int to find the respective slot and spawn the cosmetic in there
			int slotIndex = (int) item.OccupiedSlot;
			_currentCosmetic = Instantiate(item.CosmeticPrefab, _cosmeticSlots[slotIndex]);
		}

		#endregion
	}
}