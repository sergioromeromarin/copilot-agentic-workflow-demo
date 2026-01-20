
#!/usr/bin/env bash
set -euo pipefail

BASE_URL=${BASE_URL:-https://localhost:5001}

echo "[SMOKE] GET /ping"
curl -s -w "
Status: %{http_code}
" -k "$BASE_URL/ping" | tee /dev/stderr

echo "[SMOKE] GET /weatherforecast"
curl -s -w "
Status: %{http_code}
" -k "$BASE_URL/weatherforecast" | tee /dev/stderr
