using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace ItemSystem.View
{
	/// <summary>
	/// 	UI Component representing the item ui of the player.
	/// </summary>
	public class ItemUI : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] private Image _itemImage;
		[SerializeField] private TextMeshProUGUI _cooldownText;
		[SerializeField] private PlayableDirector _cooldownStoppedTimeline;

		#endregion

		#region Private Fields

		private Color _initialColor;

		#endregion

		#region Unity methods

		private void Awake()
		{
			Item.CooldownUpdate += UpdateUI;
			Item.CooldownStopped += FinishCooldown;
			Item.CooldownStarted += StartCooldown;
			Item.ItemUsed += UseItem;

			_initialColor = _itemImage.color;
			_cooldownText.enabled = false;
			TurnOff();
		}

		private void OnDestroy()
		{
			Item.CooldownUpdate -= UpdateUI;
			Item.CooldownStopped -= FinishCooldown;
			Item.CooldownStarted -= StartCooldown;
			Item.ItemUsed -= UseItem;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	Updates the sprite of the item.
		/// </summary>
		/// <param name="image"></param>
		public void SetItemImage(Sprite image)
		{
			gameObject.SetActive(true);
			_itemImage.sprite = image;
		}

		/// <summary>
		/// 	Disables the image component.
		/// </summary>
		public void TurnOff()
		{
			gameObject.SetActive(false);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 	Update the ItemUI based on its cooldown. Blends alpha from 0 - 1 dependant on remaining cooldown.
		/// </summary>
		/// <param name="cooldownTimeLeft">Cooldown in s left</param>
		/// <param name="cooldownTimeMax">Cooldown in s max</param>
		private void UpdateUI(float cooldownTimeLeft, float cooldownTimeMax)
		{
			Color c = _initialColor;
			float currentAlpha = 1 - (cooldownTimeLeft / cooldownTimeMax);
			_itemImage.color = new Color(c.r, c.g, c.b, currentAlpha);

			// show cooldown time as text
			_cooldownText.text = cooldownTimeLeft.ToString("00");
		}

		/// <summary>
		/// 	Cooldown has done - plays a visual effect to notify the player.
		/// </summary>
		private void FinishCooldown()
		{
			_cooldownStoppedTimeline.time = 0;
			_cooldownStoppedTimeline.Play();

			_cooldownText.enabled = false;
		}

		/// <summary>
		/// 	Item has been used - grey it out
		/// </summary>
		private void UseItem()
		{
			_itemImage.DOColor(Color.gray, 0.5f);
		}

		/// <summary>
		/// 	Starts the cooldown.
		/// </summary>
		private void StartCooldown()
		{
			_cooldownText.enabled = true;
		}

		#endregion
	}
}