using ShopSystem;
using UnityEngine;

namespace CosmeticSystem.Buyables
{
	/// <summary>
	/// 	A "Skin" for the player - changes its color by swapping materials out.
	/// 	Scriptable Object Data representation. 
	/// </summary>
	[CreateAssetMenu(fileName = "New Color Item", menuName = "Dodgey/Shop/Cosmetics/Color")]
	public class CosmeticColor : BaseBuyable
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("The material used by the skin.")] private Material _skin;

		#endregion

		#region Properties

		public Material Skin => _skin;

		#endregion

		#region Public methods

		/// <summary>
		/// 	Applies the skin upon selection.
		/// </summary>
		public override void Select()
		{
			base.Select();
			CosmeticController.Instance.ApplySkin(this);
		}

		/// <summary>
		/// 	Reapplies the regular skin upon active deselection or simply removes the current one.
		/// </summary>
		/// <param name="activeSelection">If set to true, an active deselection has been made.</param>
		public override void Deselect(bool activeSelection)
		{
			base.Deselect(activeSelection);
			if (!activeSelection)
			{
				return;
			}

			CosmeticController.Instance.ApplySkin(null);
		}

		#endregion
	}
}