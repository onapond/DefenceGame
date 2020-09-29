using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterBehavior : MonoBehaviour {

    private CharacterStat characterStat;
    private GameManager gameManager;

    public GameObject bullet;
    private Animator animator;
    private AudioSource audioSource;

    private GameObject bulletObjectPool;
    private ObjectPooler bulletobjectPooler;
    
	void Start () {
        characterStat = gameObject.GetComponent<CharacterStat>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        if(gameObject.name.Contains("Character 1"))
        {
            bulletObjectPool = GameObject.Find("Bullet1ObjectPool");
        }
        else if (gameObject.name.Contains("Character 2"))
        {
            bulletObjectPool = GameObject.Find("Bullet2ObjectPool");
        }
        bulletobjectPooler = bulletObjectPool.GetComponent<ObjectPooler>();
    }

    public void attack(int damage)
    {
        GameObject bullet = bulletobjectPooler.getObject();
        if (bullet == null) return;
        bullet.transform.position = gameObject.transform.position;
        bullet.GetComponent<BulletBehavior>().setDamage(damage);
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(audioSource.clip);
        bullet.GetComponent<BulletBehavior>().Spawn();
    }
	
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject(-1) == true) return;
        if (EventSystem.current.IsPointerOverGameObject(0) == true) return;
        if (characterStat.canLevelUp(gameManager.seed))
        {
            characterStat.increaseLevel();
            gameManager.seed -= characterStat.upgradeCost;
            gameManager.updateText();
        }
    }
}
