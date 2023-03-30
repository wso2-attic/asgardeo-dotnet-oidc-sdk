# [DEPRECATED] Asgardeo .NET OIDC SDK

## :warning: Warning!
### .NET SDK is no longer encouraged and enriched by Asgardeo and may not work with the latest versions.
### You can implement login using [Authorization Code flow](https://wso2.com/asgardeo/docs/guides/authentication/oidc/implement-auth-code/#prerequisites) with Asgardeo using OIDC standards.

![Build Status](https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk/workflows/CI/badge.svg)
[![Stackoverflow](https://img.shields.io/badge/Ask%20for%20help%20on-Stackoverflow-orange)](https://stackoverflow.com/questions/tagged/wso2is)
[![Discord](https://img.shields.io/badge/Join%20us%20on-Discord-%23e01563.svg)](https://discord.gg/wso2)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://github.com/wso2/product-is/blob/master/LICENSE)
[![Twitter](https://img.shields.io/twitter/follow/wso2.svg?style=social&label=Follow)](https://twitter.com/intent/follow?screen_name=wso2)
---

Asgardeo .NET OIDC SDK enables you to add OIDC based login, logout to your .NET apps in a  simple manner.

- [Getting Started](#getting-started)
- [How it works](#how-it-works)
- [Integrating OIDC SDK to your existing .NET application](#integrating-oidc-sdk-to-your-existing-net-application)
- [Building from the source](#building-from-the-source)
- [Contributing](#contributing)
  * [Reporting issues](#reporting-issues)
- [License](#license)

## Getting started
You can experience the capabilities of Asgardeo .NET OIDC SDK by following this small guide which contains main sections as listed below.

  * [Prerequisites](#prerequisites)
  * [Configuring the sample](#configuring-the-sample)
  * [Configuring Identity Server](#configuring-identity-server)
  * [Running the sample](#running-the-sample)

### Prerequisites
1. Microsoft Windows 8 (Or server equivalent) or greater.
2. [.NET Framework Standard 4.6.1](https://dotnet.microsoft.com/download/dotnet-framework/net461) or greater.
3. WSO2 Identity Server

### Configuring Identity Server
Here we are using WSO2 Identity Server as the OIDC Identity Provider. The sample can be configured with any other preferred Identity Provider as well.
1. Start the WSO2 IS.
2. Access WSO2 IS management console from https://localhost:9443/carbon/ and create a service provider.
   ![Management Console](https://user-images.githubusercontent.com/15249242/91068131-6fc2d380-e651-11ea-9d0a-d58c825bbb68.png)
   i. Navigate to the `Service Providers` tab listed under the `Identity` section in the management console and click `Add`.<br/>
   ii. Provide a name for the Service Provider (ex:- sample-app) and click `Register`. Now you will be redirected to the
    `Edit Service Provider` page.<br/>
   iii. Expand the  `Inbound Authentication Configuration` section and click `Configure` under the `OAuth/OpenID Connect Configuration` section.<br/>
   iv. Provide the following values for the respective fields and click `Update` while keeping other default settings as it is.

       Callback Url - regexp=(http://localhost:8080/pickup-manager/callback/|http://localhost:8080/pickup-manager/postlogout/)
   v. Click `Update` to save.

3. Once the service provider is saved, you will be redirected to the `Service Provider Details` page. Here, expand the
    `Inbound Authentication Configuration` section and click the `OAuth/OpenID Connect Configuration` section. Copy the
    values of  `OAuth Client Key` and `OAuth Client Secret` shown here.
    ![OAuth Client Credentials](https://user-images.githubusercontent.com/15249242/91567068-27155e00-e962-11ea-8eab-b3bdd790bfd4.png)

### Configuring the sample
1. Download the [PickupManagerOIDC-v0.1.1.msi](https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk/releases/download/v0.1.1/PickupManagerOIDC-v0.1.1.msi).
2. Double click the `PickupManagerOIDC-v0.1.1.msi`.
3. Follow the on-screen guidance until you get to the app configuration window.
   ![Sample Setup](https://user-images.githubusercontent.com/15249242/95815321-064e6f80-0d3a-11eb-91bf-44dd6ff47b45.gif)

4. Fill out the following fields.

       Client ID - <Enter the copied value of `OAuth Client Key` when creating the Service Provider>
       Client Secret - <Enter the copied value of `OAuth Client Secret` when creating the Service Provider>
       Authorization Endpoint - https://localhost:9443/oauth2/authorize
       Token Endpoint - https://localhost:9443/oauth2/token
       Userinfo Endpoint - https://localhost:9443/oauth2/userinfo
       Logout Endpoint - https://localhost:9443/oidc/logout
       Redirect URI - http://localhost:8080/pickup-manager/callback/
       PostLogout Redirect URI - http://localhost:8080/pickup-manager/postlogout/
5. Continue the on-screen guidance and complete the installation.

### Running the sample
Once the installation is complete the `Pickup Manager - OIDC v0.1.1` application wiil be launched automatically.<br/>
You can always re-launch the application by double clicking on the `Pickup Manager - OIDC v0.1.1` application available on your Desktop.<br/>
![pickup manager](https://user-images.githubusercontent.com/15249242/95334396-aa17c580-08cb-11eb-83ee-3b88b8512f68.gif)

## How it works
This section explains a detailed walkthrough on how key aspects are handled in the Asgardeo .NET OIDC SDK.
Througout this section we will refer to the [source folder](https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk/tree/master/Sample) of the sample as <APP_HOME>

  * [Trigger authentication](#trigger-authentication)
  * [Retrieve user attributes](#retrieve-user-attributes)
  * [Trigger logout](#trigger-logout)

The structure of the sample would be as follows:<br/>
![Sample Structure](https://user-images.githubusercontent.com/15249242/96110931-2f1f6200-0efe-11eb-97dd-6517b8234bb2.png)

### Trigger authentication
In the [<APP_HOME>/LoginPage.xaml](https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk/blob/fbb348e51ce584ed7f044a77d775d9de732e6458/Sample/LoginPage.xaml#L30) page, we have registered a `Click` event named `LoginButton_Click` for the login button to trigger an OIDC authentication:
```xml
<Button x:Name ="login" Click="LoginButton_Click"/>
```

The button click would trigger an authentication request, and redirect the user to the IdP authentication page.
Upon successful authentication, the user would be redirected to the application homepage.

### Retrieve user attributes
In the [<APP_HOME>/LoginPage.xaml.cs](https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk/blob/fbb348e51ce584ed7f044a77d775d9de732e6458/Sample/LoginPage.xaml.cs#L45) file, we have added the following code inside the `LoginButton_Click` trigger method to get the user subject value and the user attributes referring the SDK API.

```csharp
private async void LoginButton_Click(object sender, RoutedEventArgs e)
{
    // Redirect the user to IDP authentication page
    await authenticationHelper.Login();

    // Focus to app windows after succeful authentication
    this.Activate();

    // Retrieve access token and user information
    accessToken = authenticationHelper.AccessToken;
    userInfo = authenticationHelper.UserInfo;

    // Display the home page window
    HomePage home = new HomePage(accessToken, userInfo);
    home.Show();
    this.Close();
}
```

### Trigger logout
In the [<APP_HOME>/HomePage.xaml](https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk/blob/fbb348e51ce584ed7f044a77d775d9de732e6458/Sample/HomePage.xaml#L48) file, we have added the following button to trigger a SLO flow:
```xml
<Button x:Name="logoutButton" Click="Logout_button_click" />
```

Clicking on the logout link would trigger the SLO flow.

## Integrating OIDC SDK to your existing .NET application
This section will guide you on integrating OIDC into your existing .NET application with the Asgardeo Dotnet OIDC SDK.
This allows a .NET application (i.e. Service Provider) to connect with an IDP using the OpenID Connect protocol.
This guide consist with the following sections.
- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Installing the SDK](#installing-the-sdk)
  * [Using Nuget Package Manager](#using-nuget-package-manager)
  * [Using the library DLL](#using-the-library-dll)
- [Login](#login)
- [Logout](#logout)
- [Get User Info](#get-user-info)

### Prerequisites
1. Microsoft Windows 8 (Or server equivalent) or greater.
2. [.NET Framework Standard 4.6.1](https://dotnet.microsoft.com/download/dotnet-framework/net461) or greater.
3. Visual Studio 2017 Community or greater.

### Installing the SDK
#### Using Nuget Package Manager
1. Open the Nuget Package Manger.
2. Search for [Asgardeo.OIDC.SDK](https://www.nuget.org/packages/Asgardeo.OIDC.SDK/).
3. Include it with the suggested required dependencies for the project/solution. 

Alternatively, you can also run the following command in the Package Manager CLI as shown below. 

``Install-Package Asgardeo.OIDC.SDK -Version 0.1.1``

#### Using the library DLL
1. Download [Asgardeo.OIDC.SDK.dll](https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk/releases/download/v0.1.1/Asgardeo.OIDC.SDK.dll).
2. Add the `Asgardeo.OIDC.SDK.dll` file as a Reference in your Visual Studio project.
3. Build the project. 

Once you have installed the SDK, create a file named `App.config` as shown below and place it in the application path.

```xml
<configuration>
    <appSettings>
        <add key="ClientId" value="<YOUR_CLIENT_KEY>" />
        <add key="ClientSecret" value="<YOUR_CLIENT_SECRET>" />
        <add key="AuthorizationEndpoint" value="https://localhost:9443/oauth2/authorize" />
        <add key="TokenEndpoint" value="https://localhost:9443/oauth2/token" />
        <add key="UserInfoEndpoint" value="https://localhost:9443/oauth2/userinfo" />
        <add key="LogoutEndpoint" value="https://localhost:9443/oidc/logout" />
        <add key="RedirectURI" value="http://localhost:8080/pickup-manager/callback/" />
        <add key="PostLogoutRedirectURI" value="http://localhost:8080/pickup-manager/postlogout/" />
        <add key="ClientSettingsProvider.ServiceUri" value="" />
  </appSettings>
</configuration>
```

### Login
Use the following code snippet to authenticate a user. 

```csharp
readonly AuthenticationHelper authenticationHelper = new AuthenticationHelper();
await authenticationHelper.Login();
var accessToken = authenticationHelper.AccessToken;
```

### Logout
Use the following code snippet to log out an already logged in user. 

```csharp
await authenticationHelper.Logout(accessToken);
var request = authenticationHelper.Request;
```

### Get User Info
Use the following code snippet to access the user information.

```csharp
readonly AuthenticationHelper authenticationHelper = new AuthenticationHelper();
await authenticationHelper.Login();
var userInfo = authenticationHelper.UserInfo;
dynamic json = JsonConvert.DeserializeObject(userInfo);
var subject = json.sub;
```

## Building from the source
### Prerequisites
1. Microsoft Windows 8 (Or server equivalent) or greater.
2. [.NET Framework Standard 4.6.1](https://dotnet.microsoft.com/download/dotnet-framework/net461) or greater.
3. Visual Studio 2017 Community or greater.
4. [WiX Toolset V3.x](https://wixtoolset.org/releases/) - Required only if you are building the full solution in `Release` configuration.

To build the project from the source, follow the instructions given below.

1. Clone the repository using the following command. 
``git clone https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk.git``
2. Open the solution using Visual Studio.
3. Build the solution in `Debug` configuration. 

## Contributing
Please read [Contributing to the Code Base](http://wso2.github.io/) for details on our code of conduct, and the
 process for submitting pull requests to us.
 
### Reporting issues
We encourage you to report issues, improvements, and feature requests creating [git Issues](https://github.com/asgardeo/asgardeo-dotnet-oidc-sdk/issues).

Important: And please be advised that security issues must be reported to security@wso2.com, not as GitHub issues, 
in order to reach the proper audience. We strongly advise following the WSO2 Security Vulnerability Reporting Guidelines
 when reporting the security issues.

## License
This project is licensed under the Apache License 2.0. See the [LICENSE](LICENSE) file for details.

