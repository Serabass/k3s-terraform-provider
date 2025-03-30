dotnet publish -r win-x64 --self-contained -c release -p:PublishSingleFile=true

# $REGISTRY = "registry.terraform.io"
$REGISTRY = "example.com"
$PROVIDER = "serabass/k3s"
$VERSION = "1.0.0"
$NET_VERSION = "net7.0"
$PLATFORM = "windows_amd64"
$OS = "win-x64"

# Delete old plugin
Remove-Item -Recurse -ErrorAction SilentlyContinue -Force $env:APPDATA\.terraform.d\plugins\$REGISTRY\$PROVIDER\$VERSION

# Create plugin directory
mkdir -p $env:APPDATA/.terraform.d/plugins/$REGISTRY/$PROVIDER/$VERSION/$PLATFORM/

# Copy binary
cp ./bin/release/$NET_VERSION/$OS/publish/* $env:APPDATA/.terraform.d/plugins/$REGISTRY/$PROVIDER/$VERSION/$PLATFORM/

# Copy log config
cp ./bin/release/$NET_VERSION/$OS/serilog.json $env:APPDATA/.terraform.d/plugins/$REGISTRY/$PROVIDER/$VERSION/$PLATFORM/serilog.json
