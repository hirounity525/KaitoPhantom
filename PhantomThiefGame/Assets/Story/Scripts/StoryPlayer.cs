using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StoryPlayer : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;

    private void Start()
    {
        switch (CommonData.Instance.selectedStageNum)
        {
            case 0:
                flowchart.SendFungusMessage("Story1");
                break;
            case 2:
                flowchart.SendFungusMessage("Story2");
                break;
            case 4:
                flowchart.SendFungusMessage("Story3");
                break;
            case 6:
                flowchart.SendFungusMessage("Story4");
                break;
            case 8:
                flowchart.SendFungusMessage("Story5");
                break;
            case 11:
                flowchart.SendFungusMessage("Story6");
                break;
        }
    }
}
