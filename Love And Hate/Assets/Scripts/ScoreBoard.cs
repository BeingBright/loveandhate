using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TMP_Text devilScore;
    [SerializeField] private TMP_Text cupidScore;

    [SerializeField] private Controller devil;
    [SerializeField] private Controller cupid;

    private List<NPC> _npcs;

    private void Start()
    {
        _npcs = GameObject.FindObjectsByType<NPC>(FindObjectsSortMode.None).ToList();
    }

    private void Update()
    {
        // var dScore = _npcs.Count(npc => npc.Side == Side.Devil);
        // var cScore = _npcs.Count(npc => npc.Side == Side.Cupid);

        devilScore.text = devil.Health.ToString();
        cupidScore.text = cupid.Health.ToString();
    }
}