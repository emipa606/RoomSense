# GitHub Copilot Instructions for Room Sense (Continued) Mod

## Mod Overview and Purpose
**Room Sense (Continued)** is an updated version of Falconnes mod that provides a faster and more intuitive way to view room statistics in the RimWorld game. It offers toggleable panels that display relevant room stats such as beauty, cleanliness, etc., using graphical meters. Additionally, it includes a colored heat map overlay that provides a visual representation of the primary stat for each room. The mod is designed to enhance the player's ability to quickly assess the condition of rooms and make informed decisions.

## Key Features and Systems
### Toggleable Panels
- **Graphical Meters:** Displays relevant room stats using segmented meters that correspond to different labels in the environment tool (e.g., "dull," "mediocre," "interesting").
- **Colored Overlay:** Provides a visual heat map that highlights the primary stat for each room. The color intensity depends on the stat's value.
- **Mouse-Over Details:** Allows players to mouse over individual panels for more detailed information about each stat.
- **Independent Toggles:** The meter panels and heat map can be enabled/disabled independently, and their opacity can be adjusted via the Mod Settings page.

### Heat Map
- **Primary Stat Highlighting:** The heat map is keyed to the primary stat for each room, which is usually "Impressiveness." If "Impressiveness" is not relevant, it defaults to an average of the relevant stats.
- **Opacity Adjustment:** Players can adjust the opacity of the heat map from the Mod Settings page.

### Integration with Existing Save
- **Compatibility:** It is safe to add or remove the mod from an existing save without causing issues.

## Coding Patterns and Conventions
- **Mod Structure:** The mod follows a standard structure with separate files for different functionalities, such as `HeatMap.cs`, `RoomSenseMod.cs`, `RoomSenseSettings.cs`, `RoomStat.cs`, and `GraphOverlay.cs`.
- **Harmony Patching:** The mod uses Harmony to patch game methods to integrate new features without altering the original game code.
- **XML Configuration:** XML files (`About.xml`, `KeyBindingCategories.xml`, `KeyBindings.xml`, `Version.xml`) are used for configuration and metadata.

## XML Integration
- **About.xml:** Contains metadata about the mod, including its dependencies.
- **TranslationTemplate:** Includes XML definitions for key binding categories and key bindings to support localization.
- **Version.xml:** Specifies the version of the mod.

## Harmony Patching
- The mod uses Harmony to patch game methods to integrate new features without altering the original game code. This includes patches for methods related to rendering, updating, and handling user input.

## Suggestions for Copilot
- **C# File Structure:** Suggest organizing C# files based on functionality (e.g., `HeatMap.cs` for heat map-related code, `RoomSenseMod.cs` for mod settings).
- **Harmony Patches:** Provide examples of Harmony patches for integrating new features into the game.
- **XML Configuration:** Assist with creating and maintaining XML files for mod configuration and metadata.
- **Code Conventions:** Follow established coding conventions for C# and XML to ensure readability and maintainability.
- **Error Handling:** Include error handling in code to manage potential issues, such as compatibility problems or configuration errors.

## Example Usage
### C# File Structure

RoomSense/
├── Source/
│   ├── RoomSense/
│   │   ├── HeatMap.cs
│   │   ├── RoomSenseMod.cs
│   │   ├── RoomSenseSettings.cs
│   │   ├── RoomStat.cs
│   │   ├── GraphOverlay.cs
│   ├── About.xml
│   ├── TranslationTemplate/
│   │   ├── KeyBindingCategoryDef/
│   │   │   ├── KeyBindingCategories.xml
│   │   ├── KeyBindingDef/
│   │   │   ├── KeyBindings.xml
│   ├── Version.xml


### Harmony Patch Example
csharp
using HarmonyLib;
using Verse;

namespace RoomSense
{
    [HarmonyPatch(typeof(EnvironmentDrawer), nameof(EnvironmentDrawer.DrawRoom))]
    public static class HarmonyPatches
    {
        public static void Postfix(Room room)
        {
            // Custom code to draw room stats
        }
    }
}


### XML Configuration Example
xml
<?xml version="1.0" encoding="utf-8"?>
<About>
    <Name>Room Sense (Continued)</Name>
    <Author>Falconne</Author>
    <PackageId>Mlie.RoomSense</PackageId>
    <Description>Update of Falconnes mod</Description>
    <Dependencies>
        <Dependency>brrainz.harmony</Dependency>
        <Dependency>UnlimitedHugs.HugsLib</Dependency>
    </Dependencies>
</About>


By following these guidelines, developers can contribute effectively to the Room Sense (Continued) mod, ensuring that the codebase remains clean, maintainable, and functional.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

