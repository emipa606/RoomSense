using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace RoomSense
{
    public class RoomInfo
    {
        public readonly int MaxStatSize;
        public readonly IntVec3 PanelCellTopLeft;
        public readonly string RoomName;
        public readonly List<RoomStat> Stats;

        private RoomStat _primaryStat;

        public RoomInfo(List<RoomStat> stats, IntVec3 panelCellTopLeft, int maxStatSize, string roomName)
        {
            Stats = stats;
            PanelCellTopLeft = panelCellTopLeft;
            MaxStatSize = maxStatSize;
            RoomName = roomName;
        }

        public RoomStat GetPrimaryStat()
        {
            if (_primaryStat != null)
            {
                return _primaryStat;
            }

            if (Stats.Count == 1)
            {
                _primaryStat = Stats.First();
                return _primaryStat;
            }

            _primaryStat = Stats.FirstOrDefault(s => s.StatDef == RoomStatDefOf.Impressiveness);
            if (_primaryStat != null)
            {
                return _primaryStat;
            }

            // No obvious primary stat.
            // Create an average of the stats to serve as primary, with a resolution of 12.
            var averageOfStatFractions = Stats.Average(s => (float) s.CurrentLevel / s.MaxLevel);
            _primaryStat = new RoomStat
            {
                CurrentLevel = (int) (averageOfStatFractions * 12),
                MaxLevel = 12
            };

            return _primaryStat;
        }
    }
}