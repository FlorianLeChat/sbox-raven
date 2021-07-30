using Sandbox;

namespace Raven
{
	public partial class ConVars : Game
	{
		/// <summary>
		/// Énumérations pour les types de valeur d'une variable de console.
		/// </summary>
		public enum Type : byte
		{
			String = 0,
			Float = 1,
			Bool = 2,
			Int = 3
		}

		/// <summary>
		/// Permet de récupérer à la manière de Garry's Mod les informations d'une variable de console.
		/// </summary>
		public static string GetValue( string name, Type type = Type.String )
		{
			var value = ConsoleSystem.GetValue( "rcv_" + name, "" );

			switch (type)
			{
				// Note : le type de retour "dynamic" semble être bloqué pour des raisons de sécurité (?).
				/*case Type.Float:
					return value.ToFloat();

				case Type.Bool:
					return value.ToBool();

				case Type.Int:
					return value.ToInt();*/

				default:
					return value.ToLower();
			}
		}

		/// <summary>
		/// Certains éléments de l'interface doivent être tout le temps affichés ?
		/// </summary>
		[ConVar.ClientData( "rcv_permanent_hud" )]
		public static bool Permanent_Hud { get; set; } = false;
	}
}