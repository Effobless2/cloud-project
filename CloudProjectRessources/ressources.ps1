terraform init .

terraform apply `
    -var subscription_id="00000000-00000000-00000000" `
    -var tenant_id="00000000-00000000-00000000" `
    -var client_id="00000000-00000000-00000000" `
    -var client_secret="00000000-00000000-00000000" `
    .

#dotnet publish CloudAPI.csproj--configuration Release -o Publish
#Get-ChildItem -Path .\Publish\ | Compress-Archive -DestinationPath Deploy.zip

.\deployment.ps1