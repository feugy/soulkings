using UnityEngine;
using System.Collections;

public class NavigateButton : OnClick {

    // Destination scene
    public string scene;

    // overrides onClick behaviour to navigate instead of sending a message
    override internal void Action()
    {
        Application.LoadLevel(scene);
    }
}