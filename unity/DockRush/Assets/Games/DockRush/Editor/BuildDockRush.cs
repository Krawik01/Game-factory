using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFactory.DockRush
{
    /// <summary>Small build helper for this one experiment; it is not shared studio infrastructure.</summary>
    public static class BuildDockRush
    {
        private const string ScenePath = "Assets/Games/DockRush/Scenes/DockRush.unity";

        [MenuItem("Dock Rush/Prepare playable scene")]
        public static void PreparePlayableScene()
        {
            Directory.CreateDirectory("Assets/Games/DockRush/Scenes");
            if (File.Exists(ScenePath)) return;
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(scene, ScenePath);
            AssetDatabase.Refresh();
        }

        [MenuItem("Dock Rush/Build Android APK")]
        public static void BuildAndroid()
        {
            PreparePlayableScene();
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            Directory.CreateDirectory("Builds/Android");
            BuildPipeline.BuildPlayer(new[] { ScenePath }, "Builds/Android/DockRush.apk", BuildTarget.Android, BuildOptions.Development);
        }

        public static void BuildWindows()
        {
            PreparePlayableScene();
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
            Directory.CreateDirectory("Builds/Windows");
            BuildPipeline.BuildPlayer(new[] { ScenePath }, "Builds/Windows/DockRush.exe", BuildTarget.StandaloneWindows64, BuildOptions.Development);
        }

        [MenuItem("Dock Rush/Build iOS Xcode Project")]
        public static void BuildIOS()
        {
            PreparePlayableScene();

            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS &&
                !EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS))
            {
                throw new System.InvalidOperationException("Could not switch Unity to the iOS build target.");
            }

            Directory.CreateDirectory("Builds");
            var report = BuildPipeline.BuildPlayer(
                new[] { ScenePath },
                "Builds/iOS",
                BuildTarget.iOS,
                BuildOptions.None);

            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new System.InvalidOperationException("Dock Rush iOS export failed. See the Unity build report for details.");
            }
        }
    }
}
