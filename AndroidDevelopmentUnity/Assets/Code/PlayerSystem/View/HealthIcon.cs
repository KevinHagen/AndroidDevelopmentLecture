using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem.View
{
	/// <summary>
	/// 	UI Class visualizing a single health icon.
	/// </summary>
	public class HealthIcon : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private Image _healthFG;

		#endregion

		#region Unity methods

		private void Awake()
		{
			// ensure view is "filled"
			_healthFG.enabled = true;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	Enables the healthIcon.
		/// </summary>
		public void Replenish()
		{
			_healthFG.enabled = true;
		}

		/// <summary>
		/// 	Disables the healthIcon.
		/// </summary>
		public void Deplete()
		{
			_healthFG.enabled = false;
		}

		#endregion
	}
}