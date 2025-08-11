using UnityEngine;

public class ObstacleMetadata : MonoBehaviour {
    public enum AllowedLane { Left, Middle, Right, LeftOrMiddle, RightOrMiddle, Any }
    public AllowedLane allowedLane = AllowedLane.Any;
}