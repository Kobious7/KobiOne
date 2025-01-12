using System.Collections;
using Battle;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatic : GMono
{
    public static PlayerStatic Instance;

    protected override void Awake()
    {
        base.Awake();
        if(Instance != null) Debug.LogError("Only 1 PlayerStatic is allowed to exist!");

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameObject(GameObject obj)
    {
        Transform rigModel = Instantiate(obj.transform, Vector3.zero, Quaternion.identity);

        rigModel.transform.SetParent(transform);
        rigModel.name = "RigModel";
        Destroy(rigModel.GetComponent<PlayerAdjustColliderInMap>());
        rigModel.AddComponent<PlayerAdjustColliderInBattle>();
    }

    public void LoadBattle(string name)
    {
        LoadScene(name);
        StartCoroutine(MoveAndAttachToPlayerCoroutine());
    }

    private IEnumerator MoveAndAttachToPlayerCoroutine()
    {
        // Wait for the next frame to ensure the scene is loaded
        yield return null;

        // Get the active scene
        Scene newScene = SceneManager.GetActiveScene();
        // Move the object into the new scene's hierarchy
        SceneManager.MoveGameObjectToScene(gameObject, newScene);
        GameObject player = GameObject.FindObjectOfType<Player>().gameObject;

        if(player == null) Debug.Log("null");
    
        // Find the player object in the new scene // Make sure your player has the "Player" tag
            // Set this object as a child of the player
            transform.SetParent(player.transform, true);

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

    }
}