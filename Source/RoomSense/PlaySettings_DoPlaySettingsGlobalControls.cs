using HarmonyLib;
using RimWorld;
using Verse;

namespace RoomSense;

[HarmonyPatch(typeof(PlaySettings), "DoMapControls")]
public static class PlaySettings_DoPlaySettingsGlobalControls
{
    private static void Postfix(WidgetRow row)
    {
        var component = Current.Game.GetComponent<RoomSense_GameComponent>();
        if (component == null)
        {
            return;
        }

        row?.ToggleableIcon(ref component.ShowOverlay, Resources.GraphToggle,
            "FALCRS.GraphToggle".Translate(), SoundDefOf.Mouseover_ButtonToggle);
    }
}