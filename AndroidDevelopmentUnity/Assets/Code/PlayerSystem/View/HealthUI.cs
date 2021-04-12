using UnityEngine;

namespace PlayerSystem.View
{
	/// <summary>
	/// 	UI component to visualize the players current hp.
	/// </summary>
	public class HealthUI : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("Prefab for the health icon - gets instantiated once for each life of the player.")]
		private HealthIcon _healthIconPrefab;
		[SerializeField] [Tooltip("Where to place the hearts")]
		private Transform _healthIconHolder;

		#endregion

		#region Private Fields

		private HealthIcon[] _healthIcons;

		#endregion

		#region Public methods

		/// <summary>
		/// 	Instantiates one <see cref="HealthIcon"/> for each life of the player.
		/// </summary>
		/// <param name="maxHealth">The amount of lives for the player</param>
		public void InitializeForHealth(int maxHealth)
		{
			_healthIcons = new HealthIcon[maxHealth];
			for (int i = 0; i < maxHealth; i++)
			{
				_healthIcons[i] = Instantiate(_healthIconPrefab, _healthIconHolder);
			}
		}

		/// <summary>
		/// 	Updates the health to the given value and either depletes or replenishes a heart.
		/// </summary>
		/// <param name="current">Amount of lives left.</param>
		/// <param name="replenish">If set to true, a life has been replenished.</param>
		public void SetHealth(int current, bool replenish)
		{
			if (replenish)
			{
				_healthIcons[current - 1].Replenish();
			}
			else
			{
				_healthIcons[current].Deplete();
			}
		}

		#endregion
	}
}