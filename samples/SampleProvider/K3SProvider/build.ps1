dotnet publish -r win-x64 --self-contained -c release -p:PublishSingleFile=true

# $REGISTRY = "registry.terraform.io"
$REGISTRY = "example.com"
$PROVIDER = "serabass/k3s"
$VERSION = "1.0.0"

# Delete old plugin
Remove-Item -Recurse -Force $env:APPDATA\.terraform.d\plugins\$REGISTRY\$PROVIDER\$VERSION

# Create plugin directory
mkdir -p $env:APPDATA/.terraform.d/plugins/$REGISTRY/$PROVIDER/$VERSION/windows_amd64/

# Copy binary
cp ./bin/release/net7.0/win-x64/publish/* $env:APPDATA/.terraform.d/plugins/$REGISTRY/$PROVIDER/$VERSION/windows_amd64/

# Copy log config
cp ./bin/release/net7.0/win-x64/serilog.json $env:APPDATA/.terraform.d/plugins/$REGISTRY/$PROVIDER/$VERSION/windows_amd64/serilog.json
