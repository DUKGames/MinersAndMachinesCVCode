using UnityEngine;

public class Parcel : MonoBehaviour
{
    [field: SerializeField]
    public GameSceneManager.Scene LevelScene { get; private set; }
}
