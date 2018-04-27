Param(
	[ValidateSet("local", "remove")]
	[string]
	$storageType
)

$sourceCodeRepository = "https://github.com/PeterIntel/UserService.git"
$postmanCollection = ".\User.Service\User.Service\Postman\Users.postman_collection.json"
$postmanEnvironment = ".\User.Service\User.Service\Postman\Dev.postman_environment.json"
$dotnetWorkingDirectory = ".\Service.User\Service.User"
$newmanReportFile = "newman_report.json"

Write-Output $storageType
