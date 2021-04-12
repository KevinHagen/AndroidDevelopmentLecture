using TMPro;
using UnityEngine;

namespace CurrencySystem.View
{
	/// <summary>
	/// 	UI Component for in game (during match) representation of coins gathered this round.
	/// </summary>
	public class IngameCoinUI : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private TextMeshProUGUI _coinAmountText;

		#endregion

		#region Unity methods

		private void Awake()
		{
			CoinPurse.UpdateCoinsDuringRun += OnUpdateCoinsDuringRun;
			_coinAmountText.text = "0";
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 	Listens to <see cref="CoinPurse.UpdateCoinsDuringRun"/> to monitor coin change and visualize to that to the player.
		/// </summary>
		/// <param name="obj"></param>
		private void OnUpdateCoinsDuringRun(int obj)
		{
			_coinAmountText.text = obj.ToString();
		}

		#endregion
	}
}