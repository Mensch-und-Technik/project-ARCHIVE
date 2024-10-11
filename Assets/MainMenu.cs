using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayMikwe()
    {
        SceneManager.LoadSceneAsync(1);
    }

   public void PlayTurm()
    {
        SceneManager.LoadSceneAsync(2);
    }

   public void PlayMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
