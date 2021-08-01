using Raven;
using Sandbox;

internal class LogEvents : Game
{
	/// <summary>
	/// Se déclenche lors d'un rafraîchissement des fichiers.
	/// </summary>
	[Event.Hotload]
	public void OnHotload()
	{
		if ( IsServer )
			Logs.Add( "Le serveur semble avoir procéder à un rechargement des fichiers." );
	}

	/// <summary>
	/// Se déclenche lorsqu'un joueur se connecte au serveur.
	/// </summary>
	[Event( "OnClientJoined" )]
	public static void OnClientJoined( Client client )
	{
		Logs.Add( $"Apparition du joueur : { client.Name } ({ client.SteamId })." );
	}

	/// <summary>
	/// Se déclenche lorsqu'un joueur se déconnecte du serveur.
	/// </summary>
	[Event( "OnClientDisconnect" )]
	public static void OnClientDisconnect( Client client, NetworkDisconnectionReason reason )
	{
		Logs.Add( $"Déconnexion du joueur : { client.Name } ({ client.SteamId }) [raison = { reason })." );
	}

	/// <summary>
	/// Se déclenche lorsqu'un joueur fait apparaître un prop.
	/// </summary>
	[Event( "OnPropSpawned" )]
	public static void OnPropSpawned( Client client, string modelName )
	{
		Logs.Add( $"Apparition d'un prop : { modelName } par { client.Name } ({ client.SteamId })." );
	}

	/// <summary>
	/// Se déclenche lorsqu'un joueur fait apparaître une entité.
	/// </summary>
	[Event( "OnEntitySpawned" )]
	public static void OnEntitySpawned( Client client, string className )
	{
		Logs.Add( $"Apparition d'une entité : { className } par { client.Name } ({ client.SteamId })." );
	}

	/// <summary>
	/// Se déclenche lorsqu'un joueur passe en noclip.
	/// </summary>
	[Event( "OnPlayerNoClip" )]
	public static void OnPlayerNoClip( Client client )
	{
		Logs.Add( $"Changement d'état du noclip de { client.Name } ({ client.SteamId })." );
	}

	/// <summary>
	/// Se déclenche lorsqu'un joueur est tué.
	/// </summary>
	[Event( "OnPlayerKilled" )]
	public static void OnPlayerKilled( Client client, Entity pawn )
	{
		if ( pawn.LastAttacker is Player attacker )
		{
			var target = attacker.GetClientOwner();

			Logs.Add( $"Mort de { client.Name } ({ client.SteamId }) par { target.Name }." );
		}
	}
}