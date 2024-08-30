using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using Spawner;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TMP_Text devilScore;
    [SerializeField] private TMP_Text cupidScore;

    [SerializeField] private Controller devil;
    [SerializeField] private Controller cupid;

    [SerializeField] private Image devilImage;
    [SerializeField] private Image cupidImage;

    [SerializeField] private SpawnController spawnController;

    private List<NPC> _npcs;

    private void Start()
    {
        _npcs = GameObject.FindObjectsByType<NPC>(FindObjectsSortMode.None).ToList();
    }

    private void Update()
    {
        var dScore = (float)_npcs.Count(npc => npc.Side == Side.Devil) / (float)spawnController.maxCount;
        var cScore = (float)_npcs.Count(npc => npc.Side == Side.Cupid) / (float)spawnController.maxCount;

        devilImage.fillAmount = Mathf.Clamp01(dScore);
        cupidImage.fillAmount = Mathf.Clamp01(cScore);

        devilScore.text = devil.Health.ToString();
        cupidScore.text = cupid.Health.ToString();

        if (cupid.Health <= 0)
        {
            SceneManager.LoadScene("DevilWin");
        }
        else if (devil.Health <= 0)
        {
            SceneManager.LoadScene("CupidWin");
        }
    }
}