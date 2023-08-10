using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class OptionsUiPresenter : BasePopUpPanelUi
{
    [SerializeField] private TMP_Dropdown languageDropDown;

    private AsyncOperationHandle m_InitializeOperation;

    protected override void Awake()
    {
        base.Awake();

        MessageSender<MessagesUi>.Subscribe(MessagesUi.OptionsButtonClick, OnOptionsButtonClick);
    }

    private void OnDestroy()
    {
        MessageSender<MessagesUi>.Unsubscribe(MessagesUi.OptionsButtonClick, OnOptionsButtonClick);
    }

    private void OnOptionsButtonClick(object obj)
    {
        Initialize();
    }

    private void Start()
    {     
        languageDropDown.onValueChanged.AddListener(OnSelectionChanged);

        languageDropDown.ClearOptions();
        languageDropDown.options.Add(new TMP_Dropdown.OptionData("Loading..."));
        languageDropDown.interactable = false;

        m_InitializeOperation = LocalizationSettings.SelectedLocaleAsync;
        if (m_InitializeOperation.IsDone)
        {
            InitializeCompleted(m_InitializeOperation);
        }
        else
        {
            m_InitializeOperation.Completed += InitializeCompleted;
        }
    }

    private void InitializeCompleted(AsyncOperationHandle obj)
    {
        var options = new List<string>();
        int selectedOption = 0;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        for (int i = 0; i < locales.Count; ++i)
        {
            var locale = locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selectedOption = i;

            var displayName = locales[i].Identifier.CultureInfo != null ? locales[i].Identifier.CultureInfo.NativeName : locales[i].ToString();
            options.Add(displayName);
        }

        if (options.Count == 0)
        {
            options.Add("No Locales Available");
            languageDropDown.interactable = false;
        }
        else
        {
            languageDropDown.interactable = true;
        }

        languageDropDown.ClearOptions();
        languageDropDown.AddOptions(options);
        languageDropDown.SetValueWithoutNotify(selectedOption);

        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnSelectionChanged(int index)
    {
        UserPreferences.LanguageOption.CurrentValue = index;

        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;

        var locale = LocalizationSettings.AvailableLocales.Locales[index];
        LocalizationSettings.SelectedLocale = locale;

        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
    {
        var selectedIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(locale);
        languageDropDown.SetValueWithoutNotify(selectedIndex);
    }
}
