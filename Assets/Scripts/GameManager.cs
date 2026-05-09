using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Letter Prefabs")]
    public Shape[] letterPrefabs;

    [Header("Spawn")]
    public Transform spawnParent;

    [Header("UI")]
    public TextMeshProUGUI targetText;

    [Header("Colors")]
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;

    string targetLetter;

    int wave = 1;
    int stars = 0;

    List<Shape> activeBalloons = new List<Shape>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        Debug.Log("Wave: " + wave);

        // RANDOM TARGET
        Shape targetPrefab =
            letterPrefabs[Random.Range(0, letterPrefabs.Length)];

        targetLetter = targetPrefab.gameObject.name;

        targetText.text = "Catch: " + targetLetter;

        // UNIQUE LETTER SELECTION
        List<Shape> selectedPrefabs = new List<Shape>();

        selectedPrefabs.Add(targetPrefab);

        while (selectedPrefabs.Count < 4)
        {
            Shape randomPrefab =
                letterPrefabs[Random.Range(0, letterPrefabs.Length)];

            if (!selectedPrefabs.Contains(randomPrefab))
            {
                selectedPrefabs.Add(randomPrefab);
            }
        }

        Shuffle(selectedPrefabs);

        // SPAWN
        for (int i = 0; i < 4; i++)
        {
            Shape spawned =
                Instantiate(selectedPrefabs[i], spawnParent);

            spawned.name = selectedPrefabs[i].name;

            RectTransform rect =
                spawned.GetComponent<RectTransform>();

            rect.anchoredPosition =
                new Vector2(-300 + (i * 200), -500);

            activeBalloons.Add(spawned);
        }
    }

    public void CheckClick(Shape clicked)
    {
        if (clicked.gameObject.name == targetLetter)
        {
            clicked.outline.enabled = true;
            clicked.outline.color = correctColor;

            AudioManager.Instance.PlayWin();

            stars++;

            Debug.Log("Stars: " + stars);

            Invoke(nameof(NextWave), 1f);
        }
        else
        {
            clicked.outline.enabled = true;
            clicked.outline.color = wrongColor;

            AudioManager.Instance.PlayLose();
        }
    }

    void NextWave()
    {
        foreach (Shape balloon in activeBalloons)
        {
            Destroy(balloon.gameObject);
        }

        activeBalloons.Clear();

        wave++;

        if (wave > 10)
        {
            Debug.Log("GAME FINISHED");
            return;
        }

        StartWave();
    }

    void Shuffle(List<Shape> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random =
                Random.Range(i, list.Count);

            Shape temp = list[i];
            list[i] = list[random];
            list[random] = temp;
        }
    }
}