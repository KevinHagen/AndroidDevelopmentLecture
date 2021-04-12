using CurrencySystem.Model;
using ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CurrencySystem.View
{
	/// <summary>
	/// 	UI Component to visualize a given currencyType and the amount the player has.
	/// </summary>
	public class CurrencyView : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("The type of currency to visualize in the UI")]
		private CurrencyType _currencyType = CurrencyType.Regular;
		[SerializeField] [Tooltip("Image component to display the currency icon.")]
		private Image _currencyIcon;
		[SerializeField] [Tooltip("Text component to show the amount of currency available")]
		private TextMeshProUGUI _currencyAmountText;

		#endregion

		#region Unity methods

		private void Start()
		{
			// listen to event in case coin amount changes
			CoinPurse.OnCoinAmountChanged += UpdateCurrencyView;

			_currencyIcon.sprite = ShopController.Instance.Setup.GetSpriteForCurrency(_currencyType);
			_currencyAmountText.text = CoinPurse.Instance.TotalCoins.ToString();
		}

		private void OnDestroy()
		{
			// make sure to unsub from event to avoid memory leaks
			CoinPurse.OnCoinAmountChanged -= UpdateCurrencyView;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	Updates the text component to match the amount of total coins.
		/// </summary>
		public void UpdateView()
		{
			_currencyAmountText.text = CoinPurse.Instance.TotalCoins.ToString();
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 	Updates the text component to match the amount of currency given the type, in case <see cref="CoinPurse.OnCoinAmountChanged"/> was called.
		/// </summary>
		/// <param name="arg1">The new amount of currency</param>
		/// <param name="arg2">The type of currency that has changed</param>
		private void UpdateCurrencyView(int arg1, CurrencyType arg2)
		{
			// not our currency, return
			if (arg2 != _currencyType)
			{
				return;
			}

			// update value
			_currencyAmountText.text = arg1.ToString();
		}

		#endregion
	}
}