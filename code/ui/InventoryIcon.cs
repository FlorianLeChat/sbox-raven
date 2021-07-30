using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class InventoryIcon : Panel
{
	public Entity TargetEnt;
	public Label Label;
	public Label Number;
	public Label Magazine;

	public InventoryIcon( int index, Panel parent )
	{
		Parent = parent;
		Label = Add.Label( "vide", "item-name" );
		Number = Add.Label( $"{index}", "slot-number" );
		Magazine = Add.Label( "0 / 0", "magazine-number" );
	}

	public void Clear()
	{
		Label.Text = "";
		SetClass( "active", false );
	}
}
