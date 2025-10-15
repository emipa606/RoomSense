using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace RoomSense;

public class RoomInfo(List<RoomStat> stats, IntVec3 panelCellTopLeft, int maxStatSize, string roomName)
{
    public readonly int MaxStatSize = maxStatSize;
    public readonly IntVec3 PanelCellTopLeft = panelCellTopLeft;
    public readonly string RoomName = roomName;
    public readonly List<RoomStat> Stats = stats;

    private RoomStat primaryStat;

    public RoomStat GetPrimaryStat()
    {
        if (primaryStat != null)
        {
            return primaryStat;
        }

        if (Stats.Count == 1)
        {
            primaryStat = Stats.First();
            return primaryStat;
        }

        primaryStat = Stats.FirstOrDefault(s => s.StatDef == RoomStatDefOf.Impressiveness);
        if (primaryStat != null)
        {
            return primaryStat;
        }

        // No obvious primary stat.
        // Create an average of the stats to serve as primary, with a resolution of 12.
        var averageOfStatFractions = Stats.Average(s => (float)s.CurrentLevel / s.MaxLevel);
        primaryStat = new RoomStat
        {
            CurrentLevel = (int)(averageOfStatFractions * 12),
            MaxLevel = 12
        };

        return primaryStat;
    }
}