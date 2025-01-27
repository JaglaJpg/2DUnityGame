using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public float m_FieldOfView;
    //makes camera follow player

    void Start()
    {
        //Camera.orthographic = false;
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x + 1f, player.position.y, transform.position.z);
        Camera.main.fieldOfView = m_FieldOfView;
    }
}
