/* 
 * Copyright (c) 2019, WSO2 Inc. (http://wso2.com) All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Windows;

namespace io.asgardio.dotnet.oidc.sdk.sample
{
    using io.asgardio.dotnet.oidc.sdk;
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// This method refers to ServerConfigurationManager class --> IsEmpty() method
        /// in 'io.asgardio.dotnet.oidc.sdk' library and check whether each and every <app_settings> is 
        /// completed in 'app.config' file. If <app_settings> data is empty it'll load the 'ConfigurationPage',
        /// else load the 'LoginPage'.
        /// </summary>
        public App()
        {
            if (ServerConfigurationManager.IsEmpty() == true)
            {
                ConfigurationPage configuration = new ConfigurationPage();
                configuration.Show();
            }
            else
            {
                LoginPage login = new LoginPage();
                login.Show();
            }           
        }      
    }
}
