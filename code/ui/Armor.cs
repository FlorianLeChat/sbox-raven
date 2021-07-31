using Raven;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Armor : Panel
{
	public Label Label;

	public Armor()
	{
		Add.Label("⚔", "icon");
		Label = Add.Label( "100", "value" );
	}

	public override void Tick()
	{
		var player = Local.Pawn;
		if ( player == null )
			return;

		// NYI
		Label.Text = "0%";

		SetClass( "open", ConVars.GetValue( "permanent_hud" ) == "true" || Input.Down( InputButton.Walk ) );
	}
}
