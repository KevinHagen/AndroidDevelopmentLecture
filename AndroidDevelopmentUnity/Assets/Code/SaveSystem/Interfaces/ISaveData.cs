namespace SaveSystem.Interfaces
{
	/// <summary>
	/// 	Interface to implement save data.
	/// </summary>
	public interface ISaveData
	{
		#region Public methods

		void Save(ISaveData data);

		#endregion
	}
}