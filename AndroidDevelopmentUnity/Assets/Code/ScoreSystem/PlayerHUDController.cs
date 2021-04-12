using Core;
using Core.Extensions;
using UnityEngine;

namespace ScoreSystem
{
	public class PlayerHUDController : MonoBehaviour
	{
		#region Private Fields

		private CanvasGroup _canvasGroup;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			_canvasGroup.Disable();
		}

		#endregion

		#region Public methods

		public void Show()
		{
			_canvasGroup.Enable();
		}

		public void Hide()
		{
			_canvasGroup.Disable();
		}

		#endregion
	}
}