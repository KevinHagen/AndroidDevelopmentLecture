using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFlowSystem.View
{
	public class MainMenuUI : MonoBehaviour
	{
		#region Static Stuff

		public static event Action OnStartGame;
		public static event Action OnOpenShop;

		#endregion

		#region Serialize Fields

		[SerializeField] private Button _startGameButton;
		[SerializeField] private Button _shopButton;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_startGameButton.onClick.AddListener(OnClickStartGame);
			_shopButton.onClick.AddListener(OnClickShop);
		}

		#endregion

		#region Private methods

		private void OnClickShop()
		{
			OnOpenShop?.Invoke();
		}

		private void OnClickStartGame()
		{
			OnStartGame?.Invoke();
		}

		#endregion
	}
}