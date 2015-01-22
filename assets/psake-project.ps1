Properties {
    $solution = "CommandPipeline.sln"
}

$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'

Include "$root\assets\psake-common.ps1"

Task Default -Depends Pack

Task Pack -Depends Compile -Description "Create NuGet packages and archive files." {
    $version = Get-BuildVersion

    Create-Package "CommandPipeline" $version
    Create-Package "CommandPipeline.Ninject" $version
}