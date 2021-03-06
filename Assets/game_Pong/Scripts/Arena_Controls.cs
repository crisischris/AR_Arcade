﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_Controls : MonoBehaviour
{
    public Camera playerView;
    public GameObject gm;
    // Update is called once per frame
    void Update()
    {
        if (gm.GetComponent<Game_Manager>().game_started == false)
            MoveArenaUpDown();
    }

    void MoveArenaUpDown()
    {
        if (Input.touchCount != 1)
            return;

        var ray = playerView.ScreenPointToRay(Input.touches[0].position);
        var hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.name != "Player Score Wall")
                return;
            var moveArena = transform.position;
            moveArena.y = hitInfo.point.y;
            transform.position = moveArena;
        }
    }

}
