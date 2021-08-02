using Sandbox;
using System.Collections.Generic;

namespace Raven
{
	public partial class Characters : Game
	{
		public const int SaveInterval = 300 * 1000;
		public const string SaveDirectory = "characters";
		public static Dictionary<ulong, Dictionary<string, string>> ServerCache { get; set; } = new();

		/// <summary>
		/// Permet de créer un timer continu pour sauvegarder périodiquement les données des joueurs.
		/// </summary>
		[Event( "OnServerStartup" )]
		public static async void OnServerStartup()
		{
			FileSystem.Data.CreateDirectory( "characters" );

			while ( true )
			{
				await GameTask.Delay( SaveInterval );

				OnServerShutdown();
			}
		}

		/// <summary>
		/// Permet d'ajouter une information de sauvegarde sur un joueur.
		/// </summary>
		public static void SetData( Client client, string key, string value )
		{
			if ( ServerCache.TryGetValue( client.SteamId, out var cache) )
			{
				cache[ key ] = value;
			}
			else
			{
				ServerCache[ client.SteamId ] = new Dictionary<string, string> { [ key ] = value };
			}
		}

		/// <summary>
		/// Permet de récupérer une information de sauvegarde d'un joueur.
		/// </summary>
		public static string GetData( Client client, string key, string fallback = "" )
		{
			if ( ServerCache.TryGetValue( client.SteamId, out var cache ) )
			{
				return cache[ key ];
			}
			else
			{
				return fallback;
			}
		}

		/// <summary>
		/// Permet de charger/vider le cache d'un joueur à sa (dé)connexion.
		/// </summary>
		[Event( "OnClientJoined" )]
		public static void OnClientJoined( Client client )
		{
			LoadData( client.SteamId );
		}

		[Event( "OnClientDisconnect" )]
		public static void OnClientDisconnect( Client client, NetworkDisconnectionReason _ )
		{
			var steamID = client.SteamId;

			SaveData( steamID );

			ServerCache.Remove( steamID );
		}

		[Event( "OnServerShutdown" )]
		public static void OnServerShutdown()
	{
			var players = Client.All;

			foreach ( var player in players )
			{
				SaveData( player.SteamId );
			}

			Logs.Add( $"Sauvegarde de {players.Count} joueur(s) effectuée." );
		}

		/// <summary>
		/// Permet de charger ou de sauvegarder les données d'un joueur.
		/// </summary>
		private static void LoadData( ulong steamID )
		{
			ServerCache[ steamID ] = FileSystem.Data.ReadJsonOrDefault( $"{ SaveDirectory }/{ steamID }.json", new Dictionary<string, string> {} );
		}

		private static void SaveData( ulong steamID )
		{
			if ( ServerCache.TryGetValue( steamID, out var cache ) )
			{
				FileSystem.Data.WriteJson( $"{ SaveDirectory }/{ steamID }.json", cache );
			}
		}
	}
}