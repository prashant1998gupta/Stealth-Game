using UnityEngine;

public class CCTVPlayerDetection : MonoBehaviour
{
    public GameObject player;
    public LastPlayerSighting lastPlayerSighting;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log($" Nmaes {collider.gameObject.name} - {gameObject.name} ");

        if(collider.gameObject == player)
        {
            Vector3 relPosPlayer = player.transform.position - transform.position;
            RaycastHit hit;

            if(Physics.Raycast(transform.position , relPosPlayer , out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    lastPlayerSighting.position = player.transform.position;
                }
            }
        }
    }
}
