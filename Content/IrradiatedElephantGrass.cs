using SpiritReforged.Common.TileCommon;
using SpiritReforged.Content.Savanna.Tiles;
using Terraria.ID;
using Terraria.ObjectData;

namespace ExampleSpiritCrossmod.Content;

/// <summary>
/// A converted Savanna Foliage tile. Behaves the same as default savanna foliage.<br/>
/// Note the <see cref="DrawOrderAttribute"/> applied to this class. This is an internal Reforged tool we use to draw it over players.
/// </summary>
[DrawOrder(DrawOrderAttribute.Layer.NonSolid, DrawOrderAttribute.Layer.OverPlayers)]
public class IrradiatedElephantGrass : ElephantGrass
{
    protected override Color SubColor => Color.YellowGreen;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
    
        // Registers the appropriate conversion
        ExampleSpiritCrossmod.Reforged.Call("RegisterConversionTile", IrradiatedConversion.ConversionType, ModContent.TileType<ElephantGrass>(), Type);
    }

    public override void PreAddObjectData()
    {
        AddMapEntry(new(109, 106, 174));

        TileObjectData.newTile.AnchorValidTiles = [ModContent.TileType<IrradiatedSavannaGrass>()]; // Make sure to set your anchor properly!
        DustType = DustID.AncientLight;
    }
}