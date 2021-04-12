using Core.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFlowSystem.View
{
	public class GameOverScreen : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private Button _replayButton;
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private TextMeshProUGUI _highScoreText;

		#endregion

		#region Private Fields

		private CanvasGroup _canvasGroup;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			_replayButton.onClick.AddListener(OnClickReplay);
			Hide();
		}

		private void OnDestroy()
		{
			_replayButton.onClick.AddListener(OnClickReplay);
		}

		#endregion

		#region Public methods

		public void SetScore(int score)
		{
			_scoreText.text = $"Score: {score.ToString()}";
		}

		public void Show()
		{
			_canvasGroup.Enable();
		}

		private void Hide()
		{
			_canvasGroup.Disable();
		}

		#endregion

		#region Private methods

		private void OnClickReplay()
		{
			GameFlow.Instance.GameOverState.Continue = true;
		}

		#endregion
	}
}