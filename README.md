# CallForHelp

O aplicativo CallForHelp visa auxiliar pessoas com deficiência visual a buscarem auxílio no contexto do trabalho, seja na realização de alguma atividade ou na busca de uma informação. O auxílio é solicitado por meio de um botão, e a solicitação é enviada para um grupo de voluntários dispostos a ajudar (cadastrados no aplicativo Microsoft Teams).

O aplicativo foi desenvolvido utilizando a plataforma Xamarin.

A comunicação entre o aplicativo e o Microsoft Teams é feita via Logic Apps e a geração de notificação para o usuário por meio de Azure Functions a Azure Notification Hub. 

**Requisitos do Sistema para Desenvolvimento em Xamarin**

https://docs.microsoft.com/pt-br/xamarin/cross-platform/get-started/requirements

**FCM (Firebase Cloud Messaging)**

https://docs.microsoft.com/pt-br/xamarin/android/data-cloud/google-messaging/firebase-cloud-messaging

**Enviar notificações por push para aplicativos Xamarin.Android usando os Hubs de Notificação**

https://docs.microsoft.com/pt-br/azure/notification-hubs/xamarin-notification-hubs-push-notifications-android-gcm#create-a-firebase-project-and-enable-firebase-cloud-messaging

## Instalação

Faça o clone do repositório, ou o download do projeto:
```
git clone https://github.com/beatrizmayumi/CallForHelp
```

## Utilização
No arquivo Constants.cs, substitua <ENDPOINT> pela ListenConnectionString gerada no seu Notification Hub:

```
public const string ListenConnectionString = "<ENDPOINT>";
public const string NotificationHubName = "<NOTIFICATION_HUB_NAME>";
```

No arquivo google-services.json, substitua os campos com “ ”, com os dados do seu projeto.

Exemplo de campos a serem substituídos:

```
"project_info": {
"project_number": "",
"firebase_url": "",
"project_id": "",
"storage_bucket": ""
},
```

  
