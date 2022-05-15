using System;
using UnityEngine;

public class Styles
{
    private static Lazy<Font> mFont = new Lazy<Font>(() => Resources.Load<Font>("unifont-14.0.03"));

    public static Lazy<GUIStyle> Label = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
    {
        fontSize = 20,
        font = mFont.Value,
    });

    public static Lazy<GUIStyle> Button = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
    {
        fontSize = 20,
        font = mFont.Value,
    });

    public static Lazy<GUIStyle> BigLabel = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
    {
        fontSize = 30,
        font = mFont.Value,
    });
    
    public static Lazy<GUIStyle> BigButton = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
    {
        fontSize = 30,
        font = mFont.Value,
    });
}
