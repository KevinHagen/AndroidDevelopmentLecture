namespace SaveSystem.Interfaces
{
	/// <summary>
	/// 	Interface for a saveable object - while the ISaveData interface represents the data being saved, the ISaveable represents the object that should be registered to the
	/// 	<see cref="SaveManager"/>.
	/// </summary>
	public interface ISaveable
	{
		#region Public methods

		void Save();

		#endregion
	}
}