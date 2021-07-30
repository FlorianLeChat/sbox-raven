using Sandbox;
using System.Collections.Generic;

[Library( "sandbox", Title = "Raven" )]
partial class SandboxGame : Game
{
	private static readonly List<string> commands = new()
	{
		// Affiche certaines informations sur la position du joueur.
		"cl_showpos 1",
		// Affiche certaines statistiques sur le nombre d'images par seconde.
		"cl_showfps 4",
		// Affiche les erreurs de prédictions en provenance du moteur.
		"cl_showerror 1",
		// Active certaines fonctionnalités de développement.
		"developer 1"
	};

	public SandboxGame()
	{
		if ( IsServer )
		{
			// Create the HUD
			_ = new SandboxHud();
		}
	}

	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );

		var player = new SandboxPlayer();
		player.Respawn();

		client.Pawn = player;

		// Permet d'activer les commandes personnalisées.
		foreach ( var command in commands )
		{
			client.SendCommandToClient( command );
		}

		// Raven-side
		Event.Run( "OnClientJoined", client );
	}

	public override void ClientDisconnect( Client client, NetworkDisconnectionReason reason )
	{
		base.ClientDisconnect( client, reason );

		// Raven-side
		Event.Run( "OnClientDisconnect", client, reason);
	}

	public override void OnKilled( Entity pawn )
	{
		base.OnKilled( pawn );

		// Raven-side
		Event.Run( "OnPlayerKilled", pawn );
	}

	public override void DoPlayerNoclip( Client client )
	{
		if ( client.Pawn is Player basePlayer )
		{
			if ( basePlayer.DevController is NoclipController )
			{
				basePlayer.DevController = null;
			}
			else
			{
				basePlayer.DevController = new NoclipController();
			}
		}

		// Raven-side
		Event.Run( "OnPlayerNoClip", client );
	}

	public override void DoPlayerSuicide( Client client )
	{
		// Can't suicide.
	}
	public override void PostLevelLoaded()
	{
		base.PostLevelLoaded();

		// Raven-side
		Event.Run( "InitPostEntity" );
	}

	[ServerCmd( "spawn" )]
	public static void Spawn( string modelName )
	{
		var caller = ConsoleSystem.Caller;

		if ( caller == null )
			return;

		var owner = caller.Pawn;

		if ( owner == null )
			return;

		var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 500 )
			.UseHitboxes()
			.Ignore( owner )
			.Size( 2 )
			.Run();

		var ent = new Prop();
		ent.Position = tr.EndPos;
		ent.Rotation = Rotation.From( new Angles( 0, owner.EyeRot.Angles().yaw, 0 ) ) * Rotation.FromAxis( Vector3.Up, 180 );
		ent.SetModel( modelName );

		if ( ent.PhysicsBody != null && ent.PhysicsGroup.BodyCount == 1 )
		{
			var p = ent.PhysicsBody.FindClosestPoint( tr.EndPos );

			var delta = p - tr.EndPos;
			ent.PhysicsBody.Position -= delta;
		}

		// Raven-side
		Event.Run( "OnPropSpawned", caller, modelName);
	}

	[ServerCmd( "spawn_entity" )]
	public static void SpawnEntity( string entName )
	{
		var caller = ConsoleSystem.Caller;

		if ( caller == null )
			return;

		var owner = caller.Pawn;

		if ( owner == null )
			return;

		var attribute = Library.GetAttribute( entName );

		if ( attribute == null || !attribute.Spawnable )
			return;

		var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 200 )
			.UseHitboxes()
			.Ignore( owner )
			.Size( 2 )
			.Run();

		var ent = Library.Create<Entity>( entName );
		if ( ent is BaseCarriable && owner.Inventory != null )
		{
			if ( owner.Inventory.Add( ent, true ) )
				return;
		}

		ent.Position = tr.EndPos;
		ent.Rotation = Rotation.From( new Angles( 0, owner.EyeRot.Angles().yaw, 0 ) );

		// Raven-side
		Event.Run( "OnEntitySpawned", caller, entName );
	}
}
