using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlM : MonoBehaviour
{
    public Camera camP1;
    public Camera camP2;
    public HighScore highScoreDisplay;

    // Update is called once per frame
    void Update()
    {
        // if either players lives are 0 switch to full screen on the remaining camera's pov
        // get camera of player 1 and player 2
        if (GameManager.instance.player1 != null)
        {
            camP1 = GameManager.instance.player1.GetComponentInChildren<Camera>();
        }
        if (GameManager.instance.player2 != null)
        {
            camP2 = GameManager.instance.player2.GetComponentInChildren<Camera>();
        }
        // if multiplayer is enabled
        // set transforms of those players cameras
        if (GameManager.instance.isMultiplayer == true)
        {
            if (p1Alive() == true && p2Alive() == true)
            {
                setCameraSplitscreen();
            }
            else
            {
                setCameraFullscreen(PlayerCamera());
                highScoreDisplay = FindObjectOfType<HighScore>();
                highScoreDisplay.setHighScoreSinglePlayer();
            }
        }
        else
        {
            setCameraFullscreen(camP1);
        }
    }

    bool p1Alive()
    {
        if (GameManager.instance.p1Lives >= 0)
        {
            return true;
        }
        return false;
    }

    bool p2Alive()
    {
        if (GameManager.instance.p2Lives >= 0)
        {
            return true;
        }
        return false;
    }

    Camera PlayerCamera()
    {
        if (p1Alive() == true)
        {
            return camP1;
        }
        if (p2Alive() == true)
        {
            return camP2;
        }
        return null;
    }

    // set up split screen
    void setCameraSplitscreen()
    {
        if (camP1 != null)
        {
            camP1.rect = new Rect(0, 0.5f, 1, 0.5f);
        }
        if (camP2 != null)
        {
            camP2.rect = new Rect(0, 0, 1, 0.5f);
        }
    }

    // set up full screen
    void setCameraFullscreen(Camera cam)
    {
        cam.rect = new Rect(0, 0, 1, 1);
    }
}
