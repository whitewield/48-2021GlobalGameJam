using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameManager : MonoBehaviour {
    private static CS_GameManager instance = null;
    public static CS_GameManager Instance { get { return instance; } }


    [SerializeField] GameObject myDialogPrefab = null;
    [SerializeField] float mySubmitDistance = 1f;
    private CS_Dialog myCurrentDialog;
    private int myCurrentTaskIndex = 0;

    private CS_Spot[] mySpots = null;

    [SerializeField] List<GameObject> myTaskItemPrefabList = new List<GameObject> ();
    private List<CS_Item> myItems = new List<CS_Item> ();
    private List<CS_Item> myTaskItems = new List<CS_Item> ();

    private float myTimer = 0;
    private bool isGameOver = false;

    private void Awake () {
        if (instance != null && instance != this) {
            Destroy (this.gameObject);
        } else {
            instance = this;
        }
    }

    private void Start () {
        isGameOver = false;
        // init task index
        myCurrentTaskIndex = 0;
        // init timer
        myTimer = 0;

        // find all spots
        mySpots = FindObjectsOfType<CS_Spot> ();

        // init all items in scene
        myItems.AddRange (FindObjectsOfType<CS_Item> ());

        // create task items
        foreach (GameObject f_itemPrefab in myTaskItemPrefabList) {
            // create object
            GameObject f_itemObject = Instantiate (f_itemPrefab);
            // find a spot for this object
            CS_Spot f_spot = mySpots[Random.Range (0, mySpots.Length)];
            // set position
            f_itemObject.transform.position = f_spot.GetPosition ();
            // set parent
            f_itemObject.transform.parent = f_spot.transform.parent;

            // ge item script
            CS_Item f_item = f_itemObject.GetComponent<CS_Item> ();
            // add to the temp item list
            myTaskItems.Add (f_item);
        }

        // add task items to item list
        myItems.AddRange (myTaskItems);

        // create the first task
        CreateTask ();
    }

    private void Update () {
        if (isGameOver) {
            return;
        }
        myTimer += Time.deltaTime;
        CS_GameUI.Instance.SetTimer (myTimer);
    }

    private void CreateTask () {
        // create a dialog 
        GameObject t_dialogObject = Instantiate (myDialogPrefab, Vector3.zero, Quaternion.identity);
        myCurrentDialog = t_dialogObject.GetComponent<CS_Dialog> ();

        // add dialog box to item list
        myItems.Add (myCurrentDialog);

        // move the dialog box to front
        SelectItem (myCurrentDialog);

        // get a random item
        CS_Item t_item = myTaskItems[Random.Range (0, myTaskItems.Count)];

        // set ending UI
        CS_GameUI.Instance.SetTargetItem (t_item.GetSprite (), myCurrentTaskIndex);

        // init dialog
        myCurrentDialog.SetTarget (t_item);
    }

    public void CheckSubmit (CS_Item g_item) {
        if (myCurrentDialog == null) {
            return;
        }

        // check if its the item you are looking for
        if (myCurrentDialog.GetTarget() != g_item) {
            return;
        }

        float t_distance = Vector2.Distance (g_item.transform.position, myCurrentDialog.GetSubmitPosition ());
        if (t_distance < mySubmitDistance) {
            // remove item
            g_item.Hide ();
            myTaskItems.Remove (g_item);

            // remove dialog
            myCurrentDialog.Hide ();
            myCurrentDialog = null;

            // update task index number 
            myCurrentTaskIndex++;

            if (myCurrentTaskIndex >= 5) {
                CS_GameUI.Instance.ShowEnding (myTimer);
            } else {
                // create new dialog
                CreateTask ();
            }
        }
    }


    public void AddItem (CS_Item g_item, bool g_isTaskItem) {
        myItems.Add (g_item);
        if (g_isTaskItem == true) {
            myTaskItems.Add (g_item);
        }
    }

    public void SelectItem (CS_Item g_targetItem) {
        foreach (CS_Item f_item in myItems) {
            if (g_targetItem == f_item) {
                // set z position to 0
                Vector3 f_position = f_item.transform.position;
                f_position.z = 0;
                f_item.transform.position = f_position;
            } else {
                // check if its in a box
                if (f_item.transform.parent == null) {
                    // update the z position, move away from the camera
                    Vector3 f_position = f_item.transform.position;
                    f_position.z += 0.01f;
                    f_item.transform.position = f_position;
                }
            }
        }


    }
}
