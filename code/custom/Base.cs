using Sandbox;

partial class SandboxGame : Game
{
	/// <summary>
	///
	/// </summary>
	[Event.Hotload]
	public void OnHotload()
	{
		if ( IsServer )
			Raven.Logs.Add( "Le serveur semble avoir procéder à un rechargement des fichiers." );
	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnClientJoined" )]
	public static void OnClientJoined( Client client )
	{
		Raven.Logs.Add( $"Apparition du joueur : {client.Name} ({client.SteamId})." );
	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnPropSpawned" )]
	public static void OnPropSpawned( Client client, string modelName )
	{
		Raven.Logs.Add( $"Apparition d'un prop : {modelName} par {client.Name} ({client.SteamId})." );
	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnEntitySpawned" )]
	public static void OnEntitySpawned(Client client, string className )
	{
		Raven.Logs.Add( $"Apparition d'une entité : {className} par {client.Name} ({client.SteamId})." );
	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnPlayerNoClip" )]
	public static void OnPlayerNoClip( Client client )
	{
		Raven.Logs.Add( $"Changement d'état du noclip de {client.Name} ({client.SteamId})." );
	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnPlayerKilled" )]
	public static void OnPlayerKilled( Entity pawn )
	{
		Raven.Logs.Add( $"Mort de {pawn} par {pawn.LastAttacker}." );
	}
}
