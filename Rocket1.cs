using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket1 : MonoBehaviour {
	public float thrust;
	public GameObject targetPad;
	public string level;
	Rigidbody _rigidbody;
	AudioSource Sound;
	bool soundplaying = false;
	bool bump = false;
	int stopcollision = 0;
	float distance;
	float soclose = 10f;

	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
		Sound = GetComponent<AudioSource>();
	}

	void Update () {
		distance = Vector3.Distance(gameObject.transform.position,targetPad.transform.position);
		ProcessInput();
	}

	private void ProcessInput(){

		float rcsThrust = 100f;
		float rotation = rcsThrust * Time.deltaTime;

		if(distance <= soclose && !bump){
			targetPad.GetComponent<Renderer>().material.color = Color.yellow;
		}else if(bump){
			if (level == "0"){
				targetPad.GetComponent<Renderer>().material.color = Color.red;
			}
			else if (level == "1"){
				targetPad.GetComponent<Renderer>().material.color = Color.red;
			}
			else if (level == "2"){
				targetPad.GetComponent<Renderer>().material.color = Color.red;
			}else{
				targetPad.GetComponent<Renderer>().material.color = Color.green;
			}
		}

		if (Input.GetKeyDown(KeyCode.O)){
			if(stopcollision == 0){
				stopcollision = 1;
				print("collision off");
			}else{
				stopcollision = 0;
				print("collision on");
			}
		}

		if (Input.GetKey(KeyCode.Space)){
            print("Up");
			_rigidbody.AddRelativeForce(Vector3.up * thrust);
			if (!soundplaying && Input.GetKeyDown(KeyCode.Space))
            {
                soundplaying = true;
				Sound.Play();
            }
        }
		else if(Input.GetKeyUp(KeyCode.Space)){
			Sound.Pause();
			soundplaying = false;
		}
		
		if (Input.GetKey(KeyCode.A)){
            print("Left");
			transform.Rotate(Vector3.forward * rotation);
		}

		if (Input.GetKey(KeyCode.D)){
            print("Right");
			transform.Rotate(-Vector3.forward);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(stopcollision == 0){
			if(collision.gameObject.tag == "Obstacle"){ 
				SceneManager.LoadScene("Level1");    
				Debug.Log("Collided with: " + collision.gameObject.name);
			}
		}

			if(collision.gameObject.tag == "target"){
				bump = true;
				if (level == "0"){
					collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
				}
				else if (level == "1"){
					collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
					SceneManager.LoadScene("Level2");
				}
				else if (level == "2"){
					collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
					SceneManager.LoadScene("Level3");
				}else{
					collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
				}
				
				print("You Won~!!");
				Debug.Log("Triggered with: " + collision.gameObject.name);
			}
	}

}
