using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler
{
    private static PortalHandler instance = null;
    public static PortalHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PortalHandler();
            }
            return instance;
        }
    }

    public bool CanTeleport = true;
}
