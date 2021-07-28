using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Armor : Panel
{
	public Label Label;

	public Armor()
	{
		Label = Add.Label( "100", "value" );
	}

	public override void Tick()
	{
		var player = Local.Pawn;
		if ( player == null ) return;

		// NYI
		Label.Text = "0%";
	}
}
