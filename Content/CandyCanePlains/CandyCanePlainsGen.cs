using SpiritReforged.Common.WorldGeneration.Ecotones;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace ExampleSpiritCrossmod.Content.CandyCanePlains;

/// <summary>
/// Extremely simple custom biome. Used solely to have a custom <see cref="EcotoneEdgeDefinition"/>.
/// </summary>
internal class CandyCanePlainsGen : ModSystem
{
    // Adds a custom edge definition so the ecotone mapper can find it properly.
    // This is the only functional part of this file, aside from placing the associated tiles in-world.
    //
    // For a deeper explanation of EcotoneEdgeDefinition, we have 3 parameters:
    // int displayId: Solely for debugging
    // string name: Name for internal use
    // int[] validIds: The valid IDs to scan for. This only needs to be what appears in-world, on the surface. 
    public override void SetStaticDefaults() 
        => EcotoneEdgeDefinitions.AddEdgeDefinition(new EcotoneEdgeDefinition(TileID.CandyCaneBlock, "CandyCanePlains", [TileID.CandyCaneBlock, TileID.GreenCandyCaneBlock]));

    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        int pyramidIndex = tasks.FindIndex(x => x.Name == "Pyramids");

        if (pyramidIndex == -1)
            return;

        tasks.Insert(pyramidIndex, new PassLegacy("Candy Cane Plains", BaseGeneration));
    }

    public static void BaseGeneration(GenerationProgress progress, GameConfiguration config)
    {
        int left = WorldGen.oceanDistance + 200;
        int right = left + 100;

        for (int i = left; i < right; ++i)
        {
            for (int j = (int)(Main.worldSurface * .35f); j < Main.worldSurface; ++j)
            {
                Tile tile = Main.tile[i, j];

                if (tile.HasTile)
                    tile.TileType = WorldGen.genRand.NextBool() ? TileID.CandyCaneBlock : TileID.GreenCandyCaneBlock;
            }
        }
    }
}
