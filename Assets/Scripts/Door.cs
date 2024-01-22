using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private SpawnManager spawnManager;
    public bool playerIsNear;
    public bool playerTeleport;
    public GameObject camera_object;
    //public GameObject camera;

    private void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        playerIsNear = false;
    }

    private void Update()
    {
            DoorInteraction();
    }

    private void DoorInteraction()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.T) && spawnManager.AreAllEnemiesDefeated())
        {
            //camera.SetActive(false);
            TriggerTeleportation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void TriggerTeleportation()
    {
        CameraController main_camera = camera_object.GetComponent<CameraController>();
        /*
            
        */
        main_camera.transition.position = new Vector3(main_camera.transform.position.x - (45), main_camera.transform.position.y, 10f);

        main_camera.side = 0;

        main_camera.timer = 180;

        if (PlayerMovement.PlayerHealth < 5) PlayerMovement.PlayerHealth++;
        
        ScoreTracker.Score++;
    }

}