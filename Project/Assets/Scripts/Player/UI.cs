using Cinemachine;
using UnityEngine;
using TMPro;

public class UI : PlayerModule
{
    [SerializeField] private TMP_Text m_ammoCounter;
    [SerializeField] private TMP_Text m_healthCounter;
    [SerializeField] private UnityEngine.UI.Image m_weaponImageOuput;

    [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
    [SerializeField] private UnityEngine.U2D.PixelPerfectCamera m_pixelPerfect;

    public override void OnOnEnable()
    {
        m_playerController.PlayerValues.HealthChangedEvent += OnHealthChanged;
        m_playerController.PlayerValues.AmmoChangedEvent += OnAmmoChanged;
        m_playerController.PlayerValues.ScopeChangedEvent += OnScopeChanged;
        m_playerController.PlayerValues.WeaponImageAndCameraFollowChangeEvent += WeaponImageAndCameraFollowChange;
    }

    public override void OnOnDisable()
    {
        m_playerController.PlayerValues.HealthChangedEvent -= OnHealthChanged;
        m_playerController.PlayerValues.AmmoChangedEvent -= OnAmmoChanged;
        m_playerController.PlayerValues.ScopeChangedEvent -= OnScopeChanged;
        m_playerController.PlayerValues.WeaponImageAndCameraFollowChangeEvent -= WeaponImageAndCameraFollowChange;
    }


    public void OnHealthChanged(int value)
    {
        m_healthCounter.text = value.ToString();
    }
    private void OnAmmoChanged(string value)
    {
        m_ammoCounter.text = value;
    }
    private void OnScopeChanged(int value)
    {
        m_pixelPerfect.assetsPPU = value;
    }
    private void WeaponImageAndCameraFollowChange(Color image, Transform cameraFollowTransform)
    {
        m_weaponImageOuput.color = image;
        m_virtualCamera.Follow = cameraFollowTransform;
    }
}
