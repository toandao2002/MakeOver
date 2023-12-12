using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoSceneController : MonoBehaviour
{
    [SerializeField] private Image fade;

    // Start is called before the first frame update
    void Start()
    {
        fade.color = Color.black;
        DOTween.Sequence()
            .Append(fade.DOColor(Color.clear, .3f))
            .AppendInterval(3)
            .Append(fade.DOColor(Color.black, .3f))
            .AppendCallback(() => SceneManager.LoadSceneAsync((int) SceneIndex.Splash));
    }
}
