using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(player != null )
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement == null)
                Debug.Log("nullnull");
            else
                Debug.Log("999999");
        }
        else
        {
            Debug.Log("player=null");
        }
           
    }

    public void ReStartGame()
    {
        playerMovement.Restart();
        Debug.Log("----");
        SceneManager.LoadScene("GameScenes");
    }
}
