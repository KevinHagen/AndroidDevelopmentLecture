using System.Collections;
using Core;
using Core.Random;
using ObstacleSystem.Models;
using PlayerSystem;
using UnityEditor;
using UnityEngine;

namespace ObstacleSystem
{
	/// <summary>
	/// 	Spawns clusters of obstacles in intervals.
	/// </summary>
	public class ObstacleSpawner : MonoBehaviour
	{
		#region Static Stuff

		private const int MaxAttemptsForPositionCheck = 1000;

		#endregion

		#region Serialize Fields

		[SerializeField] private ObstacleSpawnerModel _model;
		[SerializeField] private Transform _spawnTransform;

		#endregion

		#region Private Fields

		private float _lastTimeSpawned;
		private PlayerController _player;
		private Vector3 _position;
		private Vector3[] _spawnPositions;
		private int _obstacleAmountForCluster;

		#endregion

		#region Properties

		public float SpawnRate => 1 / _model.SpawnRate;

		#endregion

		#region Unity methods

		private void Awake()
		{
			// search for player - required for position of spawning clusters
			_player = FindObjectOfType<PlayerController>();
		}

		void Update()
		{
			// check on spawn time
			if (_lastTimeSpawned + SpawnRate < Time.time)
			{
				StartCoroutine(SpawnCluster());
			}
		}

		#endregion

		#region Private methods

		private IEnumerator SpawnCluster()
		{
			// spawning started, update spawn time to prevent premature cluster spawning
			_lastTimeSpawned = Time.time;

			// calc center point
			float clusterCenterPointX = _player != null ? _player.transform.position.x : _spawnTransform.position.x;

			// randomize amount of obstacles for this cluster
			_obstacleAmountForCluster = Random.Range(_model.ClusterSizeMin, _model.ClusterSizeMax);
			_spawnPositions = new Vector3[_obstacleAmountForCluster];
			for (int i = 0; i < _obstacleAmountForCluster; i++)
			{
				SpawnObstacle(clusterCenterPointX, i);
				// wait for SpawnDelay seconds between individual spawns in a cluster
				yield return new WaitForSeconds(_model.SpawnDelay);
			}

			// spawning done - update spawn time
			_lastTimeSpawned = Time.time;
		}

		
		private void SpawnObstacle(float centerPointX, int i)
		{
			// try to find a valid x position that is at least _model.MinDistanceBetweenObstaclesInCluster apart from the previous ones.
			float x;
			// security fallback: dont loop more than MaxAttempts to avoid potential endless loop
			int attempts = 0;
			do
			{
				x = centerPointX + Random.Range(-_model.ClusterRadius, _model.ClusterRadius);
				attempts++;
			} while ((attempts <= MaxAttemptsForPositionCheck) && !IsFreeSpot(x, i));

			// position found
			_spawnPositions[i] = new Vector3(x, _spawnTransform.position.y, _spawnTransform.position.z);

			// return if were not in playmode
			if (!Application.isPlaying)
			{
				return;
			}

			// spawn the obstacle choosing a random prefab from the list
			BaseObstacle chosenPrefab = _model.ObstaclePrefabs.ChooseRandom(Random.value);
			BaseObstacle instantiate = Instantiate(chosenPrefab);
			
			// update position & rotation, randomize spawn left/right
			instantiate.transform.position = _spawnPositions[i];
			instantiate.transform.rotation = Quaternion.Euler(0f, Random.value > 0.5f ? 0f : 180f, Random.Range(0f, 360f));
			instantiate.IsSpinningLeft = Random.value > 0.5f;
		}

		/// <summary>
		/// 	Returns true if the chosen x position is still free, meeting the criteria set in <see cref="ObstacleSpawnerModel"/>.
		/// </summary>
		/// <param name="x">x position to check</param>
		/// <param name="index">Current index</param>
		/// <returns>true if position is valid</returns>
		private bool IsFreeSpot(float x, int index)
		{
			for (int i = 0; i < index; i++)
			{
				if (Mathf.Abs(_spawnPositions[i].x - x) < _model.MinDistanceBetweenObstaclesInCluster)
				{
					return false;
				}
			}

			return true;
		}

		#endregion

#if UNITY_EDITOR
		public void Regenerate()
		{
			SpawnCluster();
		}
		
		private void OnDrawGizmosSelected()
		{
			// Draw some gizmos for in-editor vis of object spawning
			for (int i = 0; i < _obstacleAmountForCluster; i++)
			{
				Gizmos.color = _model.ObstacleColor;
				Vector3 obstaclePos = _spawnPositions[i];
				GUIStyle guiStyle = new GUIStyle { fontSize = 42, };
				Handles.Label(obstaclePos, i.ToString(), guiStyle);
				Gizmos.DrawSphere(obstaclePos, 1f);

				if (i > 0)
				{
					Gizmos.color = _model.LineColor;
					float xDiff = obstaclePos.x - _spawnPositions[i - 1].x;
					Handles.Label(obstaclePos - (Vector3.right * (xDiff * 0.5f)), $"Distance is {Mathf.Abs(_spawnPositions[i].x - _spawnPositions[i - 1].x)}", guiStyle);
					Gizmos.DrawLine(_spawnPositions[i - 1], _spawnPositions[i]);
				}
			}

			Gizmos.color = _model.Color;
			Gizmos.DrawCube(_spawnTransform.position, new Vector3(_model.ClusterRadius * 2, 3f, 6f));
		}
#endif
	}
}