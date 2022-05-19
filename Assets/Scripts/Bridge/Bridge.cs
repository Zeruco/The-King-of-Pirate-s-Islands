using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bridge : MonoBehaviour
{
    protected Room _targetRoom;
    protected int _entryIndex;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hello");
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            foreach (var entry in _targetRoom.Entries)
            {
                if (_entryIndex == entry.Index)
                {
                    player.transform.position = entry.transform.position;
                }
            }
        }
    }

    public void SetRoomIndex(Room targetRoom, int entryIndex)
    {
        _targetRoom = targetRoom;
        _entryIndex = entryIndex;
    }
}
