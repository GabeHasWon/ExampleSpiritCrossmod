using SpiritReforged.Common.WorldGeneration.Ecotones;
using SpiritReforged.Content.Savanna.Tiles;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace ExampleSpiritCrossmod.Content.FakeSavanna;

/// <summary>
/// Defines a custom ecotone's logic.
/// </summary>
internal class FakeSavannaEcotone : EcotoneBase
{
    public override void AddTasks(List<GenPass> tasks, List<EcotoneSurfaceMapping.EcotoneEntry> entries)
    {
        int pyramidIndex = tasks.FindIndex(x => x.Name == "Surface Ore and Stone");

        if (pyramidIndex == -1)
            return;

        // This is always placed in the generation order, but may not generate, per CanGenerate below.
        // Note that this is pretty late in generation, and if you want something more like Reforged's 
        tasks.Insert(pyramidIndex + 2, new PassLegacy("False Savanna", BaseGeneration));
    }

    private static bool CanGenerate(out (int, int) bounds)
    {
        // Removes forest patches on the left side
        const int offX = EcotoneSurfaceMapping.TransitionLength + 1;
        bounds = (0, 0);

        // If there's a valid place to put this ecotone - here, between the Ocean and the custom "CandyCanePlains" - return the bounds
        if (EcotoneSurfaceMapping.FindWhere(x => x.SurroundedBy("Ocean", "CandyCanePlains") && EcotoneSurfaceMapping.OnSurface(x)) is EcotoneSurfaceMapping.EcotoneEntry entry)
        {
            bounds = (entry.Start.X - offX, entry.End.X);
            return true;
        }

        return false;
    }

    private static void BaseGeneration(GenerationProgress progress, GameConfiguration configuration)
    {
        if (!CanGenerate(out var bounds))
            return;

        // This code means nothing and looks terrible.
        // Do whatever here that looks better.
        // You should use EcotoneSurfaceMapping.TotalSurfaceY to guage the default height of the world in the area;
        // this is what the Savanna uses to ramp up/down.

        int startX = bounds.Item1;
        int endX = bounds.Item2;
        short startY = EcotoneSurfaceMapping.TotalSurfaceY[(short)startX];
        short endY = EcotoneSurfaceMapping.TotalSurfaceY[(short)endX];
        int lowY = Math.Max(startY, endY);

        for (int x = startX; x < endX; ++x)
        {
            for (int y = lowY + 100; y > Main.worldSurface * 0.45f; --y)
            {
                Tile tile = Main.tile[x, y];

                if (TileID.Sets.Grass[tile.TileType])
                    tile.TileType = (ushort)ModContent.TileType<SavannaGrass>();
                else if (tile.TileType == TileID.Stone)
                    tile.TileType = TileID.SandstoneBrick;
                else if (tile.TileType == TileID.ClayBlock)
                    tile.TileType = TileID.Sand;
                else
                    tile.TileType = (ushort)ModContent.TileType<SavannaDirt>();
            }
        }
    }
}
