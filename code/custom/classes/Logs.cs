using Sandbox;
using Sandbox.UI;
using System.Text.RegularExpressions;

namespace Raven
{
	public partial class Logs : Game
	{
		/// <summary>
		/// Énumérations pour les types de journalisation.
		/// </summary>
		public enum Type : byte
		{
			Warning = 0,
			Error = 1,
			Info = 2
		}

		/// <summary>
		/// Permet d'afficher un message dans le chat directement depuis un appel serveur.
		/// </summary>
		[ClientRpc]
		public static void SendChatMessage( string message )
		{
			var matches = Regex.Matches( message, "([0-9]{17,25})" );
			var steamID = matches.Count > 0 ? $"avatar:{ matches[0] }" : null;

			ChatBox.AddInformation( message, steamID );
		}

		/// <summary>
		/// Permet de créer une nouvelle entrée dans la journalisation de la console.
		/// </summary>
		public static void Add( string message, bool toAdmins = true, Type type = Type.Info )
		{
			message = "[Raven] " + message;

			if ( toAdmins )
				SendChatMessage( message );

			switch ( type )
			{
				case Type.Warning:
					Log.Warning( message );
					break;

				case Type.Error:
					Log.Error( message );
					break;

				case Type.Info:
					Log.Info( message );
					break;
			}
		}
	}
}
