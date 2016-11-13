using System;
namespace TileStyleSDK
{
    /// <summary>
    /// Represents the Exception thrown when the user tries to interact with a Panel that does not exist.
    /// </summary>
    [Serializable]
    public class PanelNotFoundException : Exception
    {
        public PanelNotFoundException ()
        {
        }

        public PanelNotFoundException(string message) : base (message)
        {
        }

        public PanelNotFoundException(string message, Exception innerException) : base (message, innerException)
        {
        }
    }
}
