using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHazardsButtonActivated : FallingHazard
{
    [Space(20)]
    public ButtonScript Button;

    public override void CheckForActivation()
    {
        if (Button.buttonActive)
        {
            if (!activated)
            {
                activated = true;
            }
        }
        if (activated && !fell)
        {
            Fall();
            fell = true;
        }
    }
}
