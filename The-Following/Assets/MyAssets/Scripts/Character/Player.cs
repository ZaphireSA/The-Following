using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour {

    public float detectVisualDistance = 1;
    public float detectHearingDistance = 0;
    List<FollowerAI> followers = new List<FollowerAI>();
    
    [SerializeField]
    float biteDistance = 0.5f;

    [SerializeField]
    GameObject followerPrefab;
    [SerializeField]
    float followerPosFactor = 2f;

    private void Start()
    {
        StartCoroutine(BiteEnemies());
        StartCoroutine(ControlFollowers());
    }

    IEnumerator ControlFollowers()
    {
        while (true)
        {
            float x = -3;
            float y = 1;
            for (int i = 0; i < followers.Count; i++)
            {
                x += 2;
                FollowerAI fol = followers[i];
                if (fol == null) continue;
                Vector3 targetPos = Vector3.zero;
                targetPos.z += y * followerPosFactor;
                targetPos.x += x * followerPosFactor;
                fol.SetTarget(transform.TransformPoint(-targetPos));

                

                if (x == y)
                {
                    y++;
                    x = -y -2;
                }
                
            }
            yield return new WaitForSeconds(0.2f);
        }
    }


    IEnumerator BiteEnemies()
    {
        while(true)
        {
            var enemyList = FindObjectsOfType<HumanAI>().Where(x => Vector3.Distance(transform.position, x.transform.position) < biteDistance).ToList();
            for (int i = 0; i < enemyList.Count; i++)
            {
                var newFollower = Instantiate(followerPrefab, enemyList[i].transform.position, Quaternion.identity);
                followers.Add(newFollower.GetComponent<FollowerAI>());
                Destroy(enemyList[i].gameObject);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

}
