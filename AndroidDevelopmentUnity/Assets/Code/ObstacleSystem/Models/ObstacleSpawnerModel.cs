using Core.Random;
using UnityEngine;

namespace ObstacleSystem.Models
{
	/// <summary>
	/// 	Scriptable Object Representation of the Obstacle Spawner Data.
	/// </summary>
	[CreateAssetMenu(fileName = "New Obstacle", menuName = "Dodgey/Obstacles/SpawnerModel", order = 0)]
	public class ObstacleSpawnerModel : ScriptableObject
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("List of potential obstacle spawns with their given probability.")] 
		private ProbabilityItemList<BaseObstacle> _obstaclePrefabs = new ProbabilityItemList<BaseObstacle>();
		[SerializeField] [Tooltip("Amount of spawns in objects/second")] 
		private float _spawnRate = 2f;
		[SerializeField] [Tooltip("Minimum amount of objects spawned in a cluster")] 
		private int _clusterSizeMin = 2;
		[SerializeField] [Tooltip("Maximum amount of objects spawned in a cluster")] 
		private int _clusterSizeMax = 5;
		[SerializeField] [Tooltip("Radius in meters for a cluster (centered at player)")] 
		private float _clusterRadius = 1.75f;
		[SerializeField] [Tooltip("Delay between spawns")] 
		private float _spawnDelay = 0.25f;
		[SerializeField] [Tooltip("Spawn Area color for debug drawing")] 
		private Color _color = new Color(0.75f, 0.25f, 033f, 0.25f);
		[SerializeField] [Tooltip("Obstacle color for debug drawing")] 
		private Color _obstacleColor = new Color(0.75f, 0.25f, 033f, 0.25f);
		[SerializeField] [Tooltip("Line Color for debug drawing")] 
		private Color _lineColor = new Color(0.75f, 0.25f, 033f, 0.25f);
		[SerializeField] [Tooltip("min distance in m between obstacles within a given cluster")] 
		private float _minDistanceBetweenObstaclesInCluster = 0.5f;

		#endregion

		#region Properties

		public float ClusterRadius => _clusterRadius;
		public int ClusterSizeMax => _clusterSizeMax;
		public int ClusterSizeMin => _clusterSizeMin;
		public Color Color => _color;
		public Color LineColor => _lineColor;
		public float MinDistanceBetweenObstaclesInCluster => _minDistanceBetweenObstaclesInCluster;
		public Color ObstacleColor => _obstacleColor;
		public ProbabilityItemList<BaseObstacle> ObstaclePrefabs => _obstaclePrefabs;
		public float SpawnDelay => _spawnDelay;
		public float SpawnRate => _spawnRate;

		#endregion
	}
}