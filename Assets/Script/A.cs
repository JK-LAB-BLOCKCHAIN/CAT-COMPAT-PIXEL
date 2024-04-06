using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
    [SerializeField] GameObject Cong1;
    [SerializeField] GameObject Cong2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Cong1)
        {
            gameObject.transform.position = Cong2.transform.position;
        }

        if (collision.gameObject == Cong2)
        {
            gameObject.transform.position = Cong1.transform.position;
        }
    }
}
