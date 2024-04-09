using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PortalIn") )
        {
            GameObject portalOther = GameObject.FindGameObjectWithTag("PortalOut");

            if (portalOther != null && portalOther != collision.gameObject)
            {
                gameObject.transform.position = portalOther.transform.position;

            }
        }

    }
}
