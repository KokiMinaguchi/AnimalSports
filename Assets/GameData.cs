using SleepingAnimals;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Project/GameData")]
public class GameData : ScriptableObject
{
    public int Score;
    // アイテムの得点
    public readonly int[] FishPoint = { 10, 5, 3, 2 };
}
