param ( [switch] $recreate)

if ($recreate){
	#kill the container if it is running
	Write-Host "kill local-sql container"
	docker kill local-sql
	#remove the container if exist
	Write-Host "remove local-sql container"
	docker rm local-sql
}

 if (-NOT (docker start local-sql)){
    docker run --name local-sql -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Cowboys12@Home" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
 }

 
docker ps -f name=local-sql

