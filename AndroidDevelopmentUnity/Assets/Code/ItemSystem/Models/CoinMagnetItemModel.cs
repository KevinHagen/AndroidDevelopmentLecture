using UnityEngine;

namespace ItemSystem.Models
{
	/// <summary>
	/// 	Model for the <see cref="CoinMagnetItem"/>.
	/// </summary>
	[CreateAssetMenu(fileName = "New Item", menuName = "Dodgey/Items/CoinMagnet", order = 0)]
	public class CoinMagnetItemModel : ItemModel
	{
		[SerializeField] [Range(0, 1)] [Tooltip("Speed at which the coins are being drawn in - steps from 0 - 1")]
		private float _drawInSpeed = 0.85f;
		
		public float DrawInSpeed => _drawInSpeed;
	}
}