using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject target;
    public ObjectTimer targetTimer;
    public GameObject destroyAtStart;
    public GUIText surviveTime;
    public GUIText finalTime;
    public GUIText highscore;
    public Canvas menu;
    public AudioClip[] audios;

    private float _destroyTime;
    private float _waitTime;
    private float _startWait;
    private float _xPos = 7.7f;
    private float _zPos = 4.75f;
    private float timePlayed;
    private bool playing;
    private bool destroyed;
    private int index;

	// Use this for initialization
	void Start () {
        Initialize();
        finalTime.text = "";
        surviveTime.text = "";
        Input.backButtonLeavesApp = true;
    }

    // Update is called once per frame
    void Update () {
        if (playing)
            surviveTime.text = (Time.time - timePlayed).ToString("F2");
	}

    IEnumerator SpawnTargets()
    {
        Ad advertisement = new Ad();
        yield return new WaitForSeconds(_startWait);
        while (true)
        {
            Quaternion spawnRotation = Quaternion.identity;
            Vector3 screenPosition = new Vector3(Random.Range(-_xPos, _xPos), 0, Random.Range(-_zPos, _zPos));

            destroyed = false;

            if (index >= 87)
                index = 87;
            AudioSource.PlayClipAtPoint(audios[index], screenPosition);
            Destroy(Instantiate(target, screenPosition, spawnRotation), _destroyTime);
            index++;
            yield return new WaitForSeconds(_waitTime);

            if (!destroyed)
            {
                var finishTime = (float)System.Math.Round(Time.time - timePlayed, 2);
                StoreHighscore(finishTime);
                surviveTime.text = "";
                finalTime.text = finishTime.ToString("F2");
                Initialize();
                break;
            }
        }

        if (Random.Range(0, 10) >= 8)
            advertisement.ShowAd();
    }

    IEnumerator SpawnManager()
    {
        while(_destroyTime > 0.15f)
        {
            if (_destroyTime <= 0.2f)
            {
                yield return new WaitForSeconds(7);
                _destroyTime -= 0.01f;
                _waitTime -= 0.01f;
            }
            else
            {
                yield return new WaitForSeconds(3);
                _destroyTime -= 0.05f;
                _waitTime -= 0.05f;
            }
        }
    }

    public void Initialize()
    {
        _destroyTime = 1.0f;
        _waitTime = _destroyTime;
        _startWait = 1.0f;
        index = 0;
        highscore.text = "Highscore: " + GetHighscore();
        destroyAtStart.SetActive(true);
        playing = false;
        menu.enabled = true;
        timePlayed = Time.time;
    }

    public void Destroyed()
    {
        destroyed = true;
    }

    public void Play()
    {
        highscore.text = "";
        finalTime.text = "";
        destroyAtStart.SetActive(false);
        menu.enabled = false;
        timePlayed = Time.time;
        playing = true;
        StartCoroutine(SpawnTargets());
        StartCoroutine(SpawnManager());
    }

    void StoreHighscore(float newHighscore)
    {
        float oldHighscore = PlayerPrefs.GetFloat("highscore", 0.00f);
        if (newHighscore > oldHighscore)
            PlayerPrefs.SetFloat("highscore", newHighscore);
    }

    float GetHighscore()
    {
        return PlayerPrefs.GetFloat("highscore", 0.00f);
    }
}
