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
            case 1:
                flowchart.SendFungusMessage("Story1");
                break;
            case 3:
                flowchart.SendFungusMessage("Story2");
                break;
            case 5:
                flowchart.SendFungusMessage("Story3");
                break;
            case 7:
                flowchart.SendFungusMessage("Story4");
                break;
            case 9:
                flowchart.SendFungusMessage("Story5");
                break;
            case 12:
                flowchart.SendFungusMessage("Story6");
                break;
        }
    }
}
