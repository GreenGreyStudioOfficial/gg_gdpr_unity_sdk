using GreenGrey.GDPR;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GreenGrey.GDPR
{
    public class GGGdprInit
    {
        [MenuItem("GreenGrey/GDPR/Create GGGdpr GameObject")]
        public static void CreateGGGdprGameObject()
        {
            var ggGdprObject = new GameObject("GGGdpr");
            ggGdprObject.AddComponent<GGGdprComponent>();
            ggGdprObject.AddComponent<GGGdprConfiguration>();
            SceneManager.MoveGameObjectToScene(ggGdprObject, SceneManager.GetActiveScene());
        } 
    }
}