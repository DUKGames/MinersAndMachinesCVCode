using UnityEngine;

public class Land
{
    [field: SerializeField]
    public Lands.LandType ThisLandType { get; private set; }

    [field: SerializeField]
    public Parcels LandParcels { get; private set; }

}
