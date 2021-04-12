using UnityEngine;

namespace ItemSystem.Models
{
	/// <summary>
	/// 	ShockWave Item model. 
	/// </summary>
	[CreateAssetMenu(fileName = "New Item", menuName = "Dodgey/Items/ShockWave", order = 0)]
	public class ShockWaveItemModel : ItemModel
	{
		[SerializeField] [Tooltip("Layers affected by the shock wave item.")]
		private LayerMask _shockWaveAffectedLayers;
		
		public LayerMask ShockWaveAffectedLayers => _shockWaveAffectedLayers;
	}
}