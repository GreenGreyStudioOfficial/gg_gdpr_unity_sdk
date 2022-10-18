# Unity SDK для работы с политикой GDPR
Данный SDK работает в паре с аналитическим [SDK Green Grey](https://github.com/GreenGreyStudioOfficial/dmp_unity_library)

## Подключение и настройка
Для работы сервиса GDPR необходимо на стартовую сцену добавить пустой **GameObject** и добавить к нему 2 компонента - **GGGdpComponent** и **GGGdprConfiguration** или воспользоваться пунктом меню *GreenGrey -> GDPR -> Create GGGdpr GameObject*

Во втором компоненте необходимо настроить URL адресс сервиса GDPR и количество попыток на отправку запроса с таймаутом.
## Если у вас используется Green Grey Meta-server
Если в вашем проекте используется Green Grey Meta-server то для корректной отправки данных с мета-сервера в **PlayerSettings** необходимо добавить дефайн **GG_METASERVER**.

Так же для отправки корректных *MainToken* и *AuthToken* в сервис GDPR необходимо передать объект **NetworkManager** в статический метод класса 
```
GGGdprMetaServerDataProvider.InjectNetworkManager(networkManager)
```
## Отправка запросов на сервис GDPR
Для отправки запросов на сервис GDPR нужно создать один из объектов запроса и вызвать функцию **ExecuteCommand** у инстанса статического класса **GGGdpr**:
```C#
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
    var result = await GGGdpr.Instance.ExecuteCommand(_gdprCommand);
}
```