using Terraria.DataStructures;
using Terraria.ID;

namespace ExampleSpiritCrossmod.Content;

internal class IrradiationTool : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.StaffofRegrowth);
        Item.Size = new Vector2(14);
        Item.rare = ItemRarityID.Blue;
        Item.damage = 0;
        Item.noMelee = true;
    }

    public override bool? UseItem(Player player)
    {
        if (Main.myPlayer == player.whoAmI)
        {
            Point16 pos = Main.MouseWorld.ToTileCoordinates16();
            WorldGen.Convert(pos.X, pos.Y, IrradiatedConversion.ConversionType, 2);
        }

        return true;
    }
}
