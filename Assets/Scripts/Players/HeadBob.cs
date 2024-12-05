using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    private PlayerMovement player;
    private Animator anim;
    private bool walking;
    private bool running;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        walking = player.walking;
        running = player.running;

        UpdateHeadBob();
    }

    private void UpdateHeadBob()
    {
        anim.SetBool("Walk", walking);
        anim.SetBool("Run", running);
    }
}
