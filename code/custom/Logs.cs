using Sandbox;
using Sandbox.UI;
using System.Text.RegularExpressions;

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
			// https://stackoverflow.com/questions/8298614/php-regular-expression-failing
			var matches = Regex.Matches( message, "[0-9]+");
			var steamID = matches.Count > 0 ? $"avatar:{matches[0]}" : null;

			ChatBox.AddInformation( message, steamID );
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
