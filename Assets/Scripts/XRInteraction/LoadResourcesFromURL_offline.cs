//Step 1: Check assets directory for files
//Step 2: Compare assets with database. If an entry for an item exists load from given coordinates x, y, z. Else, make new DB entry (default values of x, y, and z = 0).
//Step 2.5: If there is an entry but no corresponding item, remove entry from DB. (?)
//Step 3: Load new items to the scene.
//Step 4: Write new coordinates of each item to the DB <-- only after UpdateRecords button has been pressed.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Globalization;

//namespace WebXR.Interactions
namespace UnityEngine.XR.Interaction.Toolkit
{
public class LoadResourcesFromURL_offline : MonoBehaviour
	{
		private string globalStringOfTruth = "";
		
		void Start(){
			string stringOfTruth = "Roll-Up_VR-Ausstellung_Mikwe_01_small300.png Roll-Up_VR-Ausstellung_Mikwe_02_small300.png Roll-Up_VR-Ausstellung_Mikwe_03_small300.png Roll-Up_VR-Ausstellung_Mikwe_04_small300.png Roll-Up_VR-Ausstellung_Mikwe_05_small300.png Roll-Up_VR-Ausstellung_Mikwe_06_small300.png Roll-Up_VR-Ausstellung_Mikwe_07_small300.png Roll-Up_VR-Ausstellung_Mikwe_08_small300.png Roll-Up_VR-Ausstellung_Mikwe_09_small300.png Roll-Up_VR-Ausstellung_Mikwe_10_small300.png Roll-Up_VR-Ausstellung_Mikwe_11_small300.png Roll-Up_VR-Ausstellung_Mikwe_12_small300.png Roll-Up_VR-Ausstellung_Mikwe_13_small300.png Roll-Up_VR-Ausstellung_Mikwe_14_small300.png";
			globalStringOfTruth = stringOfTruth;
			CreateMediaObjects(stringOfTruth);
		}
		void Update()
    	{

		}

		void CreateMediaObjects(string stringOfTruth){
			//Debug.Log("CreateMediaObjects");
			string[] arrayOfTruth = stringOfTruth.Split(' ');
			int mediaCounter = 0;
			foreach (var asset in arrayOfTruth){
				//exclude last empty item of the array
				if(asset.Length > 0){
					//Debug.Log(asset);

					GameObject myGO = new GameObject();
					//myGO.SetActive(false);
					myGO.name = asset;
					//myGO.tag = "Interactable";

					myGO.AddComponent<Canvas>();
					Canvas myCanvas = myGO.GetComponent<Canvas>();
					myCanvas.renderMode = RenderMode.WorldSpace;
					myCanvas.worldCamera = Camera.main;
					RectTransform rectTForm = myCanvas.GetComponent<RectTransform>();	//= new Vector2(1, 1);

					myGO.AddComponent<BoxCollider>();
					myGO.AddComponent(typeof(XRGrabInteractable));
					myGO.GetComponent<Rigidbody>().useGravity = false;
					myGO.GetComponent<Rigidbody>().isKinematic = true;

					myGO.AddComponent<RawImage>();
					RawImage myRawImage = myGO.GetComponent<RawImage>();

					//StartCoroutine(DownloadImage("http://localhost/assets/" + asset, myRawImage, rectTForm));
					StartCoroutine(DownloadImage("./assets/" + asset, myRawImage, rectTForm));
					//Debug.Log(asset);


					mediaCounter++;
				}
			}
			arrangeMediaObjects(stringOfTruth);
		}
		IEnumerator DownloadImage(string mediaUrl, RawImage rawImage2Bset, RectTransform rectTForm){
			UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
			yield return request.SendWebRequest();
			if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
				Debug.Log(request.error);
			}else{
				rawImage2Bset.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
			}
			rawImage2Bset.SetNativeSize();
			rectTForm.sizeDelta = rectTForm.sizeDelta/1000;
		}
		void arrangeMediaObjects(string stringOfTruth){
			string fullAssetsString = "Roll-Up_VR-Ausstellung_Mikwe_01_small300.png,-2.5,3.5,-3.5,0,270,0\tRoll-Up_VR-Ausstellung_Mikwe_02_small300.png,-2.5,3.5,-2.8,0,270,0\tRoll-Up_VR-Ausstellung_Mikwe_03_small300.png,2.6,3.5,-4.5,0,180,0\tRoll-Up_VR-Ausstellung_Mikwe_04_small300.png,1.9,3.5,-4.5,0,180,0\tRoll-Up_VR-Ausstellung_Mikwe_05_small300.png,-1.3,3.5,2,0,270,0\tRoll-Up_VR-Ausstellung_Mikwe_06_small300.png,-1.31,3.5,2.7,0,90,0\tRoll-Up_VR-Ausstellung_Mikwe_07_small300.png,-1.3,3.5,2.7,0,270,0\tRoll-Up_VR-Ausstellung_Mikwe_08_small300.png,3.5,3,-1,0,90,0\tRoll-Up_VR-Ausstellung_Mikwe_09_small300.png,-1.31,3.5,2,0,90,0\tRoll-Up_VR-Ausstellung_Mikwe_10_small300.png,0.5,3.5,-4.5,0,180,0\tRoll-Up_VR-Ausstellung_Mikwe_11_small300.png,2.8,3.5,4.5,0,0,0\tRoll-Up_VR-Ausstellung_Mikwe_12_small300.png,2.1,3.5,4.5,0,0,0\tRoll-Up_VR-Ausstellung_Mikwe_13_small300.png,1.4,3.5,4.5,0,0,0\tRoll-Up_VR-Ausstellung_Mikwe_14_small300.png,1.2,3.5,-4.5,0,180,0";
			string[] fullAssetsArray = fullAssetsString.Split('\t');
			GameObject currentObject;
			string newAssets = stringOfTruth;
			foreach (var fullAsset in fullAssetsArray){
				//Debug.Log(fullAsset);
				if(fullAsset == ""){
					//Debug.Log("empty String item");
				} else {
				string[] assetColumns = fullAsset.Split(",");
				currentObject = GameObject.Find(assetColumns[0]);
				currentObject.transform.localPosition = new Vector3(float.Parse(assetColumns[1], CultureInfo.InvariantCulture), float.Parse(assetColumns[2], CultureInfo.InvariantCulture), float.Parse(assetColumns[3], CultureInfo.InvariantCulture));
				Quaternion targetRot = Quaternion.Euler(float.Parse(assetColumns[4], CultureInfo.InvariantCulture), float.Parse(assetColumns[5], CultureInfo.InvariantCulture), float.Parse(assetColumns[6], CultureInfo.InvariantCulture));
				currentObject.transform.rotation = targetRot;
				newAssets = newAssets.Replace(assetColumns[0], "");
				}
			}

			string[] newAssetsArray = newAssets.Split(" ");
			
			int mediaCounter = 0;
			foreach (var newAsset in newAssetsArray){
				if(newAsset == ""){
					//Debug.Log("Empty String");
				} else {
					currentObject = GameObject.Find(newAsset);
					currentObject.transform.position = new Vector3((float)mediaCounter*1.5f,2.25f,2.5f);
					currentObject.transform.rotation = Quaternion.Euler(0,0,0);
				}
				mediaCounter++;
			}
		}
	}
}