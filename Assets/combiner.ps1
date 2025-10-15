param(
  [string]$Directory = ".",
  [string]$Out = "all_cs_combined.cs"
)

# resolve full path for output (if exists) or build absolute path
try {
  $OutFull = (Resolve-Path -LiteralPath $Out -ErrorAction Stop).Path
} catch {
  $OutFull = Join-Path (Get-Location) $Out
}

# gather files, exclude the output file if it would match
Get-ChildItem -Path $Directory -Recurse -Filter *.cs -File |
  Where-Object { $_.FullName -ne $OutFull } |
  Sort-Object FullName |
  ForEach-Object {
    "// ----- File: $($_.FullName) -----"
    Get-Content -LiteralPath $_.FullName -Raw
    ""  # add an extra newline between files
  } | Out-File -FilePath $OutFull -Encoding utf8

Write-Host "Combined files written to $OutFull"
