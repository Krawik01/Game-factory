[CmdletBinding()]
param(
    [string]$LevelsPath = (Join-Path $PSScriptRoot '..\design\dock-rush\levels')
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$colors = @('Red', 'Green', 'Blue', 'Yellow')
$directions = @('North', 'East', 'South', 'West')
$nodeTypes = @('spawn', 'hub', 'bay', 'sink', 'junction')
$errors = [System.Collections.Generic.List[string]]::new()

function Add-Error([string]$Level, [string]$Message) {
    $script:errors.Add("[$Level] $Message")
}

function Test-Position([object]$Position, [string]$Level, [string]$Label) {
    if (@($Position).Count -ne 2 -or $null -eq $Position[0] -or $null -eq $Position[1]) {
        Add-Error $Level "$Label must have a two-value position."
    }
}

if (-not (Test-Path -LiteralPath $LevelsPath -PathType Container)) {
    throw "Dock Rush levels directory was not found: $LevelsPath"
}

$files = @(Get-ChildItem -LiteralPath $LevelsPath -Filter 'level_*.json' -File | Sort-Object Name)
if ($files.Count -eq 0) {
    throw "No level_*.json files found in $LevelsPath"
}

$levelIds = @{}
foreach ($file in $files) {
    $levelName = $file.Name
    try {
        $level = Get-Content -LiteralPath $file.FullName -Raw | ConvertFrom-Json
    }
    catch {
        Add-Error $levelName "Invalid JSON: $($_.Exception.Message)"
        continue
    }

    if ($null -eq $level.id -or $level.id -isnot [long] -or $level.id -lt 1) {
        Add-Error $levelName 'id must be a positive integer.'
    }
    elseif ($levelIds.ContainsKey($level.id)) {
        Add-Error $levelName "duplicates id $($level.id) from $($levelIds[$level.id])."
    }
    else {
        $levelIds[$level.id] = $levelName
    }

    $entityIds = @{}
    $hubIds = @{}
    foreach ($hub in @($level.hubs)) {
        if ([string]::IsNullOrWhiteSpace($hub.id)) {
            Add-Error $levelName 'A hub is missing id.'
            continue
        }
        if ($entityIds.ContainsKey($hub.id)) {
            Add-Error $levelName "duplicate entity id '$($hub.id)'."
        }
        $entityIds[$hub.id] = 'hub'
        $hubIds[$hub.id] = $true
        Test-Position $hub.position $levelName "Hub '$($hub.id)'"
        if ($hub.rotation -isnot [long] -or $hub.rotation -lt 0 -or $hub.rotation -gt 3) {
            Add-Error $levelName "Hub '$($hub.id)' rotation must be an integer from 0 to 3."
        }
    }

    $spawnCount = 0
    foreach ($node in @($level.nodes)) {
        if ([string]::IsNullOrWhiteSpace($node.id)) {
            Add-Error $levelName 'A node is missing id.'
            continue
        }
        if ($entityIds.ContainsKey($node.id)) {
            Add-Error $levelName "duplicate entity id '$($node.id)'."
        }
        $entityIds[$node.id] = 'node'
        Test-Position $node.position $levelName "Node '$($node.id)'"
        if ($node.type -notin $nodeTypes) {
            Add-Error $levelName "Node '$($node.id)' has unsupported type '$($node.type)'."
        }
        if ($node.type -eq 'spawn') { $spawnCount++ }
        if ($node.type -eq 'bay') {
            if ($node.acceptColor -notin $colors) {
                Add-Error $levelName "Bay '$($node.id)' has unsupported acceptColor '$($node.acceptColor)'."
            }
            if ($node.acceptFacing -notin $directions) {
                Add-Error $levelName "Bay '$($node.id)' has unsupported acceptFacing '$($node.acceptFacing)'."
            }
        }
    }
    if ($spawnCount -ne 1) {
        Add-Error $levelName "must contain exactly one spawn node; found $spawnCount."
    }

    $adjacency = @{}
    foreach ($edge in @($level.edges)) {
        if ($edge.from -notin $entityIds.Keys -or $edge.to -notin $entityIds.Keys) {
            Add-Error $levelName "Edge '$($edge.from)' -> '$($edge.to)' references an unknown entity."
            continue
        }
        if ($edge.from -eq $edge.to) {
            Add-Error $levelName "Edge '$($edge.from)' cannot point to itself."
        }
        if ($hubIds.ContainsKey($edge.from)) {
            if ($edge.hub -ne $edge.from) {
                Add-Error $levelName "Edge leaving hub '$($edge.from)' must name that hub."
            }
            $rotations = @($edge.activeAtRotation)
            if ($rotations.Count -eq 0 -or @($rotations | Where-Object { $_ -isnot [long] -or $_ -lt 0 -or $_ -gt 3 }).Count -gt 0) {
                Add-Error $levelName "Hub edge '$($edge.from)' -> '$($edge.to)' needs rotations from 0 to 3."
            }
        }
        elseif ($null -ne $edge.hub -or $edge.PSObject.Properties.Name -contains 'activeAtRotation') {
            Add-Error $levelName "Static edge '$($edge.from)' -> '$($edge.to)' cannot declare hub rotation data."
        }
        if (-not $adjacency.ContainsKey($edge.from)) { $adjacency[$edge.from] = @() }
        $adjacency[$edge.from] += $edge.to
    }

    foreach ($crate in @($level.queue)) {
        if ($crate.color -notin $colors) {
            Add-Error $levelName "Queue crate has unsupported color '$($crate.color)'."
        }
        if ($crate.facing -notin $directions) {
            Add-Error $levelName "Queue crate has unsupported facing '$($crate.facing)'."
        }
    }
    if (@($level.queue).Count -eq 0) { Add-Error $levelName 'queue cannot be empty.' }
    if ($level.tickSeconds -isnot [double] -and $level.tickSeconds -isnot [int]) {
        Add-Error $levelName 'tickSeconds must be numeric.'
    }
    elseif ($level.tickSeconds -le 0 -or $level.tickSeconds -gt 2) {
        Add-Error $levelName 'tickSeconds must be greater than 0 and no more than 2.'
    }
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Dock Rush level validation failed with $($errors.Count) error(s)."
}

Write-Host "Dock Rush level validation passed: $($files.Count) level(s), $($levelIds.Count) unique id(s)."
