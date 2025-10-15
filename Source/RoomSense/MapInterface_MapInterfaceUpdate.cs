using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace RoomSense;

[HarmonyPatch(typeof(MapInterface), nameof(MapInterface.MapInterfaceUpdate))]
public static class MapInterface_MapInterfaceUpdate
{
    private static void Postfix()
    {
        if (Find.CurrentMap == null || WorldRendererUtility.WorldRendered)
        {
            return;
        }

        Current.Game.GetComponent<RoomSense_GameComponent>().UpdateOverlays();
    }
}