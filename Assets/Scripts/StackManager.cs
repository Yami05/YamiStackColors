using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public List<GameObject> collectableColorsList = new List<GameObject>();

    public static StackManager instance;
    private bool isActive;
    private CollectorController collectorController;
    private GameEvents gameEvents;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameEvents = GameEvents.instance;
        collectorController = transform.parent.GetComponentInChildren<CollectorController>();
        isActive = true;

        gameEvents.FinalPartSet += OnFinalPart;
        gameEvents.Kick += OnKick;
    }


    void FixedUpdate()
    {

        if (isActive == true)
        {

            if (collectableColorsList.Count < 1)
            {
                return;
            }

            collectableColorsList[0].gameObject.transform.position = new Vector3(collectorController.transform.position.x,
                collectorController.transform.position.y + 1, collectorController.transform.position.z + 0.5f);

            for (int i = 1; i < collectableColorsList.Count; i++)
            {
                collectableColorsList[i].transform.position = Vector3.Lerp(collectableColorsList[i].transform.position,
                new Vector3(collectableColorsList[i - 1].transform.position.x, collectableColorsList[0].transform.position.y +
                collectableColorsList.IndexOf(collectableColorsList[i]) * 0.2f,
                collectableColorsList[0].transform.position.z), 0.9f);

            }
        }
    }


    private void OnKick(Transform transform)
    {
        isActive = false;

    }
    private void OnFinalPart(FinalPartStatus finalPartStatus)
    {
        if (finalPartStatus == FinalPartStatus.Active)
        {
            transform.SetParent(collectorController.transform);
        }
    }
    public void GameEndControl()
    {
        if (collectableColorsList.Count <= 0)
        {
            GameEvents.instance.GameOver?.Invoke();
            return;
        }
    }

}
