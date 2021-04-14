using ItemSystem;
using PlayerSystem.Models;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// 	Class to manage the player.
	/// </summary>
	public class PlayerController : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("Parent transform for useable items.")]
		private Transform _itemHolder;
		[SerializeField] [Tooltip("Data representation to use for the player")]
		private PlayerModel _playerModel;

		#endregion

		#region Private Fields

		private Rigidbody _rigidbody;
		private HealthComponent _health;
		private Item _currentItem;

		#endregion

		#region Properties

		public bool IsDead => _health.IsDead;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_health = GetComponent<HealthComponent>();
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			if (Input.GetButtonDown("Item"))
			{
				UseItem();
			}
		}

		private void FixedUpdate()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			float horizontal = 0;
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				horizontal = touch.position.x / Screen.width > 0.5f ? 1f : -1f;
			}
			
			_rigidbody.velocity = new Vector3(horizontal * _playerModel.MovementForce * Time.deltaTime, 0, 0f);
#else
			_rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * _playerModel.MovementForce * Time.deltaTime, 0, 0f);
#endif
		}

		private void OnTriggerEnter(Collider other)
		{
			// hit by obstacle, receive damage (except: god mode enabled)
			if (other.gameObject.CompareTag("Obstacle") && !_playerModel.DebugGodMode)
			{
				_health.Damage();
			}

			// hit by health pack, heal
			if (other.gameObject.CompareTag("HealthPack"))
			{
				_health.Heal();
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	If present, uses the current <see cref="Item"/>.
		/// </summary>
		public void UseItem()
		{
			if (_currentItem)
			{
				_currentItem.Use();
			}
		}

		/// <summary>
		/// 	Sets the current <see cref="Item"/> of the player to the given one.
		/// </summary>
		/// <param name="item">The item to apply to the player</param>
		public void SetItem(Item item)
		{
			// we have a current item, remove it
			if (_currentItem)
			{
				Destroy(_currentItem.gameObject);
			}

			if (item == null)
			{
				return;
			}

			// instantiate new item
			_currentItem = Instantiate(item, _itemHolder);
		}

		#endregion
	}
}