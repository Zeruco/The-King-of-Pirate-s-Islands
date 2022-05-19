using UnityEngine;

public abstract class Room : MonoBehaviour
{
    public Entry[] Entries { get; private set; }

    private void Awake()
    {
        Entries = GetComponentsInChildren<Entry>();
    }
}
