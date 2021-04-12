using Core.Extensions;
using ShopSystem.Model;
using UnityEngine;

namespace ShopSystem.View
{
	public class ShopTabView : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private Transform _content;
		[Header("Own References")] [SerializeField]
		private ShopItemView _shopItemPrefab;
		[SerializeField] private Transform _shopRowPrefab;

		#endregion

		#region Private Fields

		private Transform[] _tabRows;
		private ShopCategory _category;
		private CanvasGroup _canvasGroup;
		private ShopItemView _currentSelected;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			ShopItemView.OnSelected += ShopItemSelectionChanged;
		}

		private void OnDestroy()
		{
			ShopItemView.OnSelected -= ShopItemSelectionChanged;
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

		public void InitTab(ShopCategory cat)
		{
			_category = cat;

			int rowsRequired = 1 + (_category.ItemsInCategory.Count / ShopController.Instance.ItemsPerRow);
			_tabRows = new Transform[rowsRequired];

			for (int i = 0; i < rowsRequired; i++)
			{
				_tabRows[i] = Instantiate(_shopRowPrefab, _content);
			}

			int currentRowIndex = 0;
			for (int index = 0; index < _category.ItemsInCategory.Count; index++)
			{
				BaseBuyable baseBuyable = _category.ItemsInCategory[index];
				ShopItemView shopItemView = Instantiate(_shopItemPrefab, _tabRows[currentRowIndex]);
				shopItemView.Populate(baseBuyable);

				if ((index > 0) && ((index + 1) % ShopController.Instance.ItemsPerRow == 0))
				{
					currentRowIndex++;
				}
			}
		}

		#endregion

		#region Private methods

		private void ShopItemSelectionChanged(ShopItemView obj)
		{
			if (!_category.ItemsInCategory.Contains(obj.Buyable))
			{
				return;
			}

			if (_currentSelected)
			{
				_currentSelected.Deselect(false);
			}

			_currentSelected = obj;
		}

		#endregion
	}
}