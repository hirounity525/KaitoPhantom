using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPhantomFaceChanger : MonoBehaviour
{
    [SerializeField] private Texture defaultTex;
    [SerializeField] private Texture surpriseTex;
    [SerializeField] private Texture seriousTex;
    [SerializeField] private Texture angryTex;
    [SerializeField] private Texture motivationTex;

    private Renderer faceRender;

    private void Awake()
    {
        faceRender = GetComponent<Renderer>();
    }

    public void ChangeDefault()
    {
        faceRender.material.mainTexture = defaultTex;
    }

    public void ChangeSurprise()
    {
        faceRender.material.mainTexture = surpriseTex;
    }

    public void ChangeSerious()
    {
        faceRender.material.mainTexture = seriousTex;
    }

    public void ChangeAngry()
    {
        faceRender.material.mainTexture = angryTex;
    }

    public void ChangeMotivation()
    {
        faceRender.material.mainTexture = motivationTex;
    }
}
