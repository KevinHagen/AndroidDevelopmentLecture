using Core.Extensions;
using CurrencySystem.View;
using UnityEngine;

namespace ShopSystem.View
{
	public class ShopScreen : MonoBehaviour
	{
		#region Serialize Fields

		[Header("Prefabs & Spawning")] [SerializeField]
		private Transform _tabButtonParent;
		[SerializeField] private Transform _tabViewParent;
		[SerializeField] private CurrencyView[] _currencyViews;

		#endregion

		#region Private Fields

		private CanvasGroup _canvasGroup;

		#endregion

		#region Unity methods

		protected void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();

			_canvasGroup.Disable();
		}

		#endregion

		#region Public methods

		public void Show()
		{
			_canvasGroup.Enable();

			foreach (CurrencyView currencyView in _currencyViews)
			{
				currencyView.UpdateView();
			}
		}

		public void Close()
		{
			_canvasGroup.Disable();
		}

		public void AddTab(ShopTabView tabView, ShopTabButton tabButton)
		{
			tabView.transform.SetParent(_tabViewParent, false);
			tabButton.transform.SetParent(_tabButtonParent, false);
		}

		#endregion
	}
}