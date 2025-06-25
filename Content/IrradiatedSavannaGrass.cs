using SpiritReforged.Common.TileCommon;
using SpiritReforged.Common;
using SpiritReforged.Content.Savanna.Tiles;

namespace ExampleSpiritCrossmod.Content;

internal class IrradiatedSavannaGrass : SavannaGrass
{
    // For convenience, Savanna Grass includes a property to set map entry color.
    protected override Color MapColor => new(212, 255, 86);

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        SpiritSets.Mowable[Type] = -1; // Savanna Grass can be mowed, but this can't - remove mowability.

        // Register a tile conversion using Spirit conversion sets.
        // Because Savanna Grass does NOT rely on anchors for conversion, we can include our custom conversion ID.
        ExampleSpiritCrossmod.Reforged.Call("RegisterConversionSet", nameof(SavannaGrass), IrradiatedConversion.ConversionType, Type);
    }

    public override void RandomUpdate(int i, int j)
    {
        GrowTiles(i, j);

        // This automatically converts tiles in any cardinal direction randomly, much like a normal Corrupt or Crimson tile would.
        WorldGen.SpreadInfectionToNearbyTile(i, j, IrradiatedConversion.ConversionType);
    }

    protected override void GrowTiles(int i, int j)
    {
        var above = Framing.GetTileSafely(i, j - 1);

        if (!above.HasTile && above.LiquidAmount < 80)
        {
            int grassChance = GrassAny() ? 6 : 35;

            if (Main.rand.NextBool(grassChance)) // Grow either foliage tile randomly - Placer is an internal tool that makes it more convenient to grow/place stuff.
                Placer.PlaceTile<IrradiatedElephantGrass>(i, j - 1, Main.rand.Next(5, 8)).Send(); //Appending Send is important because it syncs the tile change in multiplayer.
            else if (Main.rand.NextBool(15))
                Placer.PlaceTile<IrradiatedSavannaFoliage>(i, j - 1).Send();
        }

        bool GrassAny() // Checks if there is any Irradiated Elephant Grass surrounding these coordinates.
        {
            int type = ModContent.TileType<IrradiatedElephantGrass>();
            return Framing.GetTileSafely(i - 1, j - 1).TileType == type || Framing.GetTileSafely(i + 1, j - 1).TileType == type;
        }
    }
}
