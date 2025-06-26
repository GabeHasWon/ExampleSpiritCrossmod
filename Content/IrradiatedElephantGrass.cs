using SpiritReforged.Common.TileCommon;
using SpiritReforged.Content.Savanna.Tiles;
using Terraria.ID;
using Terraria.ObjectData;

namespace ExampleSpiritCrossmod.Content;

/// <summary>
/// A converted Elephant Grass tile. Behaves the same as the default tile.<br/>
/// Note the <see cref="DrawOrderAttribute"/> applied to this class. This is an internal Reforged tool we use to draw it over players.
/// </summary>
[DrawOrder(DrawOrderAttribute.Layer.NonSolid, DrawOrderAttribute.Layer.OverPlayers)]
public class IrradiatedElephantGrass : ElephantGrass
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        // Register a tile conversion using Spirit conversion sets.
        // Because Elephant Grass relies on tile anchors for conversion behaviour, add IrradiatedSavannaGrass in place of the usual biome conversion ID.
        ExampleSpiritCrossmod.Reforged.Call("RegisterConversionSet", nameof(ElephantGrass), ModContent.TileType<IrradiatedSavannaGrass>(), Type);
    }

    // PreAddObjectData is a helper method implemented by some Spirit tiles.
    public override void PreAddObjectData()
    {
        AddMapEntry(new(109, 106, 174));

        TileObjectData.newTile.AnchorValidTiles = [ModContent.TileType<IrradiatedSavannaGrass>()]; // Make sure to set your anchor properly!
        DustType = DustID.AncientLight;
    }
}