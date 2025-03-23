using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle
{
    public class Player : Entity
    {
        protected override void LoadModel()
        {
            base.LoadModel();

            PlayerStatic.Instance.transform.Find("RigModel").transform.SetParent(Model);
            Model.Find("RigModel").transform.position = Model.parent.localPosition;
        }


    }
}