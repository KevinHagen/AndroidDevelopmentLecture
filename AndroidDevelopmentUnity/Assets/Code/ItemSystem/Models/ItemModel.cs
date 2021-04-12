using UnityEngine;

namespace ItemSystem.Models
{
	/// <summary>
	/// 	Base model for all items. Contains general settings like cooldown etc.
	/// </summary>
	[CreateAssetMenu(fileName = "New Item", menuName = "Dodgey/Items/Item", order = 0)]
	public class ItemModel : ScriptableObject
	{
		[SerializeField] [Tooltip("Cooldown in seconds")]
		private float _cooldownTime = 15f;
		[SerializeField] [Tooltip("If set to true, the item is being used instantaneously instead of over time")]
		private bool _instantUse;
		[SerializeField] [Tooltip("Duration in s - only has an effect if _instantUse is set to true.")]
		private float _duration = 5f;
		[SerializeField] [Tooltip("Radius in m that is affected by this item.")]
		private float _effectRadius = 5f;
		
		public float CooldownTime => _cooldownTime;
		public bool InstantUse => _instantUse;
		public float Duration => _duration;
		public float EffectRadius => _effectRadius;
	}
}