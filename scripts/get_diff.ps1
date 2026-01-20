
param(
  [string]$BaseBranch = "origin/main"
)
Write-Host "[INFO] Mostrando diff vs $BaseBranch" -ForegroundColor Cyan
git --no-pager diff "$BaseBranch...HEAD"
