using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //technique for making sure there isn't a null reference during runtime if you are going to use get component
public class Bullet : MonoBehaviour
{
  private Rigidbody2D myRigidbody2D;
  private Player mPlayer;
  private EnemyController mEnemyController;

  private Animator mAnimator;
  private GameObject destoryObject;

  public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
      myRigidbody2D = GetComponent<Rigidbody2D>();
      Fire();

      mPlayer = GameObject.Find("Player").GetComponent<Player>();

        if(mPlayer == null)
        {
            Debug.Log("Did not find player");
        }

       mEnemyController = GameObject.Find("Enemy").GetComponent<EnemyController>();


    }

    // Update is called once per frame
    private void Fire()
    {
      myRigidbody2D.velocity = Vector2.up * speed; 
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        var Object = collision.gameObject;
        var mBullet = GameObject.Find("Bullet(Clone)");
        if (Object.CompareTag("RedEnemy"))
        {
            mPlayer.addScore(1);
        }

        if (Object.CompareTag("GreenEnemy"))
        {
            mPlayer.addScore(5);
        }

        if (Object.CompareTag("BlueEnemy"))
        {
            mPlayer.addScore(8);
        }

        if (Object.CompareTag("YellowEnemy"))
        {
            mPlayer.addScore(10);
        }
        if (!Object.CompareTag("Player"))
        {
            mAnimator = Object.GetComponent<Animator>();
            mAnimator.SetTrigger("Dead");
            Object.SendMessage("PlaySound" );
            Destroy(mBullet);
        }

        if(mPlayer.getScore() > mPlayer.getSpeedUp())
        {

            if(mPlayer.getScore() > 0.1)
            {
                mEnemyController.setWaitTime(mEnemyController.getWaitTime() - 0.1f);
                mPlayer.setSpeedUpScore(mPlayer.getSpeedUp() + 8f);
            }

            //Debug.Log(mPlayer.getScore().ToString() + " " + mPlayer.getSpeedUp().ToString());
        }

    }




}
