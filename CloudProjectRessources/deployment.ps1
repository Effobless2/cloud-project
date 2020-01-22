#dotnet publish CloudAPI.csproj--configuration Release -o Publish
#Get-ChildItem -Path .\Publish\ | Compress-Archive -DestinationPath API_Deploy.zip

az webapp deployment source config-zip --resource-group TestForMyLife --name esgi-4al1-group03-cloud-project-api --src API_Deploy.zip
az functionapp deployment source config-zip -g TestForMyLife -n esgi-4al1-group03-cloud-project-azure-function  --src AZ_Deploy.zip