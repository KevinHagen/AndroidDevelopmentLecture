using System.Collections.Generic;
using System.Linq;
using Core;
using GameFlowSystem.View;
using SaveSystem;
using SaveSystem.DTOs;
using SaveSystem.Interfaces;
using ShopSystem.Model;
using ShopSystem.View;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
	/// <summary>
	/// 	Controls the shop user interface
	/// </summary>
	public class ShopController : Singleton<ShopController>, ISaveable, ILoadable
	{
		#region Serialize Fields

		[SerializeField] private Button _backButton;
		[SerializeField] private ShopSetup _setup;
		[SerializeField] private ShopScreen _screen;
		[SerializeField] private int _defaultTabIndex;
		[SerializeField] private int _itemsPerRow = 7;
		[SerializeField] private ShopTabButton _tabButtonPrefab;
		[SerializeField] private ShopTabView _tabViewPrefab;

		#endregion

		#region Private Fields

		private ShopTabView _currentTab;
		private ShopTabView[] _shopTabs;

		#endregion

		#region Properties

		public int ItemsPerRow => _itemsPerRow;
		public ShopSetup Setup => _setup;

		#endregion

		#region Unity methods

		protected override void Awake()
		{
			base.Awake();

			// sub events
			MainMenuUI.OnOpenShop += OpenShop;
			SaveManager.OnLoadingDone += AfterLoading;
			ShopTabButton.OnClickShopTabButton += SwitchTab;

			// sub to savemanager
			SaveManager.RegisterLoadable(this);
			SaveManager.RegisterSaveable(this);

			// back button listener
			_backButton.onClick.AddListener(OnClickBackButton);
		}

		private void OnDestroy()
		{
			// unsub all of the above
			MainMenuUI.OnOpenShop -= OpenShop;
			ShopTabButton.OnClickShopTabButton -= SwitchTab;
			SaveManager.OnLoadingDone -= AfterLoading;

			SaveManager.UnregisterLoadable(this);
			SaveManager.UnregisterSaveable(this);

			_backButton.onClick.RemoveAllListeners();
		}

		#endregion

		#region Public methods

		private void OpenShop()
		{
			_currentTab = _shopTabs[_defaultTabIndex];
			_currentTab.Show();
			_screen.Show();
		}

		/// <summary>
		/// 	Hides the shop window & saves the state of bought items
		/// </summary>
		private void CloseShop()
		{
			_currentTab.Hide();
			_screen.Close();

			SaveManager.Save();
		}

		private void InitShop(ShopSetup setup)
		{
			InitCategories(setup.Categories);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 	Called once loading has been done, used to initialize shop state
		/// </summary>
		private void AfterLoading()
		{
			InitShop(_setup);
		}

		/// <summary>
		/// 	Close the shop
		/// </summary>
		private void OnClickBackButton()
		{
			CloseShop();
		}

		/// <summary>
		/// 	Switches current shop tab to the one with the respective index.
		/// </summary>
		/// <param name="tabIndex">Tab index to switch to</param>
		private void SwitchTab(int tabIndex)
		{
			// make sure we dont go out of bounds
			if ((tabIndex < 0) || (tabIndex >= _setup.Categories.Length))
			{
				return;
			}

			// hide current tab, re assign and show again
			_currentTab.Hide();
			_currentTab = _shopTabs[tabIndex];
			_currentTab.Show();
		}

		private void InitCategories(ShopCategory[] setupCategories)
		{
			// init array
			_shopTabs = new ShopTabView[setupCategories.Length];

			// loop over all available categories - they come from the setup, see ShopCategory
			for (int index = 0; index < setupCategories.Length; index++)
			{
				// the current category
				ShopCategory setupCategory = setupCategories[index];

				// spawn it 
				ShopTabView tabView = Instantiate(_tabViewPrefab);
				tabView.InitTab(setupCategory);
				tabView.Hide();
				_shopTabs[index] = tabView;

				// spawn the button opening the tab
				ShopTabButton tabButton = Instantiate(_tabButtonPrefab);
				tabButton.Init(index, setupCategory.name);

				// add tab to the screen
				_screen.AddTab(tabView, tabButton);
			}
		}

		#endregion

		#region ILoadable Members

		public void Load()
		{
			// do we already have a saved object?
			BuyableDto dto = SaveManager.Get(SaveKeys.BuyableDataKey, new BuyableDto()) as BuyableDto;
			if (dto == null)
			{
				return;
			}

			// no save data yet, initialize default
			if ((dto.BuyableData == null) || (dto.BuyableData.Count == 0))
			{
				foreach (ShopCategory category in _setup.Categories)
				{
					foreach (BaseBuyable baseBuyable in category.ItemsInCategory)
					{
						baseBuyable.Bought = false;
						baseBuyable.Selected = false;
					}
				}

				return;
			}

			// save data found - need to loop through all categories and see if we have save data for an object and if yes load the corresponding data
			foreach (ShopCategory category in _setup.Categories)
			{
				foreach (BaseBuyable baseBuyable in category.ItemsInCategory)
				{
					// searches for the buyable data stored in the save game that has the same name as the current buyable from the category
					BuyableData firstOrDefault = dto.BuyableData.FirstOrDefault(b => b.Name.Equals(baseBuyable.name));
					// buyable found, assign its values from save
					if (firstOrDefault != null)
					{
						baseBuyable.Bought = firstOrDefault.Bought;
						baseBuyable.Selected = firstOrDefault.Selected;
					}
					// new buyable that is not yet saved, assign default values
					else
					{
						baseBuyable.Bought = false;
						baseBuyable.Selected = false;
					}
				}
			}
		}

		#endregion

		#region ISaveable Members

		public void Save()
		{
			BuyableDto dto = new BuyableDto();
			List<BuyableData> data = new List<BuyableData>();

			// Loop over all buyable items in the shop and store their current values (name, did buy, is selected) in a data object
			foreach (ShopCategory category in _setup.Categories)
			{
				foreach (BaseBuyable baseBuyable in category.ItemsInCategory)
				{
					data.Add(new BuyableData(
						baseBuyable.name,
						baseBuyable.Bought,
						baseBuyable.Selected)
					);
				}
			}

			// save data object to save manager
			dto.BuyableData = data;
			SaveManager.Put(SaveKeys.BuyableDataKey, dto);
		}

		#endregion
	}
}