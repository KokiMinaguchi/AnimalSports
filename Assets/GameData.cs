using SleepingAnimals;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Project/GameData")]
public class GameData : ScriptableObject
{
    public int Score;
    // �A�C�e���̓��_
    public readonly int[] FishPoint = { 10, 5, 3, 2 };
}
