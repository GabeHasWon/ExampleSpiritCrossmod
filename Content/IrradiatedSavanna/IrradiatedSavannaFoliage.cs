using SpiritReforged.Content.Savanna.Tiles;
using Terraria.ID;
using Terraria.ObjectData;

namespace ExampleSpiritCrossmod.Content.IrradiatedSavanna;

/// <summary>
/// A converted Savanna Foliage tile. Behaves the same as default savanna foliage.
/// </summary>
public class IrradiatedSavannaFoliage : SavannaFoliage
{
    public override void PreAddObjectData()
    {
        TileObjectData.newTile.AnchorValidTiles = [ModContent.TileType<IrradiatedSavannaGrass>()];

        DustType = DustID.HallowedPlants;
        AddMapEntry(new(78, 193, 227));
    }

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        // Register the single tile conversion
        ExampleSpiritCrossmod.Reforged.Call("RegisterConversionTile", IrradiatedConversion.ConversionType, ModContent.TileType<SavannaFoliage>(), Type);
    }
}
