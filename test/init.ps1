Remove-Item -Recurse -Force .terraform
Remove-Item -Recurse -Force .terraform.lock.hcl

./terraform.exe init
./terraform.exe plan
./terraform.exe apply -auto-approve
