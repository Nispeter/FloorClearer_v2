using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGateway : Gateway
{
    public string targetSceneName;

    public override void Transfer(GameObject player)
    {
        CustomSceneManager.Instance.LoadCustomLevel(targetSceneName);
    }
}
