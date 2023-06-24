using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Personaje : MonoBehaviour {
	private int vel = 5, giro=150, fuerza=300, contador=0, anim=0, nivelActual;
	private bool salto=false;
	private Rigidbody rb;
	public Text tVida, tContador;
	public Slider barraVida;
	public Button bInicio, bJugar, bFin;
	private Animator animador;
	private AudioSource[] sonidos;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		sonidos = GetComponents<AudioSource>();
		bInicio.gameObject.SetActive(false);
		bJugar.gameObject.SetActive (false);
		bFin.gameObject.SetActive(false);
		animador = GetComponent<Animator>();
		nivelActual=SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
		//Mover nave hacia adelante
		if (Input.GetKey(KeyCode.W)) {
			transform.Translate (new Vector3 (0, 0, vel) * Time.deltaTime);
		}
		//Mover nave hacia atras
		if (Input.GetKey(KeyCode.S)) {
			transform.Translate (new Vector3 (0, 0, -vel) * Time.deltaTime);
		}
		//Mover nave hacia izquierda
		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate (new Vector3 (0, -giro, 0) * Time.deltaTime);
		}
		//Mover nave hacia derecha
		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate (new Vector3 (0, giro, 0) * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.W)|Input.GetKey(KeyCode.S)|Input.GetKey(KeyCode.A)|Input.GetKey(KeyCode.D)){
			animador.SetInteger("anim",1);
		}else{
			animador.SetInteger("anim",0);
		}
		//Saltar
		if((Input.GetKeyDown(KeyCode.Space)|ControlSalto.saltoVirtual)&salto){
			rb.AddForce(0,fuerza,0);
			salto=false;
			sonidos[1].Play();
			animador.SetInteger("anim",2);
		}
		ControlVirtualPersonaje ();

	}//Fin del update

	void ControlVirtualPersonaje(){
		//Verificar si el control virtual se mueve hacia arriba o abajo
		if(ControlVirtual.inputVector.z!=0){
			animador.SetInteger("anim",1);
			transform.Translate (new Vector3 (0, 0,ControlVirtual.inputVector.z*vel) * Time.deltaTime);
		}
		//Verificar si el control virtual se mueve hacia izquierda o derecha
		if(ControlVirtual.inputVector.x!=0){
			animador.SetInteger("anim",1);
			transform.Rotate (new Vector3 (0, ControlVirtual.inputVector.x*giro, 0) * Time.deltaTime);
		}
	}//Fin de ControlnaveVirtual

	void OnCollisionEnter(Collision col){
		salto=true;
		if(col.gameObject.tag=="malo"){
			sonidos[0].Play();
			barraVida.value -=100;
			tVida.text=""+(int)(barraVida.value/5)+"%";
			if(barraVida.value==0){
				SceneManager.LoadScene(nivelActual);
			}
		}

		if(col.gameObject.tag=="enemigo"){
			sonidos[4].Play();
			barraVida.value -= 150;
			tVida.text=""+(int)(barraVida.value/5)+"%";
			if(barraVida.value==0){
				SceneManager.LoadScene(nivelActual);
			}
		}

		if(col.gameObject.tag=="falsa"){
			SceneManager.LoadScene(nivelActual);
		}

		if(col.gameObject.tag=="moneda"){
			sonidos[5].Play();
			contador++;
			tContador.text=""+contador;
			Destroy(col.gameObject);
			if(contador==10){
				bInicio.gameObject.SetActive(true);
				bJugar.gameObject.SetActive (true);
				bFin.gameObject.SetActive(true);
			}
		}
		if(col.gameObject.tag=="capsula"){
			sonidos[6].Play();
			barraVida.value +=150;
			tVida.text=""+(int)(barraVida.value/5)+"%";
			Destroy(col.gameObject);
		}
	}

	void OnTriggerStay(Collider trig){
		if(trig.tag=="pbuenas"){
			barraVida.value+=2;
			tVida.text=""+(int)(barraVida.value/5)+"%";
		}
		if(trig.tag=="pmalas"){
			barraVida.value--;
			tVida.text=""+(int)(barraVida.value/5)+"%";
			if(barraVida.value==0){
				SceneManager.LoadScene(1);
			}
		}
		
	}

	void OnTriggerEnter(Collider trig){
		if(trig.tag=="malo"){
			sonidos[0].Play();
			barraVida.value -=100;
			tVida.text=""+(int)(barraVida.value/5)+"%";
			if(barraVida.value==0){
				SceneManager.LoadScene(nivelActual);
			}
		}
		if (trig.tag == "pbuenas") {
			sonidos [2].Play ();
		}//sonido particulas buenas
		if (trig.tag == "pmalas") {
			sonidos [3].Play ();
		}//sonido particulas malas
	}
	
}
