using CurrencySystem.Model;
using UnityEngine;

namespace ShopSystem.Model
{
	[CreateAssetMenu(menuName = "Dodgey/Shop/Setup", fileName = "ShopSetup", order = 0)]
	public class ShopSetup : ScriptableObject
	{
		#region Serialize Fields

		[SerializeField] private Sprite[] _currencySprites;
		[SerializeField] private Sprite _fallbackCurrencySprite;
		[SerializeField] private ShopCategory[] _categories;

		#endregion

		#region Properties

		public ShopCategory[] Categories => _categories;

		#endregion

		#region Public methods

		// TODO handle different currency types, move to setup class
		public Sprite GetSpriteForCurrency(CurrencyType type)
		{
			int indexOfType = (int) type;
			// fallback to default if something goes wrong
			return indexOfType < _currencySprites.Length ? _currencySprites[(int) type] : _fallbackCurrencySprite;
		}

		#endregion
	}
}