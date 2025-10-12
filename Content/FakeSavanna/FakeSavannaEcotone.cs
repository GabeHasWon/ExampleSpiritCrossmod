using SpiritReforged.Common.WorldGeneration.Ecotones;
using SpiritReforged.Content.Savanna.Tiles;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace ExampleSpiritCrossmod.Content.FakeSavanna;

internal class FakeSavannaEcotone : EcotoneBase
{
    public override void AddTasks(List<GenPass> tasks, List<EcotoneSurfaceMapping.EcotoneEntry> entries)
    {
        int pyramidIndex = tasks.FindIndex(x => x.Name == "Surface Ore and Stone");

        if (pyramidIndex == -1)
            return;

        tasks.Insert(pyramidIndex + 2, new PassLegacy("False Savanna", BaseGeneration));
    }

    private static bool CanGenerate(out (int, int) bounds)
    {
        const int offX = EcotoneSurfaceMapping.TransitionLength + 1; //Removes forest patches on the left side
        bounds = (0, 0);

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

        progress.Message = "Wow this is real";

        int startX = bounds.Item1;
        int endX = bounds.Item2;
        short startY = EcotoneSurfaceMapping.TotalSurfaceY[(short)startX];
        short endY = EcotoneSurfaceMapping.TotalSurfaceY[(short)endX];
        int lowY = Math.Max(startY, endY);

        for (int x = startX; x < endX; ++x)
        {
            for (int y = lowY + 400; y > Main.worldSurface * 0.45f; --y)
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
