#if UNITY_EDITOR
using GreenGrey.GDPR.Command;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GreenGrey.GDPR.Samples.InitGdprServiceAndTestRequests
{
    public class InitGdprServiceAndTestRequests : MonoBehaviour
    {
        [SerializeField] private InputField m_projectIdInputField;
        [SerializeField] private InputField m_userIdInputField;
        [SerializeField] private InputField m_emailInputField;

        [SerializeField] private Text m_responseInfoText;

        [SerializeField] private Button m_getInformationButton;
        [SerializeField] private Button m_removeInformationButton;
        [SerializeField] private Button m_cancelRemoveInformationButton;

        private void Awake()
        {
            m_projectIdInputField.onEndEdit.AddListener(OnProjectIdChanged);
            m_userIdInputField.onEndEdit.AddListener(OnUserIdChanged);
            
            m_getInformationButton.onClick.AddListener(OnGetInformationButtonClicked);
            m_removeInformationButton.onClick.AddListener(OnRemoveInformationButtonClicked);
            m_cancelRemoveInformationButton.onClick.AddListener(OnCancelRemoveInformationButtonClicked);
        }
        
        private void OnDestroy()
        {
            m_projectIdInputField.onEndEdit.RemoveListener(OnProjectIdChanged);
            m_userIdInputField.onEndEdit.RemoveListener(OnUserIdChanged);
            
            m_getInformationButton.onClick.RemoveListener(OnGetInformationButtonClicked);
            m_removeInformationButton.onClick.RemoveListener(OnRemoveInformationButtonClicked);
            m_cancelRemoveInformationButton.onClick.RemoveListener(OnCancelRemoveInformationButtonClicked);
        }

        private void OnProjectIdChanged(string _value)
        {
            PlayerSettings.SetApplicationIdentifier(EditorUserBuildSettings.selectedBuildTargetGroup, _value);
        }

        private void OnUserIdChanged(string _value)
        {
            PlayerPrefs.SetString("dmp_user_id", _value);
        }
        
        private void OnGetInformationButtonClicked()
        {
            ExecuteCommand(new GetInformationCommand(m_emailInputField.text));
        }
        
        private void OnRemoveInformationButtonClicked()
        {
            ExecuteCommand(new RemoveInformationCommand());
        }
        
        private void OnCancelRemoveInformationButtonClicked()
        {
            ExecuteCommand(new CancelRemoveInformationCommand());
        }

        private async void ExecuteCommand(BaseGdprCommand _gdprCommand)
        {
            ClearResponseInfo();
            var result = await GGGdpr.Instance.ExecuteCommand(_gdprCommand);
            FillResponseInfo(result, _gdprCommand);
        }

        private void ClearResponseInfo()
        {
            m_responseInfoText.text = string.Empty;
        }

        private void FillResponseInfo(GGGdprResponseType _responseType, BaseGdprCommand _gdprCommand)
        {
            m_responseInfoText.text =
                $"Response type: {_responseType}\nStatus: {_gdprCommand.statusCode}\nSuccess: {_gdprCommand.success}\nError: {_gdprCommand.errorMessage}";
        }
    }
}
#endif