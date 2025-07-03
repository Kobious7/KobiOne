using UnityEngine;
using UnityEngine.SceneManagement;

public class GMono : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponents();
        ResetValues();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected virtual void LoadComponents() {}

    protected virtual void ResetValues() {}

    protected virtual void Start() {}

    protected virtual void OnEnable() {}

    protected virtual void OnDisable() {}

    public TileBoard GetTile(Transform obj)
    {
        return obj.GetComponent<TileBoard>();
    }

    public Vector3 to2DVec(Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    private string commonQualityColor = "#B0B0B0";
    private string uncommonQualityColor = "#28CF0B";
    private string rareQualityColor = "#2574FF";
    private string epicQualityColor = "#9B00FF";
    private string legendaryQualityColor = "#FFF100";

    public Color GetQualityColorByRarity(Rarity rarity)
    {
        if(rarity == Rarity.Common) return ColorUtility.TryParseHtmlString(commonQualityColor, out Color color) ? color : Color.white;
        else if(rarity == Rarity.Uncommon) return ColorUtility.TryParseHtmlString(uncommonQualityColor, out Color color) ? color : Color.white;
        else if(rarity == Rarity.Rare) return ColorUtility.TryParseHtmlString(rareQualityColor, out Color color) ? color : Color.white;
        else if(rarity == Rarity.Epic) return ColorUtility.TryParseHtmlString(epicQualityColor, out Color color) ? color : Color.white;
        else return ColorUtility.TryParseHtmlString(legendaryQualityColor, out Color color) ? color : Color.white;
    }
}
