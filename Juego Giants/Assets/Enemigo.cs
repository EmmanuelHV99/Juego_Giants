using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour {
	private NavMeshAgent cazador;
	private Animator animadorMalo;
	public Transform presa;
	private int vidaEnemigo =10;
	private AudioSource golpe;

	// Use this for initialization
	void Start () {
		cazador= GetComponent<NavMeshAgent>();
		animadorMalo = GetComponent<Animator>();
		golpe =GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs (presa.position.magnitude - cazador.gameObject.transform.position.magnitude)<5){
			cazador.SetDestination(presa.position);
			animadorMalo.SetInteger("anim",1);
		}else{
			animadorMalo.SetInteger("anim",0);
		}
	}

	void OnTriggerEnter(Collider trig){
		if(trig.tag=="bala"){
			vidaEnemigo--;
			golpe.Play();
			if(vidaEnemigo==0){
				Destroy(gameObject);
			}
		}
	}
}
