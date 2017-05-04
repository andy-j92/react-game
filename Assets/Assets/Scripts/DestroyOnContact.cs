using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

    private GameController gameController;
    
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            gameController.Destroyed();
        }
    }
}
