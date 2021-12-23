using UnityEngine;

public class GamePause : MonoBehaviour
{
    public void MakePause()
    {
        Time.timeScale = 0;
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1;
    }
}
