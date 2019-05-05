using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<Player> players;
    public GameObject m_announcement;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Player p in players) {
            if (p.m_currentHealth <= 0)
            {
                EndGame(p);
            }
        }
    }

    public void HandleServerMessage(string message)
    {
        // Message structure should be:
        // p[1,2]-[atk1,atk2,def]
        Regex rx = new Regex(@"(shield|flick|throw)\~(1|2)");
        MatchCollection matches = rx.Matches(message);
        Match match = matches[0];
        int player = int.Parse(match.Groups[2].Value);
        string ability = match.Groups[1].Value;
        players[player - 1].AbilityQueue.Enqueue(ability);
        Debug.Log(string.Format("Enqueued: {0} command for player {1}", ability, player));
    }

    public void EndGame(Player loser)
    {
        m_announcement.GetComponent<Text>().text = string.Format("Player {0} wins!", players.Find((obj) => obj.m_playerNumber != loser.m_playerNumber).m_playerNumber);
        m_announcement.SetActive(true);
    }
}
