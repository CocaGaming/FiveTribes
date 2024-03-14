using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : BaseManager<GameManager>
{
    public TribeType tribeType;
    public RoleType roleType;
    private GameObject characterSelected;
    public float hour;
    public float minute;

    private Light[] torchs;
    private bool isWarTime;
    private bool isEndWarTime;
    private bool isLoseGame;
    private bool isWinGame;

    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenHome>();
            ScreenHome scr= UIManager.Instance.GetExistScreen<ScreenHome>();
            if(scr != null)
            {
                UIManager.Instance.ShowScreen<ScreenHome>();
            }
        }

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBGM(AUDIO.BGM_MENU);
        }
    }
    private void Update()
    {
        if (GameTime.Instance != null)
        {
            hour = GameTime.Instance.hour;
            minute=GameTime.Instance.minute;
        }

        torchs = FindObjectsOfType<Light>();
        if ((hour >= 18f && hour<=23f) || (hour>=0f && hour<=5f))//tối thì đuốc mới phát sáng
        {
            foreach (Light torch in torchs)
            {
                torch.enabled = true;
            }
        }
        else
        {
            foreach (Light torch in torchs)
            {
                torch.enabled = false;
            }
        }
        //Trống trận lúc bắt đầu war
        if (isWarTime)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayLongSE(AUDIO.LONGSE_WARHORN);
            }

            if (UIManager.HasInstance)
            {
                UIManager.Instance.ShowPopup<PopupTutorial>();
            }
        }
        if (hour == 10f && minute==0f)
        {
            isWarTime = true;
        }
        else
        {
            isWarTime = false;
        }

        //Trống trận lúc kết thúc war
        if (isEndWarTime)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlayLongSE(AUDIO.LONGSE_WARHORN);
            }

            if (UIManager.HasInstance)
            {
                UIManager.Instance.ShowPopup<PopupBackToBaseCamp>();
            }
        }
        if (hour == 18f && minute == 0f)
        {
            isEndWarTime = true;
        }
        else
        {
            isEndWarTime = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        //Điều kiện thua
        PlayerInputManager player=FindObjectOfType<PlayerInputManager>();
        GameObject gameTime = GameObject.FindGameObjectWithTag("GameTime");
        if (player !=null)
        {
            if (player.gameObject.GetComponent<HealthManager>().currentHealth <= 0 && !isLoseGame)
            {
                isLoseGame = true;

                if (gameTime != null)
                {
                    gameTime.SetActive(false);//tắt đồng hồ để khi load lại scene sẽ reset giờ lại
                }

                StartCoroutine(LoseLoadHomeScene());
            }
            else if(player.gameObject.GetComponent<HealthManager>().currentHealth > 0)
            {
                isLoseGame=false;
            }
        }

        //Điều kiện thắng
        if(player!=null && AICounter.HasInstance)
        {
            if (player.gameObject.CompareTag("Asian"))
            {
                if (AICounter.Instance.orcs.Length == 0
                    && AICounter.Instance.vikings.Length == 0
                    && AICounter.Instance.titans.Length == 0
                    && AICounter.Instance.tunguss.Length == 0
                    && !isWinGame)
                {
                    isWinGame = true;

                    if (gameTime != null)
                    {
                        gameTime.SetActive(false);//tắt đồng hồ để khi load lại scene sẽ reset giờ lại
                    }

                    StartCoroutine(WinLoadHomeScene());
                }
                else if(AICounter.Instance.orcs.Length != 0
                    && AICounter.Instance.vikings.Length != 0
                    && AICounter.Instance.tunguss.Length != 0
                    && AICounter.Instance.titans.Length != 0)
                {
                    isWinGame = false;
                }
            }
            if (player.gameObject.CompareTag("Viking"))
            {
                if (AICounter.Instance.orcs.Length == 0
                    && AICounter.Instance.asians.Length == 0
                    && AICounter.Instance.titans.Length == 0
                    && AICounter.Instance.tunguss.Length == 0
                    && !isWinGame)
                {
                    isWinGame = true;

                    if (gameTime != null)
                    {
                        gameTime.SetActive(false);//tắt đồng hồ để khi load lại scene sẽ reset giờ lại
                    }

                    StartCoroutine(WinLoadHomeScene());
                }
                else if(AICounter.Instance.orcs.Length != 0
                    && AICounter.Instance.asians.Length != 0
                    && AICounter.Instance.tunguss.Length != 0
                    && AICounter.Instance.titans.Length != 0)
                {
                    isWinGame = false;
                }
            }
            if (player.gameObject.CompareTag("Orc"))
            {
                if (AICounter.Instance.vikings.Length == 0
                    && AICounter.Instance.asians.Length == 0
                    && AICounter.Instance.titans.Length == 0
                    && AICounter.Instance.tunguss.Length == 0
                    && !isWinGame)
                {
                    isWinGame = true;

                    if (gameTime != null)
                    {
                        gameTime.SetActive(false);//tắt đồng hồ để khi load lại scene sẽ reset giờ lại
                    }

                    StartCoroutine(WinLoadHomeScene());
                }
                else if(AICounter.Instance.vikings.Length != 0
                    && AICounter.Instance.asians.Length != 0
                    && AICounter.Instance.tunguss.Length != 0
                    && AICounter.Instance.titans.Length != 0)
                {
                    isWinGame = false;
                }
            }
            if (player.gameObject.CompareTag("Tungus"))
            {
                if (AICounter.Instance.vikings.Length == 0
                    && AICounter.Instance.asians.Length == 0
                    && AICounter.Instance.titans.Length == 0
                    && AICounter.Instance.orcs.Length == 0
                    && !isWinGame)
                {
                    isWinGame = true;

                    if (gameTime != null)
                    {
                        gameTime.SetActive(false);//tắt đồng hồ để khi load lại scene sẽ reset giờ lại
                    }

                    StartCoroutine(WinLoadHomeScene());
                }
                else if(AICounter.Instance.vikings.Length != 0
                    && AICounter.Instance.asians.Length != 0
                    && AICounter.Instance.orcs.Length != 0
                    && AICounter.Instance.titans.Length != 0)
                {
                    isWinGame = false;
                }
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupPause>();
        }
    }
    private IEnumerator LoseLoadHomeScene()
    {
        yield return new WaitForSeconds(3f);//chờ 3s để play anim chết rồi mới hiện popuplose

        PlayerInputManager[] playerList = FindObjectsOfType<PlayerInputManager>();
        foreach (PlayerInputManager player in playerList)//tắt player để tắt UI thanh máu, chỉ số...
        {
            player.gameObject.SetActive(false);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupLose>();
        }

        yield return new WaitForSeconds(5f);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Loading");
        asyncOperation.allowSceneActivation = false;//khi scene chưa load xong thì chưa cho hiện
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (UIManager.HasInstance)
                {
                    UIManager.Instance.ShowOverlap<OverlapFade>();
                    OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
                    if (overlapFade != null)
                    {
                        overlapFade.Fade(3f,
                            onDuringFade: () =>
                            {
                                asyncOperation.allowSceneActivation = true;
                                if (UIManager.HasInstance)
                                {
                                    UIManager.Instance.ShowScreen<ScreenHome>();
                                    UIManager.Instance.HidePopup<PopupLose>();
                                }

                                if (AudioManager.HasInstance)
                                {
                                    AudioManager.Instance.PlayBGM(AUDIO.BGM_MENU);
                                }
                            },
                            onFinish: () =>
                            {

                            });
                    }
                }
                yield return new WaitForSeconds(3f);//nghĩa là trong 1s ko làm gì hết
            }
            yield return null;//để thoát ra khỏi vòng while
        }
    }
    private IEnumerator WinLoadHomeScene()
    {
        yield return new WaitForSeconds(3f);//chờ 3s để play anim chết rồi mới hiện popuplose

        PlayerInputManager[] playerList = FindObjectsOfType<PlayerInputManager>();
        foreach (PlayerInputManager player in playerList)//tắt player để tắt UI thanh máu, chỉ số...
        {
            player.gameObject.SetActive(false);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupWin>();
        }

        yield return new WaitForSeconds(5f);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Loading");
        asyncOperation.allowSceneActivation = false;//khi scene chưa load xong thì chưa cho hiện
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (UIManager.HasInstance)
                {
                    UIManager.Instance.ShowOverlap<OverlapFade>();
                    OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
                    if (overlapFade != null)
                    {
                        overlapFade.Fade(3f,
                            onDuringFade: () =>
                            {
                                asyncOperation.allowSceneActivation = true;
                                if (UIManager.HasInstance)
                                {
                                    UIManager.Instance.ShowScreen<ScreenHome>();
                                    UIManager.Instance.HidePopup<PopupWin>();
                                }

                                if (AudioManager.HasInstance)
                                {
                                    AudioManager.Instance.PlayBGM(AUDIO.BGM_MENU);
                                }
                            },
                            onFinish: () =>
                            {

                            });
                    }
                }
                yield return new WaitForSeconds(3f);//nghĩa là trong 1s ko làm gì hết
            }
            yield return null;//để thoát ra khỏi vòng while
        }
    }
    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("FiveTribes");
        asyncOperation.allowSceneActivation = false;//khi scene chưa load xong thì chưa cho hiện
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (UIManager.HasInstance)
                {
                    UIManager.Instance.ShowOverlap<OverlapFade>();
                    OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
                    if (overlapFade != null)
                    {
                        overlapFade.Fade(3f,
                            onDuringFade: () =>
                            {
                                asyncOperation.allowSceneActivation = true;
                            },
                            onFinish: () =>
                            {

                            });
                    }
                }
                yield return new WaitForSeconds(3f);//nghĩa là trong 1s ko làm gì hết
            }
            yield return null;//để thoát ra khỏi vòng while
        }
        if (CharacterSelect.HasInstance)
        {
            CharacterSelect.Instance.SetCharacter();
        }
    }

    public void LoadSceneGame()
    {
        StartCoroutine(LoadScene());
    }
}
