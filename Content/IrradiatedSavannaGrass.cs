using SpiritReforged.Common.TileCommon;
using SpiritReforged.Common;
using SpiritReforged.Content.Savanna.Tiles;
using Terraria.ID;

namespace ExampleSpiritCrossmod.Content;

internal class IrradiatedSavannaGrass : SavannaGrass
{
    protected override Color MapColor => new(212, 255, 86);

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        SpiritSets.Mowable[Type] = -1;

        ExampleSpiritCrossmod.Reforged.Call("RegisterConversionTile", IrradiatedConversion.ConversionType, ModContent.TileType<SavannaGrass>(), Type);
    }

    public override void RandomUpdate(int i, int j)
    {
        base.RandomUpdate(i, j);

        //if (SpreadHelper.Spread(i, j, Type, 4, TileID.Grass) && Main.netMode != NetmodeID.SinglePlayer)
        //    NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None); // Try spread normal grass
    }

    protected override void GrowTiles(int i, int j)
    {
        var above = Framing.GetTileSafely(i, j - 1);

        //if (!above.HasTile && above.LiquidAmount < 80)
        //{
        //    int grassChance = GrassAny() ? 6 : 35;

        //    if (Main.rand.NextBool(grassChance))
        //        Placer.PlaceTile<ElephantGrassCrimson>(i, j - 1, Main.rand.Next(5, 8)).Send();
        //    else if (Main.rand.NextBool(15))
        //        Placer.PlaceTile<SavannaFoliageCrimson>(i, j - 1).Send();
        //}

        bool GrassAny()
        {
            int type = ModContent.TileType<ElephantGrassCrimson>();
            return Framing.GetTileSafely(i - 1, j - 1).TileType == type || Framing.GetTileSafely(i + 1, j - 1).TileType == type;
        }
    }
}
