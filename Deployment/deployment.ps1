Param(
	[ValidateSet("LocalStorage", "RemoteStorage")]
	[Parameter(Mandatory=$false)]
	[string]
	$storageType = "LocalStorage",
	[int]
	[Parameter(Mandatory=$false)]
	[ValidateRange(10000,100000)] 
	$port = 60508
)

$sourceCodeRepository = "https://github.com/PeterIntel/UserService.git"
$postmanCollection = ".\UserService\User.Service\User.Service\Postman\Users.postman_collection.json"
$postmanEnvironment = ".\UserService\User.Service\User.Service\Postman\Dev.postman_environment.json"
$dotnetWorkingDirectory = ".\UserService\User.Service\User.Service"
$newmanReportFile = "..\TestResults\newman_report.json"

$env:ASPNETCORE_URLS = "http://localhost:$port"
git clone $sourceCodeRepository
$dotnet = Start-Process "dotnet.exe" -ArgumentList "run --launch-profile $storageType --project $dotnetWorkingDirectory" -passthru
Write-Output "PowerShell is waiting before the service will have been launched..."

while($true){
	
	try{
		Get-NetTCPConnection -LocalPort $port -ErrorAction Stop
		newman run $postmanCollection -e $postmanEnvironment -r cli,json --reporter-json-export $newmanReportFile
		Stop-Process -Name "dotnet"
		Remove-Item -Path '.\UserService' -Recurse -Force
		break
	}
	catch{
	}
}



