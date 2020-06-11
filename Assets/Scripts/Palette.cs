using UnityEngine;

[CreateAssetMenu(menuName = "Colors/Palette")]
public class Palette : ScriptableObject {
    public enum Shade {
        WHITE,
        HIGHLIGHT,
        LIGHT,
        PURE,
        DARK,
        OUTLINE,
        BLACK
    }
    public string colorName;
    public Color highlight;
    public Color light;
    public Color pure;
    public Color dark;
    public Color outline;
    private static readonly Color BLACK = Color.black;
    private static readonly Color WHITE = Color.white;


    public Color GetColor(Shade shade) {
        if (shade == Shade.WHITE) {
            return WHITE;
        } else if (shade == Shade.HIGHLIGHT) {
            return highlight;
        } else if (shade == Shade.LIGHT) {
            return light;
        } else if (shade == Shade.PURE) {
            return pure;
        } else if (shade == Shade.DARK) {
            return dark;
        } else if (shade == Shade.OUTLINE) {
            return outline;
        }
        return BLACK;
    }
}