using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : MonoBehaviour
{
    [field: SerializeField]
    public GameSceneManager.Scene LevelScene { get; private set; }
}
