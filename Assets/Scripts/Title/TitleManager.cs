using UnityEngine;


public class TitleManager : MonoBehaviour
{
    bool onStart;
    public void OnCPUStartButton()
    {
        if (onStart)
        {
            return;
        }
        onStart = true;
        GameDataManager.Instance.IsOnlineBattle = false;
        FadeManager.Instance.LoadScene("Game", 1f);
    }
    public void OnOnlineStartButton()
    {
        if (onStart)
        {
            return;
        }
        onStart = true;
        GameDataManager.Instance.IsOnlineBattle = true;
        FadeManager.Instance.LoadScene("Online", 1f);
    }


}
