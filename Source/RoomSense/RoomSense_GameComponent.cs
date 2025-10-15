using RimWorld.Planet;
using Verse;

namespace RoomSense;

public class RoomSense_GameComponent : GameComponent
{
    private readonly GraphOverlay _graphOverlay = new();

    private readonly InfoCollector _infoCollector = new();

    private HeatMap _heatMap;

    public bool ShowOverlay;

    public RoomSense_GameComponent(Game game)
    {
    }

    public override void GameComponentOnGUI()
    {
        base.GameComponentOnGUI();

        if (Current.ProgramState != ProgramState.Playing || Find.CurrentMap == null
                                                         || WorldRendererUtility.WorldRendered)
        {
            return;
        }

        if (KeyBindingDef.Named("ToggleRoomSenseOverlay").KeyDownEvent)
        {
            ShowOverlay = !ShowOverlay;
        }

        if (!ShowOverlay)
        {
            return;
        }

        if (!RoomSenseMod.instance.Settings.ShowStatMeters)
        {
            return;
        }

        _graphOverlay.OnGUI(_infoCollector, RoomSenseMod.instance.Settings.GraphOpacity,
            RoomSenseMod.instance.Settings.ShowRoomLabels);
    }


    public void UpdateOverlays()
    {
        if (!ShowOverlay || !RoomSenseMod.instance.Settings.ShowStatMeters &&
            !RoomSenseMod.instance.Settings.ShowHeatMap)
        {
            _infoCollector.Reset();
            _heatMap?.Reset();
            return;
        }

        _infoCollector.Update(RoomSenseMod.instance.Settings.UpdateDelay);
        if (!RoomSenseMod.instance.Settings.ShowHeatMap)
        {
            return;
        }

        _heatMap ??= new HeatMap(_infoCollector);

        _heatMap.Update();
    }

    public override void FinalizeInit()
    {
        base.FinalizeInit();
        _infoCollector.Reset();
    }
}