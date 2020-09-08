using System;
using UnityEngine;

 [Serializable]
 [CreateAssetMenu(fileName = "New Game Data", menuName = "Custom/Game Data")]
 public class SaveLoadSO : ScriptableObject
{
    public GameData data;

    public static void Save(GameData _data, string _fileName)
    {
        SaveLoadSO saveLoadSO = (SaveLoadSO)Resources.Load("ScriptableObjects/GameData");
        saveLoadSO.data = _data;
    }

    public static GameData Load(string _fileName)
    {
        SaveLoadSO saveLoadSO = (SaveLoadSO)Resources.Load("ScriptableObjects/GameData");
        return saveLoadSO.data;
    }
}