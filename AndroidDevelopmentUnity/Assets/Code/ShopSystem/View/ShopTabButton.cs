using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.View
{
	public class ShopTabButton : MonoBehaviour
	{
		#region Static Stuff

		public static event Action<int> OnClickShopTabButton;

		#endregion

		#region Serialize Fields

		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _tabNameText;

		#endregion

		#region Private Fields

		private int _tabIndex;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_button.onClick.AddListener(Show);
		}

		#endregion

		#region Public methods

		public void Init(int tabIndex, string catName)
		{
			_tabIndex = tabIndex;
			_tabNameText.text = catName;
		}

		#endregion

		#region Private methods

		private void Show()
		{
			OnClickShopTabButton?.Invoke(_tabIndex);
		}

		#endregion
	}
}