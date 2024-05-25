using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onAwakeHide : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(autoHide());
    }

    IEnumerator autoHide()
    {
        yield return new WaitForSeconds(0.01f);
        this.gameObject.SetActive(false);
    }
}
