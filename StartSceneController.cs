using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public string MainGame;

    // Declare UnityEvent in the Inspector
    public UnityEvent onClickEvent;

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // Attach the UnityEvent to the button's onClick event
            button.onClick.AddListener(() => onClickEvent.Invoke());
        }
        

    }

    // Method to change the scene
    public void ChangeScene()
    {
        SceneManager.LoadScene(MainGame);
    }
}
