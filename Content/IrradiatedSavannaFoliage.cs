using SpiritReforged.Content.Savanna.Tiles;
using Terraria.ID;

namespace ExampleSpiritCrossmod.Content;

/// <summary>
/// A converted Savanna Foliage tile. Behaves the same as default savanna foliage.
/// </summary>
public class IrradiatedSavannaFoliage : SavannaFoliage
{
    protected override int AnchorTile => ModContent.TileType<IrradiatedSavannaGrass>();
    protected override Color MapColor => Color.Yellow;
    protected override int Dust => DustID.AncientLight;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        // Register the single tile conversion
        ExampleSpiritCrossmod.Reforged.Call("RegisterConversionTile", IrradiatedConversion.ConversionType, ModContent.TileType<SavannaFoliage>(), Type);
    }
}
