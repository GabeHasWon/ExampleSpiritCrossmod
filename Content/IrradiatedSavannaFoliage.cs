using SpiritReforged.Common.TileCommon.Conversion;
using SpiritReforged.Content.Savanna.Tiles;
using Terraria.ID;
using Terraria.ObjectData;

namespace ExampleSpiritCrossmod.Content;

/// <summary> A converted Savanna Foliage tile. Behaves the same as default savanna foliage. </summary>
public class IrradiatedSavannaFoliage : SavannaFoliage
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        // Register a tile conversion using Spirit conversion sets.
        // Because Savanna Foliage relies on tile anchors for conversion behaviour, add IrradiatedSavannaGrass in place of the usual biome conversion ID.
        // Notice the predefined key for this set, "Plants" allows common plants to convert interchangeably. Naturally, this is the key that Savanna Foliage uses.
        ExampleSpiritCrossmod.Reforged.Call("RegisterConversionSet", ConversionHandler.Plants, ModContent.TileType<IrradiatedSavannaGrass>(), Type);
    }

    // PreAddObjectData is a helper method implemented by some Spirit tiles.
    public override void PreAddObjectData()
    {
        base.PreAddObjectData();

        TileObjectData.newTile.AnchorValidTiles = [ModContent.TileType<IrradiatedSavannaGrass>()];

        AddMapEntry(Color.Yellow);
        DustType = DustID.AncientLight;
    }
}
