using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

using static Raven.ConVars;

public class Health : Panel
{
	public Label Label;

	public Health()
	{
		Add.Label( "❤", "icon" );
		Label = Add.Label( "100", "value" );
	}

	public override void Tick()
	{
		var player = Local.Pawn;
		if ( player == null )
			return;

		Label.Text = $"{player.Health.CeilToInt()} %";

		SetClass( "open", GetValue( "permanent_hud" ) == "true" || Input.Down( InputButton.Walk ) );
	}
}
