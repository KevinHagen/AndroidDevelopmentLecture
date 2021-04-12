using System;
using CurrencySystem;
using CurrencySystem.Model;
using ShopSystem.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.View
{
	public class ShopItemView : MonoBehaviour
	{
		#region Static Stuff

		public static event Action<ShopItemView> OnSelected;

		#endregion

		#region Serialize Fields

		[SerializeField] private Color _tooExpensiveColor;
		[SerializeField] private Color _affordableColor;
		[SerializeField] private Color _boughtColor;
		[SerializeField] private Color _selectedColor;
		[SerializeField] private Image _itemImage;
		[SerializeField] private Image _currencyImage;
		[SerializeField] private TextMeshProUGUI _priceText;
		[SerializeField] private Button _itemButton;

		#endregion

		#region Private Fields

		private ShopItemState _currentState;
		private BaseBuyable _buyable;

		#endregion

		#region Properties

		public BaseBuyable Buyable => _buyable;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_itemButton.onClick.AddListener(OnClickItemButton);

			CoinPurse.OnCoinAmountChanged += UpdateItemState;
		}

		private void OnDestroy()
		{
			CoinPurse.OnCoinAmountChanged -= UpdateItemState;
		}

		#endregion

		#region Public methods

		public void Deselect(bool activeSelection)
		{
			_buyable.Deselect(activeSelection);
			SetItemState(ShopItemState.Bought);
		}

		public void Populate(BaseBuyable buyable)
		{
			_buyable = buyable;

			if (buyable.Selected)
			{
				Select();
			}
			else if (buyable.Bought)
			{
				SetItemState(ShopItemState.Bought);
			}
			else
			{
				SetItemState(_buyable.Cost <= CoinPurse.Instance.TotalCoins ? ShopItemState.Affordable : ShopItemState.TooExpensive);
			}

			_itemImage.sprite = buyable.ItemIcon;
			_currencyImage.sprite = ShopController.Instance.Setup.GetSpriteForCurrency(buyable.CurrencyType);
			_priceText.text = buyable.Cost.ToString();
		}

		#endregion

		#region Private methods

		private void UpdateItemState(int currencyAmount, CurrencyType currencyType)
		{
			if ((currencyType != _buyable.CurrencyType) || (_currentState > ShopItemState.Affordable))
			{
				return;
			}

			SetItemState(_buyable.Cost <= currencyAmount ? ShopItemState.Affordable : ShopItemState.TooExpensive);
		}

		private void OnClickItemButton()
		{
			switch (_currentState)
			{
				case ShopItemState.TooExpensive:
					TooExpensive();
					break;
				case ShopItemState.Affordable:
					Buy();
					break;
				case ShopItemState.Bought:
					Select();
					break;
				case ShopItemState.Selected:
					Deselect(true);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Select()
		{
			_buyable.Select();
			SetItemState(ShopItemState.Selected);

			OnSelected?.Invoke(this);
		}

		private void TooExpensive()
		{
		}

		private void Buy()
		{
			_buyable.Bought = true;
			CoinPurse.Instance.SpendCoins(_buyable.Cost, _buyable.CurrencyType);
			SetItemState(ShopItemState.Bought);
		}

		private void SetItemState(ShopItemState state)
		{
			_currentState = state;

			_priceText.gameObject.SetActive(state <= ShopItemState.Affordable);
			_currencyImage.gameObject.SetActive(state <= ShopItemState.Affordable);

			switch (state)
			{
				case ShopItemState.TooExpensive:
					_itemButton.image.color = _tooExpensiveColor;
					break;
				case ShopItemState.Affordable:
					_itemButton.image.color = _affordableColor;
					break;
				case ShopItemState.Bought:
					_itemButton.image.color = _boughtColor;
					break;
				case ShopItemState.Selected:
					_itemButton.image.color = _selectedColor;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}

		#endregion
	}
}