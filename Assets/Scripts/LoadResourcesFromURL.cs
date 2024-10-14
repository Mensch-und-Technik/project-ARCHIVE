//Step 1: Check assets directory for files
//Step 2: Compare assets with database. If an entry for an item exists load from given coordinates x, y, z. Else, make new DB entry (default values of x, y, and z = 0).
//Step 2.5: If there is an entry but no corresponding item, remove entry from DB. (?)
//Step 3: Load new items to the scene.
//Step 4: Write new coordinates of each item to the DB.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Globalization;

namespace WebXR.Interactions
{
public class LoadResourcesFromURL : MonoBehaviour
	{
		private string globalStringOfTruth = "";
		public WebXRController physicalController;
		public Camera m_camera;

		
		void Start(){
			//Coroutine GetAssetsList() starts coroutine UpdateRecords() + calls CreateMediaObjects() which again starts coroutine DownloadImage() 
			StartCoroutine(GetAssetsList("http://localhost/listAssets.php"));
			//StartCoroutine(GetAssetsList("./listAssets.php"));
		}
		void Update()
    {
		if(physicalController.GetButton(WebXRController.ButtonTypes.ButtonA)){
			Debug.Log("Button A was pressed");

			//m_camera.GetComponent<WebXRCameraSettings>().VRClearFlags = CameraClearFlags.SolidColor;
          	//m_camera.GetComponent<WebXRCameraSettings>().VRBackgroundColor = Color.clear;

			m_camera.clearFlags = CameraClearFlags.SolidColor;
        	m_camera.backgroundColor = Color.clear;
			
			StartCoroutine(UpdateRecords(globalStringOfTruth));
		};
		if (Input.touchCount > 0)
        {
			Debug.Log("Screen touched");
			
			//m_camera.GetComponent<WebXRCameraSettings>().ARClearFlags = CameraClearFlags.SolidColor;
			//m_camera.GetComponent<WebXRCameraSettings>().ARBackgroundColor = Color.clear;

			m_camera.clearFlags = CameraClearFlags.SolidColor;
        	m_camera.backgroundColor = Color.clear;

			StartCoroutine(UpdateRecords(globalStringOfTruth));
		}
        if (Input.GetKeyUp("space"))
        {
            Debug.Log("Space key was released");

			//m_camera.GetComponent<WebXRCameraSettings>().NormalClearFlags = CameraClearFlags.SolidColor;
			//m_camera.GetComponent<WebXRCameraSettings>().NormalBackgroundColor = Color.clear;

			m_camera.clearFlags = CameraClearFlags.SolidColor;
        	m_camera.backgroundColor = Color.clear;
			
			StartCoroutine(UpdateRecords(globalStringOfTruth));
        }
    }
		IEnumerator GetAssetsList(string uri)
		{
			using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
			{
				// Request and wait for the desired page.
				yield return webRequest.SendWebRequest();

				string[] pages = uri.Split('/');
				int page = pages.Length - 1;

				switch (webRequest.result)
				{
					case UnityWebRequest.Result.ConnectionError:
					case UnityWebRequest.Result.DataProcessingError:
						Debug.LogError(pages[page] + ": Error: " + webRequest.error);
						break;
					case UnityWebRequest.Result.ProtocolError:
						Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
						break;
					case UnityWebRequest.Result.Success:
						//Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
						string stringOfTruth = webRequest.downloadHandler.text;
						globalStringOfTruth = stringOfTruth;
						//string[] arrayOfTruth = stringOfTruth.Split(' ');
						CreateMediaObjects(stringOfTruth);
						//StartCoroutine(ReadRecords(stringOfTruth));
						//StartCoroutine(UpdateRecords(stringOfTruth));
						break;
				}
			}
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
					myGO.tag = "Interactable";
					//myGO.transform.position = new Vector3((float)mediaCounter*1.5f,2.25f,2.5f);
					//myGO.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
					myGO.transform.SetParent(GameObject.Find("Interactables").transform);

					myGO.AddComponent<Canvas>();
					//myGO.AddComponent<CanvasScaler>();
					//myGO.AddComponent<GraphicRaycaster>();
					Canvas myCanvas = myGO.GetComponent<Canvas>();
					myCanvas.renderMode = RenderMode.WorldSpace;
					myCanvas.worldCamera = Camera.main;
					RectTransform rectTForm = myCanvas.GetComponent<RectTransform>();	//= new Vector2(1, 1);

					myGO.AddComponent<BoxCollider>();
					//TO-DO: set Box Collider Size 1,1,0
					myGO.AddComponent(typeof(MouseDragObject));
					myGO.GetComponent<Rigidbody>().useGravity = false;
					myGO.GetComponent<Rigidbody>().isKinematic = true;

					myGO.AddComponent<RawImage>();
					RawImage myRawImage = myGO.GetComponent<RawImage>();

					//StartCoroutine(DownloadImage("./assets/" + asset, myRawImage));
					StartCoroutine(DownloadImage("http://localhost/assets/" + asset, myRawImage, rectTForm));
					//Debug.Log(asset);

					StartCoroutine(ReadRecords(stringOfTruth));

					mediaCounter++;
				}
			}
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
		IEnumerator ReadRecords(string stringOfTruth){
			//Create a Web Form
			WWWForm form = new WWWForm();
			form.AddField("task", "ReadRecords");
			form.AddField("stringOfTruth", stringOfTruth);

			//string uri = "./UnityDBcommunication.php";
			string uri = "http://localhost/UnityDBcommunication.php";
			using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
			{
				// Request and wait for the desired page.
				yield return webRequest.SendWebRequest();

				string[] pages = uri.Split('/');
				int page = pages.Length - 1;

				switch (webRequest.result)
				{
					case UnityWebRequest.Result.ConnectionError:
					case UnityWebRequest.Result.DataProcessingError:
						Debug.LogError(pages[page] + ": Error: " + webRequest.error);
						break;
					case UnityWebRequest.Result.ProtocolError:
						Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
						break;
					case UnityWebRequest.Result.Success:
						//Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
						string fullAssetsString = webRequest.downloadHandler.text;
						string[] fullAssetsArray = fullAssetsString.Split('\t');
						GameObject currentObject;
						string newAssets = stringOfTruth;
						foreach (var fullAsset in fullAssetsArray){
							if(fullAsset == ""){
								//Debug.Log("empty String item");
							} else {
							string[] assetColumns = fullAsset.Split(",");
							//Debug.Log(assetColumns[0]);
							currentObject = GameObject.Find(assetColumns[0]);
							currentObject.transform.localPosition = new Vector3(float.Parse(assetColumns[1], CultureInfo.InvariantCulture), float.Parse(assetColumns[2], CultureInfo.InvariantCulture), float.Parse(assetColumns[3], CultureInfo.InvariantCulture));
							currentObject.transform.localRotation = Quaternion.Euler(float.Parse(assetColumns[4], CultureInfo.InvariantCulture), float.Parse(assetColumns[5], CultureInfo.InvariantCulture), float.Parse(assetColumns[6], CultureInfo.InvariantCulture));
							newAssets = newAssets.Replace(assetColumns[0], "");
							}
						}

						string[] newAssetsArray = newAssets.Split(" ");
						//Debug.Log("|" + newAssets + "|" + newAssetsArray.Length);
						
						int mediaCounter = 0;
						foreach (var newAsset in newAssetsArray){
							if(newAsset == ""){
								Debug.Log("Empty String");
							} else {
								//Debug.Log("|" + newAsset + "|");
								currentObject = GameObject.Find(newAsset);
								currentObject.transform.position = new Vector3((float)mediaCounter*1.5f,2.25f,2.5f);
								currentObject.transform.localRotation = Quaternion.Euler(0,0,0);
							}
							mediaCounter++;
						}
						//CreateMediaObjects(assets);
						//StartCoroutine(UpdateRecords(stringOfTruth));
						break;
				}
			}
		}
		IEnumerator UpdateRecords(string assetsString){
			//TO-DO: Implement save button. Only UpdateRecords() after button has been used. 
			//Currently all given Media objects get updatet. Do not update media that has not been moved away from default position. => gets reloaded next time eventually.
			//Look into lookUp Pos + Rot after a file has been deleted. Objects move to strange places..

			//Create a Web Form
			WWWForm form = new WWWForm();
			form.AddField("task", "UpdateRecords");
			form.AddField("stringOfTruth", assetsString);

			string[] assets = assetsString.Split(' ');
			GameObject currentObject;
			string xPositions = "";
			string yPositions = "";
			string zPositions = "";
			string xRotations = "";
			string yRotations = "";
			string zRotations = "";

			foreach (var asset in assets){
				//exclude last empty item of the array
				if(asset.Length > 0){
					currentObject = GameObject.Find(asset);
					
					xPositions = xPositions + currentObject.transform.localPosition.x.ToString("R", CultureInfo.InvariantCulture) + " ";
					yPositions = yPositions + currentObject.transform.localPosition.y.ToString("R", CultureInfo.InvariantCulture) + " ";
					zPositions = zPositions + currentObject.transform.localPosition.z.ToString("R", CultureInfo.InvariantCulture) + " ";

					xRotations = xRotations + currentObject.transform.rotation.x.ToString("R", CultureInfo.InvariantCulture) + " ";
					yRotations = yRotations + currentObject.transform.rotation.y.ToString("R", CultureInfo.InvariantCulture) + " ";
					zRotations = zRotations + currentObject.transform.rotation.z.ToString("R", CultureInfo.InvariantCulture) + " ";
				}
			}

			form.AddField("xPositions", xPositions);
			form.AddField("yPositions", yPositions);
			form.AddField("zPositions", zPositions);
			form.AddField("xRotations", xRotations);
			form.AddField("yRotations", yRotations);
			form.AddField("zRotations", zRotations);

			//string uri = "./UnityDBcommunication.php";
			string uri = "http://localhost/UnityDBcommunication.php";
			using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
			{
				yield return www.SendWebRequest();

				string[] pages = uri.Split('/');
				int page = pages.Length - 1;

				if (www.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError(www.error);
				}
				else
				{
					Debug.Log("Form upload complete!");
					Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);
				}

				//Debug.Log(www.downloadHandler.text);
			}
		}
	}
}