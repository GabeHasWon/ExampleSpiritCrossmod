using ExampleSpiritCrossmod.Content;
using SpiritReforged.Content.Savanna.Tiles;
using System.Collections.Generic;
using Terraria.ID;

namespace ExampleSpiritCrossmod;

public class IrradiatedConversion : ModBiomeConversion
{
    public static int ConversionType { get; private set; }

    private static readonly Dictionary<int, int> Conversions = new()
    {
        { ModContent.TileType<SavannaGrass>(), ModContent.TileType<IrradiatedSavannaGrass>() },
        //{ TileID.GolfGrass, ModContent.TileType<StargrassMowed>() },
        //{ TileID.Plants, ModContent.TileType<StargrassFlowers>() },
        //{ TileID.Plants2, ModContent.TileType<StargrassFlowers>() }
    };

    public override void SetStaticDefaults()
    {
        ConversionType = Type;
        SpiritReforged.Common.TileCommon.Conversion.ConversionHelper.RegisterConversions([.. Conversions.Keys], ConversionType, ConvertTiles);

        //TileLoader.RegisterConversion(TileID.Sunflower, ConversionType, (i, j, type, conversionType) =>
        //{
        //    ushort newType = (ushort)ModContent.TileType<Starflower>();
        //    return conversionType == ConversionType && ConversionHelper.DoMultiConversion(i, j, newType);
        //});
    }

    private static bool ConvertTiles(int i, int j, int type, int conversionType)
    {
        if (Conversions.TryGetValue(type, out int newType))
            WorldGen.ConvertTile(i, j, newType);

        return false;
    }
}