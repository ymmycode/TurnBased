using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMaterial : MonoBehaviour
{

    public float scrollX = 0.5f;
    public float scrollY = 0.5f;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offSetX = Time.time * scrollX;
        float offSetY = Time.time * scrollY;

        Vector2 newOffset = new Vector2(offSetX, offSetY);

        rend.material.SetTextureOffset("_BaseMap", newOffset);
    }
}
