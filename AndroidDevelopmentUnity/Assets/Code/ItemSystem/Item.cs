using System;
using ItemSystem.Models;
using UnityEngine;

namespace ItemSystem
{
	/// <summary>
	/// 	Base class for all items in the game.
	/// </summary>
	public abstract class Item : MonoBehaviour
	{
		#region Static Stuff

		/// <summary>
		/// 	Called when the cooldown gets updated.
		/// </summary>
		public static event Action<float, float> CooldownUpdate;
		/// <summary>
		/// 	Called when the item is being used.
		/// </summary>
		public static event Action ItemUsed;
		/// <summary>
		/// 	Called when the cooldown starts.
		/// </summary>
		public static event Action CooldownStarted;
		/// <summary>
		/// 	Called when the cooldown has stopped.
		/// </summary>
		public static event Action CooldownStopped;

		#endregion

		#region Serialize Fields

		[SerializeField] protected ItemModel _model;
		[SerializeField] private ParticleSystem[] _onUseVFX;

		#endregion

		#region Protected Fields

		protected bool _isActive;

		#endregion

		#region Private Fields

		private float _remainingDuration;
		private float _remainingCooldown;
		private bool _isOnCooldown;

		#endregion

		#region Unity methods

		protected virtual void Update()
		{
			// is the item on cooldown?
			if (_isOnCooldown)
			{
				// update remaining cooldown
				_remainingCooldown -= Time.deltaTime;
				CooldownUpdate?.Invoke(_remainingCooldown, _model.CooldownTime);
				
				// cooldown done?
				if (_remainingCooldown <= 0)
				{
					_isOnCooldown = false;
					CooldownStopped?.Invoke();
				}
			}

			// is the item being actively used?
			if (_isActive)
			{
				// update time left
				_remainingDuration -= Time.deltaTime;
				
				// item duration done?
				if (_remainingDuration <= 0)
				{
					FinishItemUse();
				}
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	Uses the item, if it can be used.
		/// </summary>
		public void Use()
		{
			if (!CanBeUsed())
			{
				return;
			}

			// play some nice vfx to support item
			foreach (ParticleSystem system in _onUseVFX)
			{
				system.Play();
			}

			UseInternal();
			// instant use means cooldown starts directly after usage, otherwise cooldown starts after effect has stopped
			if (_model.InstantUse)
			{
				StartCooldown();
			}
			else
			{
				_isActive = true;
				_remainingDuration = _model.Duration;
				ItemUsed?.Invoke();
			}
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// 	Implements internal effect of a given, concrete item.
		/// </summary>
		protected abstract void UseInternal();

		protected virtual void FinishItemUse()
		{
			_isActive = false;
			StartCooldown();
		}

		/// <summary>
		/// 	Sets the own transform and all children to the local scale given in <see cref="ItemModel"/>.
		/// </summary>
		/// <param name="tr">Transform to update</param>
		protected void ApplyRadiusToScaleRecursively(Transform tr)
		{
			foreach (Transform t in tr)
			{
				if (t.childCount > 1)
				{
					ApplyRadiusToScaleRecursively(t);
				}

				t.localScale = new Vector3(_model.EffectRadius, _model.EffectRadius, _model.EffectRadius);
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 	Starts the cooldown of this item.
		/// </summary>
		private void StartCooldown()
		{
			_isOnCooldown = true;
			_remainingCooldown = _model.CooldownTime;
			CooldownStarted?.Invoke();
		}

		/// <summary>
		/// 	Returns a bool on whether this item can be used or not.
		/// </summary>
		/// <returns>True if the item is usable</returns>
		private bool CanBeUsed()
		{
			return !_isOnCooldown;
		}

#if UNITY_EDITOR

		private void OnValidate()
		{
			ApplyRadiusToScaleRecursively(transform);
		}

#endif

		#endregion
	}
}