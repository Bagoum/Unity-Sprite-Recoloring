using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolorMe : MonoBehaviour {
    public ColorMap recolorMap;
    private void Start() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite s = sr.sprite;
        Sprite recolored_s = recolorMap.Recolor(s);
        sr.sprite = recolored_s;
    }
}