using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour {

    public float detectVisualDistance = 1;
    public float detectHearingDistance = 0;
    [SerializeField]
    float biteDistance = 0.5f;

    [SerializeField]
    GameObject followerPrefab;

    private void Start()
    {
        StartCoroutine(BiteEnemies());
    }


    IEnumerator BiteEnemies()
    {
        while(true)
        {
            var enemyList = FindObjectsOfType<HumanAI>().Where(x => Vector3.Distance(transform.position, x.transform.position) < biteDistance).ToList();
            for (int i = 0; i < enemyList.Count; i++)
            {
                var newFollower = Instantiate(followerPrefab, enemyList[i].transform.position, Quaternion.identity);
                Destroy(enemyList[i].gameObject);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

}
