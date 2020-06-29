using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject QuestPanel;

    public void Quest()
    {
        QuestPanel.SetActive(true);
        StartCoroutine(Disable(5, QuestPanel));

    }

    IEnumerator Disable(float time, GameObject gm)
    {
        GameObject gameObject = gm;
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
