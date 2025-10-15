# GitHub Copilot Instructions for RimWorld Mod: Room Sense (Continued)

## Mod Overview and Purpose

The "Room Sense (Continued)" mod is an update of the Falconnes mod designed to provide a comprehensive display of room statistics in RimWorld. This mod offers toggleable panels that visualize relevant room stats such as beauty, cleanliness, and space through graphical meters and a colored overlay. It streamlines the environment inspection process, helping players quickly assess the significance of each stat and make informed decisions about room improvement and management.

## Key Features and Systems

- **Toggleable Panels**: Display room stats with meters that segment stats into categories (e.g., "dull", "mediocre", "interesting"). Meters' colors change to red, yellow, or green based on the stat's condition.
- **Colored Heat Map**: Shows a color-coded overlay over relevant rooms, displaying the primary room stat or an average of applicable stats. Adjustable opacity is available through the Mod Settings page.
- **Room Relevance**: Show stats only for relevant room roles. For example, bedrooms display all stats, while hospitals focus on space, beauty, and cleanliness.
- **User Interface Integration**: Toggle the overlay using a newly added button in the bottom right tool dock or via a configurable keyboard shortcut (default is "=").
- **Cross-platform Considerations**: While panels work across platforms, the colored overlay is disabled on Linux due to game code issues.

## Coding Patterns and Conventions

- **Separation of Concerns**: Organization of distinct functionalities across different files and classes, such as `GraphOverlay`, `HeatMap`, and `InfoCollector`.
- **Modular Design**: Formulating methods that are focused on a single task, aiding in readability, maintenance, and testing.
- **Naming Conventions**: Alignment with C# naming conventions such as Pascal case for classes and methods, aligned with RimWorld modding norms.

## XML Integration

XML is not directly mentioned in the project files, but it likely integrates with the RimWorld game's XML-based configuration, possibly through JSON or embedded resource files for customizable settings within `Resources.cs`.

## Harmony Patching

- **Harmony Patch Usage**: Modify base game behavior with patches to classes like `MapInterface_Patch` and `PlaySettings_Patch`. Utilize patches sparingly to minimize compatibility issues.
- **Safe Patching Practices**: Structure patches to prevent crashes, such as checking for null or invalid states before executing Harmony adjustments.

## Suggestions for Copilot

To effectively utilize GitHub Copilot in developing this mod, consider:

- **Integrating Complex Logic**: Copilot can aid in forming intricate game logic scenarios, especially when determining room relevance.
- **UI Development**: Harness Copilot to generate boilerplate code for Unity-style GUI elements and integrate them with the game's existing interface.
- **Code Refactoring**: Suggest improvements or more optimized implementations for existing code within established classes like `GraphOverlay`.
- **Cross-Platform Concerns**: Seek out Copilot for alternative implementations that might resolve or better handle Linux-specific challenges.
- **Documentation Assistance**: Generate concise comments or documentation strings to ensure clear understanding across the development team.

By leveraging these tools and practices, you can enhance the functionality and reliability of the "Room Sense (Continued)" mod, ensuring a seamless user experience.
