using PlayerSystem.View;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// 	Component used to manage the players health.
	/// </summary>
	public class HealthComponent : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private int _maxHealth = 3;
		[SerializeField] private HealthUI _healthUI;

		#endregion

		#region Private Fields

		private bool _isDead;
		private int _currentHealth;

		#endregion

		#region Properties

		public bool IsDead => _isDead;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_currentHealth = _maxHealth;
		}

		private void Start()
		{
			_healthUI.InitializeForHealth(_maxHealth);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	Depletes one life from the players hp pool.
		/// </summary>
		public void Damage()
		{
			_currentHealth--;
			// player is dead
			if (_currentHealth <= 0)
			{
				_isDead = true;
			}

			// update ui
			_healthUI.SetHealth(_currentHealth, false);
		}

		/// <summary>
		/// 	Replenishes one life from the players hp pool.
		/// </summary>
		public void Heal()
		{
			// cant have more than maxHealht lives.
			if (_currentHealth < _maxHealth)
			{
				_currentHealth++;
			}

			// update ui
			_healthUI.SetHealth(_currentHealth, true);
		}

		#endregion
	}
}