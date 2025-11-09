# File: Check-DockerPaths.ps1
# Run this in the root of your Ecommerce project

$pathsToCheck = @(
    "Services\Catalog\Catalog.csproj",
    "Services\Payment\Payment.csproj",
    "Services\Identity\Identity.csproj",
    "Services\Discount\Discount\Discount.csproj",
    "Services\Ordering\Ordering\Ordering.csproj",
    "Basket\Basket\Basket.csproj",
    "Infrastructure\Common.Logging\Common.Logging.csproj",
    "Infrastructure\EventBus.Messages\EventBus.Messages.csproj"
)

$allExist = $true

foreach ($path in $pathsToCheck) {
    if (-Not (Test-Path $path)) {
        Write-Host "❌ Missing file: $path" -ForegroundColor Red
        $allExist = $false
    } else {
        Write-Host "✅ Found file: $path" -ForegroundColor Green
    }
}

if ($allExist) {
    Write-Host "`nAll files are present. You can safely run docker-compose." -ForegroundColor Cyan
} else {
    Write-Host "`nSome files are missing. Fix them before building Docker images." -ForegroundColor Yellow
}
