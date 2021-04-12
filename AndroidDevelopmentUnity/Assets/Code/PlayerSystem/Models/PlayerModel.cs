using UnityEngine;

namespace PlayerSystem.Models
{
	/// <summary>
	/// 	Data class for anything revolving around the player
	/// </summary>
	[CreateAssetMenu(fileName = "New Player Data", menuName = "Dodgey/Player/Model")]
	public class PlayerModel : ScriptableObject
	{
		[SerializeField] [Tooltip("Movement force of the player")]
		private float _movementForce = 1000f;
		[SerializeField] [Tooltip("If set to true, player cant die.")]
		private bool _debugGodMode;
		public float MovementForce => _movementForce;
		public bool DebugGodMode => _debugGodMode;
	}
}