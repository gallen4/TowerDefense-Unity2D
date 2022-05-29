using UnityEngine.SceneManagement;
using UnityEngine;
using TowerDefense;

namespace TowerDefense
{


    public class CampaignWin : MonoBehaviour
    {
        public void ChangeLevel() 
        {
            SceneManager.LoadScene(0);
        }
    }
}

