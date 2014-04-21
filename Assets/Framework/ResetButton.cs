using UnityEngine;
using System.Collections;

public class ResetButton : NavigateButton {

    // extends NavigateButton behaviour to reset game
    override internal void Action()
    {
        // reset whole game
        PlayerSingleton.Instance.Reset();
        base.Action();
    }
}
