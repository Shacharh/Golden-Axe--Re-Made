using UnityEngine;

public class Startgame : MonoBehaviour
{
   // launch GameWorld scene
   public void StartGame()
   {
       UnityEngine.SceneManagement.SceneManager.LoadScene("GameWorld");
   }
}
