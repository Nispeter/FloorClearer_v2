using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGateway : Gateway
{
    public string targetSceneName;

    public override void Transfer(GameObject player)
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
