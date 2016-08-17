using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// http://stackoverflow.com/questions/7120845/equivalent-of-tuple-net-4-for-net-framework-3-5
public class Tuple<T1, T2>
{
	public T1 First { get; private set; }
	public T2 Second { get; private set; }
	internal Tuple(T1 first, T2 second)
	{
		First = first;
		Second = second;
	}
}

public static class Tuple
{
	public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
	{
		var tuple = new Tuple<T1, T2>(first, second);
		return tuple;
	}
}


public class Rotation : MonoBehaviour {

	public GameObject card00;
	public GameObject card01;
	public GameObject card02;
	public GameObject card03;
	private GameObject card;
	private Vector3 vector;
	private List<Tuple<Vector3, float>> unitCirclePos;

	private bool randomlySorted;
	private List<Tuple<Vector3, float>> shuffledCards;
	private List<Tuple<Vector3, float>> shuffledCardsWithRemoval;

	// Use this for initialization

	void Start () {

		// Initialize list to store positions and heights
		unitCirclePos = new List<Tuple<Vector3, float>> ();
		// Initialize variables used to calculate position.
		float theta = 0;
		float radius = (float)3;
		float xPos = 3;
		float height = 0;
		float zPos = 0;
		Vector3 xzPos = new Vector3 (xPos, height, zPos);
		// Initialize variable used to store position and height
		var posAndHeight = Tuple.New (xzPos, height);;
		// Add first card's position and height to list
		unitCirclePos.Add (posAndHeight);

		for (int i = 1; i < 52; i++) {

			// Calculate angle around sphere
			theta = (2 * Mathf.PI / 52) * i;
			// Calculate x position
			xPos = Mathf.Cos (theta) * radius;
			// Calculate y position
			height = Random.Range(-2.5f, 2.5f);
			// Calculate z position
			zPos = Mathf.Sin (theta) * radius;
			// Add position and height to list
			xzPos = new Vector3 (xPos, 0, zPos);
			posAndHeight = Tuple.New(xzPos, height);
			unitCirclePos.Add (posAndHeight);


			// CREATE CLONE AND ASSIGN TO JUST CALCULATED POSITION.

			if (i < 13) {
				card = card00;
			} else if (i < 26) {
				card = card01;
			} else if (i < 39) {
				card = card02;
			} else {
				card = card03;
			}
			// create clone
			GameObject clonedObject = Instantiate (card, xzPos, Quaternion.identity) as GameObject;
			// name clone
			clonedObject.name = card.name + "_" + i.ToString ();
			// scale the card to the right size
			clonedObject.transform.localScale = new Vector3(10, 10, 10);
			// Face camera
			clonedObject.transform.LookAt (Vector3.zero);
			// Turn 180 so front of card is facing camera
			clonedObject.transform.RotateAround(Vector3.zero, Vector3.up, 180);
			// Apply height
			clonedObject.transform.position = new Vector3 (xPos, height, zPos);

		}

		randomlySorted = false;
			

	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.R)) {

			if (randomlySorted == false) {

				// shuffle cards into random order -- working
				shuffledCardsWithRemoval = unitCirclePos.OrderBy (a => System.Guid.NewGuid ()).ToList ();;

				// find first card
				GameObject currentClone = GameObject.Find (card00.name);
				// get angle and height of first card
				Vector3 xzPos = shuffledCardsWithRemoval [0].First;
				float height = Random.Range(-2.5f, 2.5f);
				shuffledCardsWithRemoval.RemoveAt (0);
		
				// Move position
				currentClone.transform.position = xzPos;
				float xPos = currentClone.transform.position.x;
				float zPos = currentClone.transform.position.z;
				iTween.MoveTo(currentClone, new Vector3(xPos,height,zPos), 5);

				// Face camera
				currentClone.transform.LookAt (Vector3.zero);
				// Turn 180 so front of card is facing camera
				currentClone.transform.RotateAround (Vector3.zero, Vector3.up, 180);

				for (int i = 1; i < 52; i++) {

					xzPos = shuffledCardsWithRemoval [0].First;
					height = Random.Range(-2.5f, 2.5f);
					shuffledCardsWithRemoval.RemoveAt (0);

					if (i < 13) {
						card = card00;
					} else if (i < 26) {
						card = card01;
					} else if (i < 39) {
						card = card02;
					} else {
						card = card03;
					}
					string cloneName = card.name + "_" + i.ToString ();
					currentClone = GameObject.Find (cloneName);

					// Move x and z positions
					currentClone.transform.position = xzPos;
					xPos = currentClone.transform.position.x;
					zPos = currentClone.transform.position.z;
					iTween.MoveTo(currentClone, new Vector3(xPos,height,zPos), 5);
					// Face camera
					currentClone.transform.LookAt (Vector3.zero);
					// Turn 180 so front of card is facing camera
					currentClone.transform.RotateAround (Vector3.zero, Vector3.up, 180);

				}

				randomlySorted = true;

			} else {

				// find first card
				GameObject currentClone = GameObject.Find (card00.name);
				// get angle and height of first card
				Vector3 xzPos = unitCirclePos [0].First;
				float height = unitCirclePos [0].Second;

				// Move x and z positions
				currentClone.transform.position = xzPos;
				float xPos = currentClone.transform.position.x;
				float zPos = currentClone.transform.position.z;
				iTween.MoveTo(currentClone, new Vector3(xPos,height,zPos), 5);
				// Face camera
				currentClone.transform.LookAt (Vector3.zero);
				// Turn 180 so front of card is facing camera
				currentClone.transform.RotateAround (Vector3.zero, Vector3.up, 180);

				for (int i = 1; i < 52; i++) {

					xzPos = unitCirclePos [i].First;
					height = unitCirclePos [i].Second;

					if (i < 13) {
						card = card00;
					} else if (i < 26) {
						card = card01;
					} else if (i < 39) {
						card = card02;
					} else {
						card = card03;
					}
					string cloneName = card.name + "_" + i.ToString ();
					currentClone = GameObject.Find (cloneName);

					// Move x and z positions
					currentClone.transform.position = xzPos;
					xPos = currentClone.transform.position.x;
					zPos = currentClone.transform.position.z;
					iTween.MoveTo(currentClone, new Vector3(xPos,height,zPos), 5);
					// Face camera
					currentClone.transform.LookAt (Vector3.zero);
					// Turn 180 so front of card is facing camera
					currentClone.transform.RotateAround (Vector3.zero, Vector3.up, 180);

				}
					
				randomlySorted = false;

			}

		}


		if(Input.GetKey(KeyCode.Space)){
			// Rotate all cards around sphere
			GameObject.Find(card00.name).transform.RotateAround(Vector3.zero, Vector3.up, (float)1);
			for (int i = 1; i < 52; i++) {
				if (i < 13) {
					card = card00;
				} else if (i < 26) {
					card = card01;
				} else if (i < 39) {
					card = card02;
				} else {
					card = card03;
				}
				string cloneName = card.name + "_" + i.ToString ();
				GameObject.Find(cloneName).transform.RotateAround(Vector3.zero, Vector3.up, (float)1);
			}	
		}
			
			
	}


}
