/**
 * Project: CPSC 362 Project (Spring 2022)
 * 
 * File: read.cs
 * Programmer: florentino Becerra
 * Date: 05/10/2022
 * Revised: 05/12/2022
 * 
 * Description: This class will be responsible for retrieving data from
 * a cloud database provided by Flushy Industries
 * 
 * TODO: Ensure game is built against Android or iOS for testing
 */

using System;
using Systems.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;


public class ReadDatabase : MonoBehaviour
{
	// Initialize an instance of firestore
	FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
	CollectionReference reviewsRef = db.Collection("Reviews");


	/**  Start
	 * This function is responsible for an attempt
	 * to read some of the data from a Firestore database
	 * 
	 * @return: void
	 */

	void Start()
	{
		reviewsRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
		{
			QuerySnapshot snapshot = task.Result;
			foreach (DocumentSnapshot document in snapshot.Documents)
			{
				Dictionary<string, object> documentDictionary = document.ToDictionary();
				if (documentDictionary.ContainsKey("rate"))
				{
					Debug.Log(String.Format("rate: {0}", documentDictionary["rate"]));
				}
		}
	});

	}

}  // End of class
