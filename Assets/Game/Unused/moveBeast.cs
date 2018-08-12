using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBeast : MonoBehaviour {

 //   public float moveSpeed;
 //   public moveCamera _moveCamera;
 //   public Sprite[] sprites;
 //   public SpriteRenderer spriteRen;
 //   bool awake = false;
 //   bool firstEncounter = true;
 //   Vector2 startPos;

 //   private void Start()
 //   {
 //       startPos = transform.position;
 //   }

 //   // Update is called once per frame
 //   void FixedUpdate () {
 //       if (awake)
 //       {
 //           transform.Translate(-moveSpeed / 100, 0, 0);
 //       }
	//}

 //   private IEnumerator OnTriggerEnter2D(Collider2D collision)
 //   {
 //       if (collision.gameObject.tag == "Player")
 //       {
 //           if (firstEncounter)
 //           {
 //               firstEncounter = false;
 //               movePlayer.canmove = false;
 //               spriteRen.sprite = sprites[1];
 //               yield return new WaitForSeconds(2);
 //               spriteRen.sprite = sprites[0];
 //               yield return new WaitForSeconds(.1f);
 //               spriteRen.sprite = sprites[1];
 //               yield return new WaitForSeconds(1);
 //               spriteRen.sprite = sprites[0];
 //               yield return new WaitForSeconds(.1f);
 //               spriteRen.sprite = sprites[1];
 //               yield return new WaitForSeconds(1);
 //               movePlayer.canmove = true;
 //               spriteRen.sprite = sprites[2];
 //               yield return new WaitForSeconds(2);
 //               awake = true;
 //               StartCoroutine(animateRun());
 //           }
 //           else
 //           {

 //               spriteRen.sprite = sprites[2];
 //               _moveCamera.disallowMovement();
 //               collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
 //               collision.transform.position = movePlayer.startPos;
 //               yield return new WaitForSeconds(1.5f);
 //               _moveCamera.allowedToMove = true;
 //               transform.position = startPos;
 //               awake = false;
 //               firstEncounter = true;
 //               spriteRen.sprite = sprites[0];
 //           }
 //       }
 //   }
 //   IEnumerator animateRun()
 //   {
 //       while (awake)
 //       {
 //           spriteRen.sprite = sprites[3];
 //           yield return new WaitForSeconds(.2f);
 //           spriteRen.sprite = sprites[2];
 //           yield return new WaitForSeconds(1);
 //       }
 //   }

 //   public void guardEnd()
 //   {
 //       awake = false;
 //       spriteRen.sprite = sprites[1];
 //       transform.localScale = new Vector3(-1, 1, 1);
 //   }

 //   public IEnumerator die()
 //   {
 //       spriteRen.sprite = sprites[2];
 //       yield return new WaitForSeconds(2);
 //       Destroy(gameObject);
 //   }
}
