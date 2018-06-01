$version = $args[0] + ""
$tag =  $args[1] + ""
$dst = $args[2] + ""

dotnet pack ./src/BrowserStack.Net/BrowserStack.Net.csproj --version-suffix $tag /property:VersionPrefix=$version --output  $dst
dotnet pack ./src/BrowserStack.Net.Local/BrowserStack.Net.Local.csproj --version-suffix $tag /property:VersionPrefix=$version --output $dst