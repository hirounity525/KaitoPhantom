using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HideObjectType
{
    STATUE,
    PAINTING,
    BOX
}

public class SneakingHideObjectInfo : MonoBehaviour
{
    public HideObjectType hideObjectType;
    public bool isHideTarget;
    public GameObject hideVC;
}
