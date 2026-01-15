using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button HostBtn;
    [SerializeField] private Button ClientBtn;

    private void Awake()
    {
        ClientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
        HostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
    }
}
