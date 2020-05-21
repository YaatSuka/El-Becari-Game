using UnityEngine;
using UnityEngine.EventSystems;

using Command;

public class DropHandler: MonoBehaviour, IDropHandler
{
    public int id;
    public GameObject token;

    private CommandFactory factory;

    void Start()
    {
        factory = new CommandFactory();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) {
            transform.parent.GetComponent<CommandQueue>().UpdateQueue(eventData.pointerDrag, gameObject);
            /* if (eventData.pointerDrag.transform.parent.name == "CommandList") {
                Debug.Log("CommandList -> CommandQueue");
                if (token == null) {
                    Debug.Log("Insert");
                    this.DropNew(eventData.pointerDrag, new Vector2(0, 0));
                } else {
                    Debug.Log("Switch");
                    Debug.Log(eventData.pointerDrag.transform.parent.name);
                }
            }
            if (eventData.pointerDrag.transform.parent.name == "CommandQueue") {
                Debug.Log("CommandQueue -> CommandQueue");
            } */
        }
    }

    public void Drop(GameObject token)
    {
        this.token = token;
        this.token.transform.SetParent(transform);
        this.token.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        this.token.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        this.token.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.token.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void DropNew(GameObject source)
    {
        this.token = Instantiate(source);
        this.token .GetComponent<CommandComponent>().command = factory.Build(source.GetComponent<CommandComponent>().command.name);
        this.token.transform.SetParent(transform);
        this.token.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        this.token.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        this.token.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.token.GetComponent<CanvasGroup>().alpha = 1f;
    }
}