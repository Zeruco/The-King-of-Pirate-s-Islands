using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private StandartRoom[] _standartRoomsTemplates;
    [SerializeField] private SpawnRoom _spawnRoomTemplate;
    [SerializeField] private SpecialRoom[] _specialRoomsTemplates;
    [SerializeField] private StandartBridge _standartHorizontalBridge;
    [SerializeField] private StandartBridge _standartVerticalBridge;
    [SerializeField] private SecretBridge _secretBrigde;

    private System.Random _random;
    private readonly int _maxRoomCount = 17;
    private readonly int _minRoomCount = 8;
    private List<int> _endRooms;
    private int[] _floorPlan;
    private int _floorPlanCount;
    private int _bossIndexRoom;
    private List<int> _cellQueue;
    private bool _placedSpecial;
    private Dictionary<int, Room> _generatedRooms;

    private void Start()
    {
        _generatedRooms = new Dictionary<int, Room>();
        _floorPlan = new int[200];
        _cellQueue = new List<int>();
        _endRooms = new List<int>();
        _random = new System.Random();

        Visit(45);
    }

    private void SetImage(int i, Room room)
    {
        int x = i % 10;
        int y = (i - x) / 10;
        var newRoom = Instantiate(room, new Vector2(x * room.GetComponent<SpriteRenderer>().bounds.size.x - 10, y * room.GetComponent<SpriteRenderer>().bounds.size.y - 8), room.transform.rotation);

        if (_generatedRooms.ContainsKey(i))
        {
            Destroy(_generatedRooms[i].gameObject);
            _generatedRooms.Remove(i);
            _generatedRooms.Add(i, newRoom);
        }
        else
            _generatedRooms.Add(i, newRoom);
    }

    private int PopRandomEndRoom()
    {
        int index = Convert.ToInt32(Math.Floor(_random.NextDouble() * _endRooms.Count));
        int i = _endRooms[index];
        Splice(index, 1);
        return i;
    }

    public void Splice(int index, int count)
    {
        var items = _endRooms.GetRange(index, count);
        _endRooms.RemoveRange(index, count);
    }

    private int PickSecretRoom()
    {
        for (int i = 0; i < 900; i++)
        {
            int x = Convert.ToInt32((Math.Floor(_random.NextDouble() * 9) + 1));
            int y = Convert.ToInt32((Math.Floor(_random.NextDouble() * 8) + 2));
            int j = y * 10 + x;

            if (_floorPlan[j] != 0)
                continue;
            if (_bossIndexRoom == i - 1 || _bossIndexRoom == i + 1 || _bossIndexRoom == i + 10 || _bossIndexRoom == i - 10)
                continue;
            if (GetNeighboursCount(j) >= 3)
                return j;
            if (i > 300 && GetNeighboursCount(j) >= 2)
                return j;
            if (i > 600 && GetNeighboursCount(j) >= 1)
                return j;
        }
        Debug.Log("returned 0");
        return 0;
    }

    private void Update()
    {
        if (_cellQueue.Count > 0)
        {

            int i = _cellQueue[0];
            _cellQueue.RemoveAt(0);

            int x = i % 10;
            bool created = false;

            if (x > 1)
                created = created | Visit(i - 1);
            if (x < 9)
                created = created | Visit(i + 1);
            if (i > 20)
                created = created | Visit(i - 10);
            if (i < 70)
                created = created | Visit(i + 10);
            if (!created)
                _endRooms.Add(i);
        }
        else if (_placedSpecial == false)
        {

            if (_floorPlanCount < _minRoomCount)
            {
                return;
            }
            _placedSpecial = true;
            _bossIndexRoom = _endRooms[0];
            SetImage(_bossIndexRoom, _specialRoomsTemplates[UnityEngine.Random.Range(0, _specialRoomsTemplates.Length)]);
            Debug.Log(_bossIndexRoom + " - Boss");
            _endRooms.RemoveAt(0);

            int rewardRoomIndex = PopRandomEndRoom();
            SetImage(rewardRoomIndex, _specialRoomsTemplates[UnityEngine.Random.Range(0, _specialRoomsTemplates.Length)]);
            Debug.Log(rewardRoomIndex + " - reward");
            int coinRoomIndex = PopRandomEndRoom();
            SetImage(coinRoomIndex, _specialRoomsTemplates[UnityEngine.Random.Range(0, _specialRoomsTemplates.Length)]);
            Debug.Log(coinRoomIndex + " - Shop");
            int sercretRoomIndex = PickSecretRoom();
            SetImage(sercretRoomIndex, _specialRoomsTemplates[UnityEngine.Random.Range(0, _specialRoomsTemplates.Length)]);
            Debug.Log(sercretRoomIndex + " - secret");

            FindBridgesPostions();
        }
    }

    private int GetNeighboursCount(int index)
    {
        return _floorPlan[index - 10] + _floorPlan[index - 1] + _floorPlan[index + 1] + _floorPlan[index + 10];
    }

    private bool Visit(int index)
    {
        if (_floorPlan[index] != 0)
            return false;

        int neighboursCount = GetNeighboursCount(index);

        if (neighboursCount > 1)
            return false;

        if (_floorPlanCount >= _maxRoomCount)
            return false;

        if (_random.NextDouble() < 0.5 && index != 45)
            return false;

        _cellQueue.Add(index);
        _floorPlan[index] = 1;
        _floorPlanCount += 1;

        if (index == 45)
            SetImage(index, _spawnRoomTemplate);
        else
            SetImage(index, _standartRoomsTemplates[UnityEngine.Random.Range(0, _standartRoomsTemplates.Length)]);
        return true;
    }

    private void FindBridgesPostions()
    {
        foreach (var room in _generatedRooms)
        {
            if (_generatedRooms.ContainsKey(room.Key - 1))
            {
                SetBridge(room.Value, -9, 0, _standartHorizontalBridge, 1, _generatedRooms[room.Key - 1]);
            }

            if (_generatedRooms.ContainsKey(room.Key + 1))
            {
                SetBridge(room.Value, 9, 0, _standartHorizontalBridge, -1, _generatedRooms[room.Key + 1]);
            }

            if (_generatedRooms.ContainsKey(room.Key - 10))
            {
                SetBridge(room.Value, 0, -5, _standartVerticalBridge, 2, _generatedRooms[room.Key - 10]);
            }

            if (_generatedRooms.ContainsKey(room.Key + 10))
            {
                SetBridge(room.Value, 0, 5, _standartVerticalBridge, -2, _generatedRooms[room.Key + 10]);
            }
        }
    }

    private void SetBridge(Room room, float offsetX, float offsetY, Bridge bridge, int roomIndex, Room targetRoom)
    {
        Vector2 roomPosition = room.transform.position;
        Bridge newBridge = Instantiate(bridge, new Vector2(roomPosition.x + offsetX, roomPosition.y + offsetY), Quaternion.identity, room.transform);
        newBridge.transform.localScale = Vector3.one;
        newBridge.SetRoomIndex(targetRoom, roomIndex);
    }
}
