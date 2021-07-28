using Sandbox;

namespace Raven
{
	public partial class Logs : Game
	{
		/// <summary>
		///
		/// </summary>
		public enum Type
		{
			Warning = 0,
			Error = 1,
			Info = 2
		}

		/// <summary>
		///
		/// </summary>
		[ClientRpc]
		public static void SendChatMessage( string message )
		{
			_ = ConsoleSystem.Run( "chat_addinfo", message );
		}

		/// <summary>
		///
		/// </summary>
		public static void Add( string message, bool toAdmins = true, int type = 0 )
		{
			if ( toAdmins )
			{
				SendChatMessage( message );
			}

			Log.Info( "[Raven] " + message );
		}
	}
}
