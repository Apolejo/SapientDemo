// Copyright Epic Games, Inc. All Rights Reserved.
// Copyright 2024 - 2025 Sapient Technology, Inc.

using System.IO;
using UnrealBuildTool;

public class Sapient : ModuleRules
{
	public Sapient(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

        // Binary distribution settings
        bUsePrecompiled = true;

		// Optimization settings
#if UE_5_6_OR_LATER
		CppCompileWarningSettings.UndefinedIdentifierWarningLevel = WarningLevel.Off;
#elif UE_5_5_OR_LATER
		UndefinedIdentifierWarningLevel = WarningLevel.Off;
#endif

		PublicIncludePaths.AddRange(
			new[]
			{
				Path.Combine(ModuleDirectory, "Public")
			}
		);

		PrivateIncludePaths.AddRange(
			new[]
			{
				Path.Combine(ModuleDirectory, "Private")
			}
		);

        // Core module dependencies
        PublicDependencyModuleNames.AddRange(
            new[]
            {
                "Core",
                "CoreUObject",
                "Engine",
                "HTTP",
                "Json",
                "JsonUtilities"
            }
        );

        // Editor-specific dependencies
        PrivateDependencyModuleNames.AddRange(
            new[]
            {
                "BlueprintEditorLibrary",
                "Projects",
                "Slate",
                "SlateCore",
                "ToolMenus",
                "UnrealEd",
                "DesktopPlatform"
            }
        );

        // Add definitions for DLL management system
        PublicDefinitions.Add("WITH_SAPIENT_DLL_MANAGEMENT=1");
    }
}
