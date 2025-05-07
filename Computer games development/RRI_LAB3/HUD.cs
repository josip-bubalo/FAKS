using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public PlayerController playerController;
    private Text speedText;
    private Text stateText;

    void Start()
    {
        CreateCanvas();
    }

    void CreateCanvas()
    {
        // Kreiraj Canvas
        GameObject canvasGO = new GameObject("HUD Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Kreiraj pozadinski panel
        GameObject panelGO = new GameObject("Panel");
        panelGO.transform.SetParent(canvas.transform);

        // Postavi panel
        Image panelImage = panelGO.AddComponent<Image>();
        panelImage.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);

        RectTransform panelRT = panelGO.GetComponent<RectTransform>();
        panelRT.anchorMin = new Vector2(0, 1);
        panelRT.anchorMax = new Vector2(0, 1);
        panelRT.pivot = new Vector2(0, 1);
        panelRT.sizeDelta = new Vector2(200, 80);
        panelRT.anchoredPosition = new Vector2(10, -10);

        // Kreiraj tekstualne elemente
        CreateTextElements(panelGO.transform);
    }

    void CreateTextElements(Transform parent)
    {
        // Speed Text
        speedText = CreateText(parent, "SpeedText",
            new Vector2(10, 0),
            TextAnchor.UpperLeft,
            "Brzina: 0.00 m/s");

        // State Text
        stateText = CreateText(parent, "StateText",
            new Vector2(10, -20),
            TextAnchor.UpperLeft,
            "Stanje: Stoji");
    }

    Text CreateText(Transform parent, string name, Vector2 position, TextAnchor alignment, string initialText)
    {
        GameObject textGO = new GameObject(name);
        textGO.transform.SetParent(parent);

        RectTransform rt = textGO.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(180, 30);
        rt.anchoredPosition = position;

        Text textComponent = textGO.AddComponent<Text>();
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.fontSize = 18;
        textComponent.color = Color.red;
        textComponent.alignment = alignment;
        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
        textComponent.verticalOverflow = VerticalWrapMode.Overflow;
        textComponent.text = initialText;

        return textComponent;
    }

    void Update()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            return;
        }

        // Ažuriraj tekst
        speedText.text = $"Brzina: {playerController.GetCurrentSpeed():F2} m/s";
        stateText.text = $"Stanje: {playerController.GetPlayerState()}";
    }
}
