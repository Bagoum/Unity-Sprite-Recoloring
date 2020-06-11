using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public abstract class ColorMap : ScriptableObject {
    protected virtual void PrepareColors() { }

    protected const byte zero = 0;
    
    public Sprite Recolor(Sprite baseSprite) {
        PrepareColors();
        Texture2D tex = Instantiate(baseSprite.texture);
        NativeArray<Color32> pixels_n = tex.GetRawTextureData<Color32>();
        int len = pixels_n.Length;
        unsafe {
            Color32* pixels = (Color32*)pixels_n.GetUnsafePtr();
            Map(pixels, len);
        }
        tex.Apply();
        Vector2 pivot = baseSprite.pivot;
        pivot.x /= baseSprite.rect.width;
        pivot.y /= baseSprite.rect.height;
        var s = Sprite.Create(tex, baseSprite.rect, pivot, baseSprite.pixelsPerUnit);
        return s;
    }

    protected abstract unsafe void Map(Color32* pixels, int len);
}
public abstract class ThreeColorGradientMap : ColorMap {
    protected Color32 mBlack;
    protected Color32 mGray;
    protected Color32 mWhite;
    
    protected override unsafe void Map(Color32* pixels, int len) {
        for (int ii = 0; ii < len; ++ii) {
            Color32 pixel = pixels[ii];
            if (pixel.a > zero) {
                float value = pixel.r / 255f;
                if (value > 0.5f) {
                    value = value * 2 - 1f;
                    pixel.r = (byte)(mGray.r + value * (mWhite.r - mGray.r));
                    pixel.g = (byte)(mGray.g + value * (mWhite.g - mGray.g));
                    pixel.b = (byte)(mGray.b + value * (mWhite.b - mGray.b));
                } else {
                    value = value * 2;
                    pixel.r = (byte)(mBlack.r + value * (mGray.r - mBlack.r));
                    pixel.g = (byte)(mBlack.g + value * (mGray.g - mBlack.g));
                    pixel.b = (byte)(mBlack.b + value * (mGray.b - mBlack.b));
                }
                pixels[ii] = pixel;
            }
        }
    }
}

[CreateAssetMenu(menuName = "Colors/ThreeColorGradient")]
public class PaletteThreeColorGradientMap : ThreeColorGradientMap {
    public Palette mapToBlackBase;
    public Palette.Shade mapToBlackShade;
    public Palette mapToGrayBase;
    public Palette.Shade mapToGrayShade;
    public Palette mapToWhiteBase;
    public Palette.Shade mapToWhiteShade;

    protected override void PrepareColors() {
        mBlack = mapToBlackBase.GetColor(mapToBlackShade);
        mGray = mapToGrayBase.GetColor(mapToGrayShade);
        mWhite = mapToWhiteBase.GetColor(mapToWhiteShade);
    }
}