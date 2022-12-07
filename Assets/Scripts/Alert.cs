using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
   public void ActivateAlert()
    {
        GameObject.Find("AlertCanvas").transform.Find("AlertAccessDenied").gameObject.SetActive(true);
    }
}
