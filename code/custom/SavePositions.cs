using Raven;
using Sandbox;

// TEMP FIX : `public` => `internal`
internal class SavePositions : Game
{
	/// <summary>
	/// Permet de charger la dernière position du joueur à son apparition.
	/// </summary>
	[Event( "OnClientJoined" )]
	public static void OnClientJoined( Client client )
	{
		var map = Characters.GetData( client, "LastMap" );
		var position = Characters.GetData( client, "LastPosition" );

		if ( !string.IsNullOrWhiteSpace( position ) && !string.IsNullOrWhiteSpace( position ) && map == Global.MapName )
		{
			var data = position.Split( "," );

			client.Pawn.Position = new Vector3( data[0].ToFloat(), data[1].ToFloat(), data[2].ToFloat() ) ;

			Logs.Add( "Vous avez été téléporté à votre dernière position sauvegardée." );
		}
	}

	/// <summary>
	/// Permet de sauvegarder la position actuelle du joueur lors de sa déconnexion.
	/// Note : il se peut que l'événement ici soit inutile car le Pawn du joueur sera toujours invalidé lors de la déconnexion du joueur.
	/// </summary>
	[Event( "OnClientDisconnect" )]
	public static void OnClientDisconnect( Client client, NetworkDisconnectionReason _ )
	{
		Characters.SetData( client, "LastMap", Global.MapName );
		Characters.SetData( client, "LastPosition", client.Pawn.Position.ToString() );
	}
}