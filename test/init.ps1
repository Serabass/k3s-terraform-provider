./terraform.exe destroy -auto-approve
Remove-Item -Recurse -Force .terraform
Remove-Item -Recurse -Force .terraform.lock.hcl
Remove-Item -Recurse -Force log.txt

./terraform.exe init
./terraform.exe plan
./terraform.exe apply -auto-approve
