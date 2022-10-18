using GreenGrey.GDPR;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GreenGray.GDPR
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