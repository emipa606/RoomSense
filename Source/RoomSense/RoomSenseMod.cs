using System.Reflection;
using HarmonyLib;
using Mlie;
using UnityEngine;
using Verse;

namespace RoomSense;

[StaticConstructorOnStartup]
internal class RoomSenseMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static RoomSenseMod instance;

    private static string currentVersion;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public RoomSenseMod(ModContentPack content) : base(content)
    {
        instance = this;
        Settings = GetSettings<RoomSenseSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
        new Harmony("Falconne.RoomSense").PatchAll(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal RoomSenseSettings Settings { get; }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Room Sense";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        listingStandard.CheckboxLabeled("FALCRS.ShowStatMeters".Translate(), ref Settings.ShowStatMeters,
            "FALCRS.ShowStatMetersDesc".Translate());
        listingStandard.CheckboxLabeled("FALCRS.ShowHeatMap".Translate(), ref Settings.ShowHeatMap,
            "FALCRS.ShowHeatMapDesc".Translate());
        Settings.GraphOpacity = listingStandard.SliderLabeled(
            "FALCRS.GraphOpacity".Translate(Settings.GraphOpacity.ToStringPercent()), Settings.GraphOpacity, 0.01f, 1f,
            tooltip: "FALCRS.GraphOpacityDesc".Translate());
        Settings.HeatMapOpacity = listingStandard.SliderLabeled(
            "FALCRS.HeatMapOpacity".Translate(Settings.HeatMapOpacity.ToStringPercent()), Settings.HeatMapOpacity,
            0.01f, 1f,
            tooltip: "FALCRS.HeatMapOpacityDesc".Translate());
        Settings.UpdateDelay = (int)listingStandard.SliderLabeled(
            "FALCRS.UpdateDelay".Translate(Settings.UpdateDelay), Settings.UpdateDelay, 1f, 999f,
            tooltip: "FALCRS.UpdateDelayDesc".Translate());
        listingStandard.CheckboxLabeled("FALCRS.ShowRoomLabels".Translate(), ref Settings.ShowRoomLabels,
            "FALCRS.ShowRoomLabelsDesc".Translate());

        if (listingStandard.ButtonTextLabeledPct("FALCRS.ResetSettings".Translate(), "FALCRS.Reset".Translate(), 0.7f))
        {
            Settings.Reset();
        }

        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("FALCRS.CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}