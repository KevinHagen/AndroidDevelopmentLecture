using UnityEngine;

namespace ObstacleSystem.Models
{
	/// <summary>
	/// 	Scriptable Object Representation of general obstacles.
	/// </summary>
	[CreateAssetMenu(fileName = "New Obstacle", menuName = "Dodgey/Obstacles/Obstacle", order = 0)]
	public class ObstacleModel : ScriptableObject
	{
		[SerializeField] [Tooltip("Speed in m/s with which the obstacle falls")]
		protected float _fallingSpeed = 10f;
		[SerializeField] [Tooltip("Speed in degree/s with which the obstacle rotates")]
		protected float _rotationSpeed = 90f;
		[SerializeField] [Tooltip("SFX played on impact")]
		private AudioClip _impactSFX;
		[SerializeField] [Tooltip("VFX played on impact")]
		private ParticleSystem _impactVFX;
		
		public float FallingSpeed => _fallingSpeed;
		public float RotationSpeed => _rotationSpeed;
		public AudioClip ImpactSFX => _impactSFX;
		public ParticleSystem ImpactVFX => _impactVFX;
	}
}