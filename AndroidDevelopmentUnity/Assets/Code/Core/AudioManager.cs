using UnityEngine;

namespace Core
{
	/// <summary>
	/// 	Barebone AudioManager class to play single audio clips
	/// </summary>
	public class AudioManager : Singleton<AudioManager>
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("Where to search for all audio sources")] private Transform _sfxSourceParent;

		#endregion

		#region Private Fields

		private AudioSource[] _sfxSources;

		#endregion

		#region Unity methods

		protected override void Awake()
		{
			base.Awake();

			_sfxSources = _sfxSourceParent.GetComponentsInChildren<AudioSource>();
		}

		#endregion

		#region Public methods

		/// <summary>
		/// 	Plays an audioclip using a free audio source
		/// </summary>
		/// <param name="clip"></param>
		public void PlaySFX(AudioClip clip)
		{
			foreach (AudioSource source in _sfxSources)
			{
				// source is "used" already, cant use it
				if (source.isPlaying)
				{
					continue;
				}

				// free source found, play clip
				source.clip = clip;
				source.Play();
				break;
			}
		}

		#endregion
	}
}