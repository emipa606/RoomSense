using UnityEngine;
using Verse;

namespace RoomSense;

[StaticConstructorOnStartup]
public class Resources
{
    public static readonly Texture2D Impressiveness = ContentFinder<Texture2D>.Get("Impressiveness");
    public static readonly Texture2D Wealth = ContentFinder<Texture2D>.Get("Wealth");
    public static readonly Texture2D Space = ContentFinder<Texture2D>.Get("Space");
    public static readonly Texture2D Beauty = ContentFinder<Texture2D>.Get("Beauty");
    public static readonly Texture2D Cleanliness = ContentFinder<Texture2D>.Get("Cleanliness");
    public static readonly Texture2D GraphToggle = ContentFinder<Texture2D>.Get("GraphToggle");
}