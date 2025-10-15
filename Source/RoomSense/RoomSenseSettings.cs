using Verse;

namespace RoomSense;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class RoomSenseSettings : ModSettings
{
    public float GraphOpacity = 1f;
    public float HeatMapOpacity = 0.44f;
    public bool ShowHeatMap = true;
    public bool ShowRoomLabels = true;
    public bool ShowStatMeters = true;
    public int UpdateDelay = 100;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref GraphOpacity, "GraphOpacity", 1f);
        Scribe_Values.Look(ref HeatMapOpacity, "HeatMapOpacity", 0.44f);
        Scribe_Values.Look(ref ShowStatMeters, "ShowStatMeters", true);
        Scribe_Values.Look(ref ShowHeatMap, "ShowHeatMap", true);
        Scribe_Values.Look(ref ShowRoomLabels, "ShowRoomLabels", true);
        Scribe_Values.Look(ref UpdateDelay, "UpdateDelay", 100);
    }

    public void Reset()
    {
        GraphOpacity = 1f;
        HeatMapOpacity = 0.44f;
        ShowStatMeters = true;
        ShowHeatMap = true;
        ShowRoomLabels = true;
        UpdateDelay = 100;
    }
}