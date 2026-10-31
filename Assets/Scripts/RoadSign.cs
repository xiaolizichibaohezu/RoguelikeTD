using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSign : MonoBehaviour
{
    [SerializeField]private int _signId = 0;
    public RoadSign nxtSign = null;

    public int signId
    {
        get { return _signId; }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && nxtSign != null)
            {
                enemy.SetRoadSign(nxtSign);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
