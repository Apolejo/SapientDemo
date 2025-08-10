// Copyright 2024 - 2025 Sapient Technology, Inc.

using System.IO;
using UnrealBuildTool;
using EpicGames.Core;
using Microsoft.Extensions.Logging;

public class SapientCore : ModuleRules
{
    public SapientCore(ReadOnlyTargetRules Target) : base(Target)
    {
        Type = ModuleType.CPlusPlus;
        PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

        // =================================================================
        // ADDITIONAL OPTIMIZATIONS
        // =================================================================

        // Disable unnecessary features for maximum performance
        bEnableExceptions = false;        // Disable C++ exceptions

        // Binary distribution settings
        bUsePrecompiled = true;

        // =================================================================
        // FORCE SHIPPING CONFIGURATION
        // =================================================================
        
        // Force maximum optimization
        OptimizeCode = CodeOptimization.Always;
        
        // Shipping-level build settings
        bUseUnity = true;             // Enable unity builds for faster compilation
        
        // =================================================================
        // DISABLE RTTI
        // =================================================================
        
        // Primary RTTI disable flag
        bUseRTTI = false;

        // =================================================================
        // COMPILER DEFINITIONS
        // =================================================================

        // Force shipping build defines (but keep editor functionality)
        PublicDefinitions.AddRange(new string[]
        {
            "DO_GUARD_SLOW=0",
            "WITH_EDITOR=1",          // Keep editor features for editor plugin
            "WITH_EDITORONLY_DATA=1", // Keep editor-only data
            "NO_RTTI=1",              // Custom RTTI disable flag
        });

        // =================================================================
        // COMPILE-TIME VERIFICATION
        // =================================================================

        // Add compile-time checks to verify our settings
        PublicDefinitions.AddRange(new string[]
        {
            "VERIFY_SHIPPING_BUILD=1",
            "VERIFY_NO_RTTI=1"
        });

        // =================================================================
        // PLATFORM-SPECIFIC COMPILER FLAGS
        // =================================================================

        // Windows (MSVC)
        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            // Use PrivateDefinitions instead of PublicAdditionalCompileArguments
            PrivateDefinitions.AddRange(new string[]
            {
                "_CRT_SECURE_NO_WARNINGS=1"
            });
            
            // Use AdditionalLinkArguments instead of PublicAdditionalLinkArguments
            PublicAdditionalLibraries.AddRange(new string[]
            {
                // Core Windows libraries if needed
            });
        }

        // Mac (Clang)
        else if (Target.Platform == UnrealTargetPlatform.Mac)
        {
            // Use PrivateDefinitions for compile flags
            PrivateDefinitions.AddRange(new string[]
            {
                "PLATFORM_MAC=1"
            });
        }

        // Linux (GCC/Clang)
        else if (Target.Platform == UnrealTargetPlatform.Linux)
        {
            // Use PrivateDefinitions for compile flags
            PrivateDefinitions.AddRange(new string[]
            {
                "PLATFORM_LINUX=1"
            });
        }

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

        // Core dependencies required for SapientCore
        PublicDependencyModuleNames.AddRange(
            new[]
            {
                "Core"
            }
        );

        // Editor-specific dependencies
        PrivateDependencyModuleNames.AddRange(
            new[]
            {
                "UnrealEd",
                "Slate",
                "SlateCore",
                "EditorFramework",
                "EditorStyle",
                "ToolMenus",
                "AssetTools",
                "BlueprintGraph",
                "BlueprintEditorLibrary",
                "ContentBrowser",
                "WorkspaceMenuStructure",
                "PythonScriptPlugin",
                "HotReload",
                "MainFrame",
                "AIModule",
                "HTTP",
                "Projects",
                "GameplayTasks",
                "Settings",
                "CoreUObject",
                "Engine",
                "Slate",
                "SlateCore",
                "InputCore",
                "UnrealEd",
                "LevelEditor",
                "Kismet",
                "KismetCompiler",
                "BlueprintGraph",
                "GraphEditor",
                "PropertyEditor",
                "EditorStyle",
                "AssetRegistry",
                "Json",
                "JsonUtilities",
                "EditorSubsystem",
                "AIModule",
                "EditorScriptingUtilities",
                "BehaviorTreeEditor",
                "AIGraph",
                "ApplicationCore",
                "FileUtilities",
                "UMGEditor",
                "UMG",
                "Niagara",
                "MovieScene"
            }
        );
         
        // Define that we're building the SapientCore DLL (exports)
        PublicDefinitions.Add("SAPIENTCORE_EXPORTS=1");
        
        /**
         * @brief Controls shipping build logging behavior
         *
         * When set to 1, development logging is disabled and only critical logs remain
         * When set to 0, all logging is enabled for development purposes
         *
         * @note Modify this value before building for different configurations:
         * - Development/Debug: Set to 0
         * - Shipping/Release: Set to 1
         */
        PublicDefinitions.Add("SAPIENTCORE_UE_BUILD_SHIPPING=1");
    }
}
