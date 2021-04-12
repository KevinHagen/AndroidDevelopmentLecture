using System.Collections.Generic;
using CurrencySystem;
using ItemSystem.Models;
using ObstacleSystem;
using ObstacleSystem.Models;
using UnityEngine;

namespace ItemSystem
{
	/// <summary>
	/// 	Item that draws in coins towards the player, making collecting them easier.
	/// </summary>
	public class CoinMagnetItem : Item
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("VFX representing the active state of the item")] 
		private ParticleSystem[] _activeParticles;

		#endregion

		#region Private Fields

		private CoinMagnetItemModel _coinMagnetItemModel;
		private List<Transform> _coinsToDrawIn = new List<Transform>();

		#endregion

		#region Unity methods

		private void Awake()
		{
			_coinMagnetItemModel = (CoinMagnetItemModel) _model;
			CoinObstacle.ObstacleHitPlayer += OnHitPlayer;
		}

		protected override void Update()
		{
			base.Update();

			var rem = new List<Transform>();
			// all coins that are in the list need to be moved towards the player
			foreach (Transform t in _coinsToDrawIn)
			{
				// safety: coins can be "null" if they hit the player but didnt get removed before - mark them in a separate list
				if (t == null)
				{
					rem.Add(t);
					continue;
				}

				// move towards the player using the given step
				t.position = Vector3.MoveTowards(t.position, transform.position, _coinMagnetItemModel.DrawInSpeed);
			}

			// loop over all "null" coins from the list and remove them - avoids "ConcurrentModificationException"
			foreach (Transform t in rem)
			{
				_coinsToDrawIn.Remove(t);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!_isActive)
			{
				return;
			}

			// coin collided with item, need to mark it to "draw in"
			if (other.CompareTag("Coin"))
			{
				other.GetComponent<CoinObstacle>().DrawIn = true;
				_coinsToDrawIn.Add(other.transform);
			}
		}

		#endregion

		#region Protected methods

		protected override void UseInternal()
		{
			// enable particles
			foreach (ParticleSystem activeParticle in _activeParticles)
			{
				activeParticle.Play();
			}
		}

		protected override void FinishItemUse()
		{
			base.FinishItemUse();

			// disable particles
			foreach (ParticleSystem activeParticle in _activeParticles)
			{
				activeParticle.Stop();
			}
		}

		#endregion

		#region Private methods

		private void OnHitPlayer(CoinObstacle obj)
		{
			if (obj == null)
			{
				return;
			}

			// remove the coin from the list of coins to move towards palyer if hit
			_coinsToDrawIn.Remove(obj.transform);
		}

		#endregion
	}
}