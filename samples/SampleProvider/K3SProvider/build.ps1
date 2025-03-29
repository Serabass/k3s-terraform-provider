dotnet publish -r win-x64 --self-contained -c release -p:PublishSingleFile=true

# Create plugin directory
mkdir -p $env:APPDATA/.terraform.d/plugins/example.com/serabass/k3s/1.0.0/windows_amd64/

# Copy binary
cp ./bin/release/net7.0/win-x64/publish/* $env:APPDATA/.terraform.d/plugins/example.com/serabass/k3s/1.0.0/windows_amd64/

# Copy log config
cp ./bin/release/net7.0/win-x64/serilog.json $env:APPDATA/.terraform.d/plugins/example.com/serabass/k3s/1.0.0/windows_amd64/serilog.json
