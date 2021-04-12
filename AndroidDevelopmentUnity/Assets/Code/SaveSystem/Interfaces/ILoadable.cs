namespace SaveSystem.Interfaces
{
	/// <summary>
	/// 	Interface for loadables that need to register themselves to the <see cref="SaveManager"/>.
	/// </summary>
	public interface ILoadable
	{
		#region Public methods

		void Load();

		#endregion
	}
}