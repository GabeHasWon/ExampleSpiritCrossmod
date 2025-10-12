global using Terraria.ModLoader;
global using Terraria;
global using Microsoft.Xna.Framework;
using SpiritReforged.Content.Savanna.Tiles.AcaciaTree;
using ExampleSpiritCrossmod.Content.IrradiatedSavanna;

namespace ExampleSpiritCrossmod;

public class ExampleSpiritCrossmod : Mod
{
    public static Mod Reforged => ModLoader.GetMod("SpiritReforged");

    public override void Load()
    {
        // Here we add in the Irradiated Savanna Tree.
        // Note that this, alongside all other RegisterConversion Calls, can be run in any Load, and for RegisterConversion calls, SetStaticDefaults methods.
        // As AddSavannaTree adds content, it can only be used in Load.
        Reforged.Call("AddSavannaTree", "ExampleSpiritCrossMod/Content/IrradiatedSavanna/", "IrradiatedAcaciaTree", () => new int[] { ModContent.TileType<IrradiatedSavannaGrass>() }, this);
        Reforged.Call("RegisterConversionTile", IrradiatedConversion.ConversionType, ModContent.TileType<AcaciaTree>(), Find<ModTile>("IrradiatedAcaciaTree").Type);
    }
}
