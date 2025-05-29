az login
$appname = "jm-webjob-demo-dotnet"
$resource_group = "jm-webjob-demos";

Write-Host "Building Dotnet Webapp";
dotnet publish -c Release -r linux-x64 -o ./dotnet/webapp/build_output ./WebApp/dotnet_note_webapp_example.csproj
    
Write-Host "Building Dotnet Webjob";
dotnet publish -c Release -r linux-x64 -o ./dotnet/webjob/build_output ./Webjobs/dotnet_note_webjob_example.csproj
$webappOutput = "./dotnet/webapp/build_output"
$webjobOutput = "./dotnet/webjob/build_output"
$webjobTarget = Join-Path $webappOutput "App_Data/jobs/triggered/Purge"

# Ensure the target directory exists
if (-not (Test-Path -Path $webjobTarget)) {
    New-Item -ItemType Directory -Path $webjobTarget -Force | Out-Null
}
    
Write-Host "Merging the WebApp and Webjob..."; 
#Deploying them independently ruined 3 hours of my life and showed so many errors.
    
# Copy webjob files into the webapp build output under App_Data
Copy-Item -Path "$webjobOutput\*" -Destination $webjobTarget -Recurse -Force

# Now package the webapp (including the webjob files)
Compress-Archive -Path "$webappOutput\*" -DestinationPath ./dotnet/package.zip


Write-Host "Deploying Dotnet Apps";
az webapp deploy --resource-group $resource_group --name $appname --src-path ./dotnet/package.zip --type zip

Write-Host "Cleaning Up Dotnet Apps";
Remove-Item -Path ./dotnet -Recurse -Force
    
    
Start-Sleep -Seconds 60 # Wait for deployment to complete
az webapp restart --resource-group $resource_group --name $appname 