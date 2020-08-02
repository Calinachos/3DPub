using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public Transform dontDestroy;
    private void Awake()
    {
        if (GameObject.Find("SceneManagerDontDestroy(Clone)") == null)
        {
            Instantiate(dontDestroy, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

}
