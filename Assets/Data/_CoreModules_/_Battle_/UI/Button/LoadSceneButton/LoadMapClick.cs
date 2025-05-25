using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapClick : ButtonUIBClick
{
    protected override void OnClick()
    {
        base.OnClick();
        SceneManager.LoadScene("InfiniteMap");
    }
}