
#!/usr/bin/env bash
set -euo pipefail

BASE_BRANCH=${BASE_BRANCH:-origin/main}

echo "[INFO] Mostrando diff vs $BASE_BRANCH" >&2
git --no-pager diff "$BASE_BRANCH...HEAD"
