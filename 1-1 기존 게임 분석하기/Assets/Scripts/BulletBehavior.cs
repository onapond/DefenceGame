using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
     

    public float speed = 10.0f;
    public GameObject character;

    public float activeTime = 3.0f;
    

    private int damage;

    public void setDamage(int input)
    {
        damage = input;
    }

    private void OnEnable()//활성화되었을 때 콜백
    {
        StartCoroutine(BulletInactive(activeTime));
    }

    IEnumerator BulletInactive (float activeTime)
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
    public void Spawn()
    {
        gameObject.SetActive(true);       
    }
		
	void Update () {
                   transform.Translate(Vector2.right * speed * Time.deltaTime);
       
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            gameObject.SetActive(false);
            other.GetComponent<MonsterStat>().attacked(damage);
        }   
    }
}
