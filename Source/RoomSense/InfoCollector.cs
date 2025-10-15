using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RimWorld;
using Verse;

namespace RoomSense;

public class InfoCollector
{
    private readonly MethodInfo roomLabelGetter;
    private bool infoChanged;
    private int nextUpdateTick;

    public InfoCollector()
    {
        RelevantRooms = new Dictionary<Room, RoomInfo>();
        MaxStatCount = 0;
        MaxStatSize = 0;

        var envInspector = GenTypes.GetTypeInAnyAssembly("EnvironmentInspectDrawer");
        roomLabelGetter =
            envInspector?.GetMethod("GetRoomRoleLabel", BindingFlags.Static | BindingFlags.NonPublic);
    }

    public Dictionary<Room, RoomInfo> RelevantRooms { get; }

    private int MaxStatCount { get; set; }

    private int MaxStatSize { get; set; }

    public bool IsValid()
    {
        return MaxStatCount > 0 && MaxStatSize > 0 && RelevantRooms.Count > 0;
    }

    public bool IsTimeToUpdateHeatMap()
    {
        if (!infoChanged)
        {
            return false;
        }

        infoChanged = false;
        return true;
    }

    public void Update(int updateDelay)
    {
        var tick = Find.TickManager.TicksGame;
        if (nextUpdateTick != 0 && tick < nextUpdateTick)
        {
            return;
        }

        nextUpdateTick = tick + updateDelay;
        infoChanged = true;
        RelevantRooms.Clear();

        var map = Find.CurrentMap;
        var listerBuildings = map.listerBuildings;
        // Room roles are defined by buildings, so only need to check rooms with buildings
        foreach (var building in listerBuildings.allBuildingsColonist)
        {
            var room = getRoomContainingBuildingIfRelevant(building, map);
            if (room == null)
            {
                continue;
            }

            if (RelevantRooms.ContainsKey(room))
            {
                continue;
            }

            var stats = getRoomStats(room);
            if (stats.Count == 0)
            {
                continue;
            }

            var roomInfo = new RoomInfo(
                stats,
                getPanelTopLeftCornerForRoom(room, map),
                stats.Max(s => s.MaxLevel),
                (string)roomLabelGetter?.Invoke(null, [room]) ?? room.Role.LabelCap
            );

            RelevantRooms[room] = roomInfo;
        }
    }

    public void Reset()
    {
        nextUpdateTick = 0;
        RelevantRooms.Clear();
    }

    private List<RoomStat> getRoomStats(Room room)
    {
        var stats = new List<RoomStat>();
        foreach (var statDef in DefDatabase<RoomStatDef>.AllDefsListForReading)
        {
            if (!isStatValidForRoom(statDef, room))
            {
                continue;
            }

            var stat = room.GetStat(statDef);
            var scoreStage = statDef.GetScoreStage(stat);
            var roomStat = new RoomStat
            {
                StatDef = statDef,
                CurrentLevel = statDef.GetScoreStageIndex(stat),
                MaxLevel = statDef.scoreStages.Count,
                RawCurrentLevel = statDef.ScoreToString(stat),
                ValueLabel = scoreStage != null ? scoreStage.label : string.Empty
            };

            if (roomStat.MaxLevel > MaxStatSize)
            {
                MaxStatSize = roomStat.MaxLevel;
            }

            stats.Add(roomStat);
        }

        if (stats.Count > MaxStatCount)
        {
            MaxStatCount = stats.Count;
        }

        return stats;
    }

    private static bool isStatValidForRoom(RoomStatDef statDef, Room room)
    {
        if (statDef.isHidden)
        {
            return false;
        }

        if (statDef == RoomStatDefOf.Cleanliness && room.Role.Worker is RoomRoleWorker_Kitchen)
        {
            return true;
        }

        return room.Role.IsStatRelated(statDef);
    }

    private static IntVec3 getPanelTopLeftCornerForRoom(Room room, Map map)
    {
        var bestCell = room.BorderCells.First();
        foreach (var cell in room.BorderCells)
        {
            if (cell.x < bestCell.x || cell.z > bestCell.z)
            {
                bestCell = cell;
            }
        }

        var possiblyBetterCell = bestCell;
        possiblyBetterCell.x++;
        possiblyBetterCell.z--;
        if (possiblyBetterCell.GetRoom(map) == room)
        {
            bestCell = possiblyBetterCell;
        }

        return bestCell;
    }

    // Filter for indoor rooms with a role
    private static Room getRoomContainingBuildingIfRelevant(Building building, Map map)
    {
        if (building.Faction != Faction.OfPlayer)
        {
            return null;
        }

        if (building.Position.Fogged(map))
        {
            return null;
        }

        var room = building.Position.GetRoom(map);
        if (room == null || room.PsychologicallyOutdoors)
        {
            return null;
        }

        return room.Role == RoomRoleDefOf.None ? null : room;
    }
}