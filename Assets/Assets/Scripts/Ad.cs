using UnityEngine;
using UnityEngine.Advertisements;

public class Ad : MonoBehaviour 
{
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
}
