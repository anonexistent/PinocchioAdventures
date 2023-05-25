//  for event system

public class QuestionButton : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerDownHandler, UnityEngine.EventSystems.IPointerUpHandler
{
    public bool IsPressed { get; set; }
    public string answerText { get; set; }
    public void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        IsPressed = true;
    }

    public void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        IsPressed = false;
        answerText = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text;

    }
}
