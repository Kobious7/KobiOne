using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIHider : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    IEnumerator Start()
    {
        yield return null;
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
}
