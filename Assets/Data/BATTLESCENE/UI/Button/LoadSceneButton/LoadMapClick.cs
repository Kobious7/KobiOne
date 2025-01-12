using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle
{
    public class LoadMapClick : ButtonUIClick
    {
        protected override void OnClick()
        {
            base.OnClick();
            SceneManager.LoadScene("InfiniteMap");
        }
    }
}