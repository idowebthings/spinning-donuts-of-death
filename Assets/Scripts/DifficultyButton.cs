using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private ProductivityTracker productivityTracker;
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        productivityTracker = GameObject.Find("Productivity Tracker").GetComponent<ProductivityTracker>();
    }

    void SetDifficulty() {
        productivityTracker.StartGame(difficulty);
    }
}
