using System.Collections;
using GameFlowSystem.States;
using TMPro;
using UnityEngine;

namespace CurrencySystem.View
{
	/// <summary>
	/// 	UI component to visualize the currency for coins in the game over flow.
	/// </summary>
	public class GameOverCoinUI : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private TextMeshProUGUI _coinAmountText;
		[SerializeField] private float _duration = 0.75f;
		[SerializeField] private bool _isTotal;

		#endregion

		#region Unity methods

		private void Awake()
		{
			GameOverState.GameOverStateEnter += OnGameOverStateEnter;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	Called by a Timeline Signal to control when exactly this should happen. 
		/// </summary>
		public void AnimateCoinText()
		{
			StartCoroutine(TweenTextFromZeroToTargetCoroutine());
		}

		#endregion

		#region Private methods

		private void OnGameOverStateEnter()
		{
			_coinAmountText.text = _isTotal ? CoinPurse.Instance.TotalCoins.ToString() : "000";
		}

		/// <summary>
		/// 	Coroutine that animates the text component from where it currently is to the target amount. E.g. from 23 coins -> 57.
		/// </summary>
		/// <returns></returns>
		private IEnumerator TweenTextFromZeroToTargetCoroutine()
		{
			int coinsCollectedThisRun = CoinPurse.Instance.CoinsCollectedThisRun;
			int target = _isTotal ? (CoinPurse.Instance.TotalCoins + coinsCollectedThisRun) : coinsCollectedThisRun;

			float current = _isTotal ? CoinPurse.Instance.TotalCoins : 0;
			float diff = target - current;
			while (current < target)
			{
				current += diff * (Time.unscaledDeltaTime / _duration);
				_coinAmountText.text = current.ToString("000");

				yield return null;
			}

			_coinAmountText.text = target.ToString();
			if (_isTotal)
			{
				CoinPurse.Instance.AddCoinsToTotal(coinsCollectedThisRun);
			}
		}

		#endregion
	}
}