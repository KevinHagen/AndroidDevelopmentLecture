using UnityEngine;

namespace ItemSystem
{
	/// <summary>
	/// 	Shield item blocking off hostile Obstacles.
	/// </summary>
	public class ShieldItem : Item
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("Scene Reference to the shield object.")] private GameObject _shieldObject;

		#endregion

		#region Private Fields

		private Collider _shieldCollider;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_shieldCollider = GetComponentInChildren<Collider>();
			ToggleShield(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Obstacle"))
			{
				Destroy(other.gameObject);
			}
		}

		#endregion

		#region Protected methods

		protected override void UseInternal()
		{
			// enable shield
			ToggleShield(true);
		}

		protected override void FinishItemUse()
		{
			base.FinishItemUse();

			// disable shield
			ToggleShield(false);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 	Toggles the state of the shield.
		/// </summary>
		/// <param name="active">If set to true, enables the shield.</param>
		private void ToggleShield(bool active)
		{
			_shieldObject.SetActive(active);
			_shieldCollider.enabled = active;
		}

		#endregion
	}
}