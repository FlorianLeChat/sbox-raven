using Sandbox;

partial class SandboxGame : Game
{
	/// <summary>
	///
	/// </summary>
	[Event.Hotload]
	public static void OnHotload()
	{
		Raven.Logs.Add( "Le serveur semble avoir procéder à un rechargement des fichiers." );
	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnClientJoined" )]
	public static void OnClientJoined( Client client )
	{
/*		Positions = FileSystem.Data.ReadJsonOrDefault<Dictionary<ulong, int>>( "data/rts/ratings.json" ) ?? new();

		Log.Info( Positions );*/
	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnPropSpawned" )]
	public static void OnPropSpawned( string modelName )
	{

	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnEntitySpawned" )]
	public static void OnEntitySpawned( string entName )
	{

	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnPlayerSpawned" )]
	public static void OnPlayerSpawned()
	{

	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnPlayerNoClip" )]
	public static void OnPlayerNoClip( Client client )
	{
		// player.SendCommandToClient( "say \"Changement d'état du noclip\"" );
	}

	/// <summary>
	///
	/// </summary>
	[Event( "OnPlayerTakeDamage" )]
	public static void OnPlayerTakeDamage( DamageInfo info )
	{

	}
}
