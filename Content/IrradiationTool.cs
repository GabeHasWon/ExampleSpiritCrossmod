using Terraria.DataStructures;
using Terraria.ID;

namespace ExampleSpiritCrossmod.Content;

/// <summary>
/// An absurdly simple tool to test conversion. Replace this with whatever form of conversion you have - natural, powder, clentaminator.<br/>
/// All of it should work fundamentally the same as long as you use <see cref="WorldGen.Convert(int, int, int, int)"/>.
/// </summary>
internal class IrradiationTool : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.StaffofRegrowth);
        Item.Size = new Vector2(14);
        Item.rare = ItemRarityID.Blue;
        Item.damage = 0;
        Item.noMelee = true;
        Item.createTile = -1;
    }

    public override bool? UseItem(Player player)
    {
        if (Main.myPlayer == player.whoAmI)
        {
            Point16 pos = Main.MouseWorld.ToTileCoordinates16();
            WorldGen.Convert(pos.X, pos.Y, IrradiatedConversion.ConversionType, 2); // Converts a 5x5 area to our custom Irradiated conversion
        }

        return true;
    }
}
