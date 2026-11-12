using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "FightScene";
    public TMP_Text hintText;
    public TMP_Text p1Text;
    public TMP_Text p2Text;
    public Button playButton;
    public Image[] p1Highlights;   // one per character: Jane first, John second
    public Image[] p2Highlights;

    void Start()
    {
        CharacterSelection.Selected[0] = -1;
        CharacterSelection.Selected[1] = -1;
        Refresh();
    }

    public void SelectP1(int index) { CharacterSelection.Selected[0] = index; Refresh(); }
    public void SelectP2(int index) { CharacterSelection.Selected[1] = index; Refresh(); }

    public void StartGame()
    {
        if (!CharacterSelection.BothReady) return;
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit() { Application.Quit(); }

    void Refresh()
    {
        int a = CharacterSelection.Selected[0];
        int b = CharacterSelection.Selected[1];

        if (p1Text) p1Text.text = "Player 1: " + (a < 0 ? "choose a fighter" : CharacterSelection.Names[a]);
        if (p2Text) p2Text.text = "Player 2: " + (b < 0 ? "choose a fighter" : CharacterSelection.Names[b]);
        if (hintText) hintText.text = CharacterSelection.BothReady ? "Ready to fight" : "Both players choose a fighter";
        if (playButton) playButton.interactable = CharacterSelection.BothReady;

        for (int i = 0; i < p1Highlights.Length; i++)
            if (p1Highlights[i]) p1Highlights[i].enabled = (i == a);
        for (int i = 0; i < p2Highlights.Length; i++)
            if (p2Highlights[i]) p2Highlights[i].enabled = (i == b);
    }
}
