using Sandbox;
using System.Linq;
// using System.Timers;
using System.Collections.Generic;

namespace Raven
{
	public partial class Characters : Game
	{
		public const int SaveDataInterval = 300 * 1000;
		// public static Timer SaveDataTimer { get; set; } = new();
		public static Dictionary<ulong, Dictionary<string, string>> ServerCache { get; set; } = new();

		/// <summary>
		/// Permet de créer un timer continu pour sauvegarder périodiquement les données des joueurs.
		/// </summary>
		[Event( "OnServerStartup" )]
		public static async void OnServerStartup()
		{
			// https://github.com/Facepunch/sbox-issues/issues/541
			/*SaveDataTimer.Elapsed += (source, e) =>
			{
				var players = Client.All;

				foreach ( var player in players )
				{
					SaveData( player.SteamId );
				}

				Logs.Add( $"Sauvegarde de {players.Count} joueur(s) effectuée." );
			};
			SaveDataTimer.Interval = SaveDataInterval;
			SaveDataTimer.Enabled = true;*/

			while ( true )
			{
				await GameTask.Delay( SaveDataInterval );

				var players = Client.All;

				foreach ( var player in players )
				{
					SaveData( player.SteamId );
				}

				Logs.Add( $"Sauvegarde de {players.Count} joueur(s) effectuée." );
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
				ServerCache[ client.SteamId ][ key ] = value;
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

		/// <summary>
		/// Permet de charger ou de sauvegarder les données d'un joueur.
		/// </summary>
		private static void LoadData( ulong steamID )
		{
			ServerCache[ steamID ] = FileSystem.Data.ReadJsonOrDefault( $"raven/characters/{ steamID }.json", new Dictionary<string, string> {} );
		}

		private static void SaveData( ulong steamID )
		{
			if ( ServerCache.TryGetValue( steamID, out var cache ) )
			{
				FileSystem.Data.WriteJson( $"raven/characters/{ steamID }.json", cache );
			}
		}
	}
}