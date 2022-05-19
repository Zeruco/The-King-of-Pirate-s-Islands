using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeHolder : MonoBehaviour
{
    Dictionary<Bridge, int> bridgesRelocators;

    public void SetBridgesRelocators(Bridge bridge, int index)
    {
        bridgesRelocators.Add(bridge, index);
    }
}
