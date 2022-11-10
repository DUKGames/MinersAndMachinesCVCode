using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;

    private void Awake()
    {
        if(GameAssets.i == null)
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Excavation excavation;
    public UIText workingCostText;
    public Machine dozer;
    public Machine excavator;

    public Transform fuelWarning;
    public Transform noMoneyWarning;
    public Transform padlockEffect;
    public Transform upgradeEffect;
    public Transform panningEffect;

    public Sprite forestBackgroundSprite;
    public Sprite forestFrontBackgroundSprite;

    public Sprite desertBackgroundSprite;
    public Sprite desertFrontBackgroundSprite;

    public Sprite jungleFrontBackgroundSprite;

    public Sprite iceIslandFrontBackgroundSprite;
}
