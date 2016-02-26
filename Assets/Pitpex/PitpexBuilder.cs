#if UNITY_EDITOR

using UnityEditor;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

public class PitpexBuilder
{
    [MenuItem("Pitpex/Build Pitpets Game")]
    public static void BuildGame()
    {
        // Get filename
        string path = EditorUtility.SaveFilePanel("Where would you like to save your game?", "", "", "pitpex");
        string filename = Path.GetFileName(path);
        string dir = Path.GetDirectoryName(path);
        StringBuilder sb = new StringBuilder();
        bool upper = false;
        foreach(char c in filename)
        {
            if('_' == c || ' ' == c)
            {
                upper = true;
                continue;
            }
            if(true == upper)
            {
                sb.Append(char.ToUpper(c));
            }
        }
        path = dir + "/" + filename;
        List<string> scenes = new List<string>();

        scenes.Add("Assets/Pitpex/PitpexStart.unity");

        foreach(UnityEditor.EditorBuildSettingsScene s in UnityEditor.EditorBuildSettings.scenes)
        {
            if (s.enabled)
            {
                scenes.Add(s.path);
            }
        }

        scenes.Add("Assets/Pitpex/PitpexEnd.unity");

        // Build player.
        BuildPipeline.BuildPlayer(scenes.ToArray(), path, BuildTarget.WebGL, BuildOptions.None);

        // Copy a file from the project folder to the build folder, alongside the built game.
        FileUtil.MoveFileOrDirectory(path + "/index.html", path + "/index.template");
        EditorUtility.DisplayDialog("Build Complete!", "You're almost done! Just drop your game's .pitpex folder into Pitpets/Games and run 'rake games'", "Awesome, thanks!");
        EditorUtility.RevealInFinder(path);
    }
}

#endif