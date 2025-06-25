global using Terraria.ModLoader;
global using Terraria;
global using Microsoft.Xna.Framework;
using ExampleSpiritCrossmod.Content;
using SpiritReforged.Content.Savanna.Tiles.AcaciaTree;

namespace ExampleSpiritCrossmod;

public class ExampleSpiritCrossmod : Mod
{
    public static Mod Reforged => ModLoader.GetMod("SpiritReforged");

    public override void Load()
    {
        // Here we add in the Irradiated Savanna Tree.
        // Note that this, alongside all other RegisterConversion Calls, can be run in any Load, and for RegisterConversion calls, SetStaticDefaults methods.
        // As AddSavannaTree adds content, it can only be used in Load.
        Reforged.Call("AddSavannaTree", "ExampleSpiritCrossMod/Content/", "IrradiatedAcaciaTree", () => new int[] { ModContent.TileType<IrradiatedSavannaGrass>() }, this);

        // To work, RegisterConversionSet requires a set name, a conversion OR tile ID, and the tile ID to convert to.
        // Whether the set requires a conversion or tile ID depends on the nature of the tile. Tiles that convert based on anchors will use a tile ID. Otherwise, a conversion ID will be used.
        // If you're unsure which to use, simply consult Spirit's source code on GitHub
        Reforged.Call("RegisterConversionSet", nameof(AcaciaTree), ModContent.TileType<IrradiatedSavannaGrass>(), Find<ModTile>("IrradiatedAcaciaTree").Type);
    }
}
