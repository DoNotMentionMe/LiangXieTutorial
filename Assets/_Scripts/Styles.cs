using System;
using UnityEngine;

public class Styles
{
    public static Lazy<GUIStyle> Label = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
    {
        fontSize = 20
    });

    public static Lazy<GUIStyle> Button = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
    {
        fontSize = 20
    });

    public static Lazy<GUIStyle> BigLabel = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
    {
        fontSize = 30
    });
    
    public static Lazy<GUIStyle> BigButton = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
    {
        fontSize = 30
    });
}
