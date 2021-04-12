using System;
using SaveSystem.Interfaces;

namespace SaveSystem.DTOs
{
	/// <summary>
	/// 	Data transfer obejct for the Score data - stores score.
	/// </summary>
	[Serializable]
	public class ScoreDataDto : ISaveData
	{
		#region Public Fields

		public int Score;

		#endregion

		#region ISaveData Members

		public void Save(ISaveData data)
		{
			Score = (data as ScoreDataDto).Score;
		}

		#endregion
	}
}