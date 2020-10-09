# ------------------------------------------------------------------------
#
# Copyright 2020 WSO2, Inc. (http://wso2.com)
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
# http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License

# ------------------------------------------------------------------------

# Version will be updated in the following files.
$sdkProjectFilePath = "$PWD\io.asgardio.dotnet.oidc.sdk\io.asgardio.dotnet.oidc.sdk.csproj"
$sdkSampleAssemblyInfoFilePath = "$PWD\io.asgardio.dotnet.oidc.sdk.sample\Properties\AssemblyInfo.cs"

# Get the Version property of the SDK
$sdkProjectFileContentXml = [xml](Get-Content $sdkProjectFilePath)
$versionPropertyXmlNode = $sdkProjectFileContentXml.SelectSingleNode('Project//PropertyGroup//Version')
$currentVersion = [Version]$versionPropertyXmlNode.InnerText
Write-Host "Current Version: $currentVersion"

# Calculate the next version
$nextVersion = [string]::Format("{0}.{1}.{2}",$currentVersion.Major, $currentVersion.Minor,($currentVersion.Build + 1))
Write-Host "Next Version: $nextVersion"

# Set the value of 'Version' property in SDK Project File to the $nextVersion
$versionPropertyXmlNode.InnerText = $nextVersion;
$sdkProjectFileContentXml.Save($sdkProjectFilePath)
Write-Host "Updated the version in SDK project"

# Get the AssemblyVersion property of the sample project
$versionPattern = '\[assembly: AssemblyVersion\("(.*)"\)\]'
(Get-Content $sdkSampleAssemblyInfoFilePath) | ForEach-Object{
    if($_ -match $versionPattern){
        # Edit the currentVersion number and put back with the value of 'AssemblyVersion' property
        # in AssemblyInfo.cs file of the sample project to the $nextVersion
        '[assembly: AssemblyVersion("{0}")]' -f $nextVersion
    } else {
        # Output line as is
        $_
    }
} | Set-Content $sdkSampleAssemblyInfoFilePath
Write-Host "Updated the version in sample project"
