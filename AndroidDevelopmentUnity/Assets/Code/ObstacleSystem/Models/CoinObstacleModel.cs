using UnityEngine;

namespace ObstacleSystem.Models
{
	/// <summary>
	/// 	Scriptable Object representation for coins.
	/// </summary>
	[CreateAssetMenu(fileName = "New Coin Obstacle", menuName = "Dodgey/Obstacles/Coin", order = 0)]
	public class CoinObstacleModel : ObstacleModel
	{
		[SerializeField] [Tooltip("Amount of currency rewarded by this coin")]
		private int _value = 1;
		
		public int Value => _value;
	}
}