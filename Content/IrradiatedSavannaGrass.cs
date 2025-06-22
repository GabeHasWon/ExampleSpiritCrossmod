using SpiritReforged.Common.TileCommon;
using SpiritReforged.Common;
using SpiritReforged.Content.Savanna.Tiles;

namespace ExampleSpiritCrossmod.Content;

internal class IrradiatedSavannaGrass : SavannaGrass
{
    protected override Color MapColor => new(212, 255, 86);

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        SpiritSets.Mowable[Type] = -1; // Savanna Grass can be mowed, but this can't - remove mowability

        ExampleSpiritCrossmod.Reforged.Call("RegisterConversionTile", IrradiatedConversion.ConversionType, ModContent.TileType<SavannaGrass>(), Type);
    }

    public override void RandomUpdate(int i, int j)
    {
        GrowTiles(i, j);

        // This automatically converts tiles in any cardinal direction randomly, much like a normal Corrupt or Crimson tile would.
        SpreadHelper.ConversionSpread(i, j, IrradiatedConversion.ConversionType);
    }

    protected override void GrowTiles(int i, int j)
    {
        var above = Framing.GetTileSafely(i, j - 1);

        if (!above.HasTile && above.LiquidAmount < 80)
        {
            int grassChance = GrassAny() ? 6 : 35;

            if (Main.rand.NextBool(grassChance)) // Grow either foliage tile randomly - Placer is an internal tool that makes it more convenient to grow/place stuff
                Placer.PlaceTile<IrradiatedElephantGrass>(i, j - 1, Main.rand.Next(5, 8)).Send();
            else if (Main.rand.NextBool(15))
                Placer.PlaceTile<IrradiatedSavannaFoliage>(i, j - 1).Send();
        }

        bool GrassAny()
        {
            int type = ModContent.TileType<IrradiatedElephantGrass>();
            return Framing.GetTileSafely(i - 1, j - 1).TileType == type || Framing.GetTileSafely(i + 1, j - 1).TileType == type;
        }
    }
}
