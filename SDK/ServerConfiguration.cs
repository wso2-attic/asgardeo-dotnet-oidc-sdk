/* 
 * Copyright (c) 2019, WSO2 Inc. (http://www.wso2.org) All Rights Reserved.
 *
 * WSO2 Inc. licenses this file to you under the Apache License,
 * Version 2.0 (the "License"); you may not use this file except
 * in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Configuration;

namespace Asgardeo.OIDC.SDK
{
    /// <summary>
    /// Manage all server configurations.
    /// </summary>
    public class ServerConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthorizationEndpoint { get; set; }
        public string TokenEndpoint { get; set; }
        public string UserInfoEndpoint { get; set; }
        public string LogoutEndpoint { get; set; }
        public string RedirectUri { get; set; }
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// Retrieves data to the frontend from 'app.config' file.
        /// </summary>
        public ServerConfiguration()
        {
            this.ClientId = ConfigurationManager.AppSettings[Constants.ClientId];
            this.ClientSecret = ConfigurationManager.AppSettings[Constants.ClientSecret];
            this.AuthorizationEndpoint = ConfigurationManager.AppSettings[Constants.AuthorizationEndpoint];
            this.TokenEndpoint = ConfigurationManager.AppSettings[Constants.TokenEndpoint];
            this.UserInfoEndpoint = ConfigurationManager.AppSettings[Constants.UserInfoEndpoint];
            this.LogoutEndpoint = ConfigurationManager.AppSettings[Constants.LogoutEndpoint];
            this.RedirectUri = ConfigurationManager.AppSettings[Constants.RedirectURI];
            this.PostLogoutRedirectUri = ConfigurationManager.AppSettings[Constants.PostLogoutRedirectURI];
        }
    }
}
