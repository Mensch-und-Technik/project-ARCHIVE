using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMScript : MonoBehaviour
{
   public void OnPlayButton ()
   {
    SceneManager.LoadScene(1);
   }

   public void OnQuitButton ()
   {
    SceneManager.LoadScene(2);
   }

      public void OnBackButton ()
   {
    SceneManager.LoadScene(0);
   }
}
