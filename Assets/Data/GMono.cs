using Battle;
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

    public Tiles GetTile(Transform obj)
    {
        return obj.GetComponent<Tiles>();
    }

    public Vector3 to2DVec(Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
