
# GitHub Copilot Agentic Workflow — .NET 8 (Repositorio base)

Este repositorio incluye una API mínima en .NET 8 y la configuración necesaria para trabajar con **GitHub Copilot** usando **agentes orquestados**.

## Requisitos previos
- .NET 8 SDK
- Git
- Visual Studio Code
- Cuenta GitHub con **GitHub Copilot** activo

## Puesta en marcha
```bash
# Restaurar y ejecutar la API
cd CopilotDemo.Api
dotnet restore
dotnet run
# Swagger: https://localhost:5001/swagger (o el puerto indicado en consola)
```

## Estructura
```
CopilotDemo.Api/               # API mínima .NET 8
.github/
  ├─ agents/                  # Definiciones de agentes
  ├─ prompts/                 # Prompts reutilizables y orquestador
  └─ workflows/               # GitHub Actions de ejemplo
scripts/                       # Guías para crear la PR de demostración
```

## Flujo de demo (PR + Orquestación)
1. Crea un repositorio en GitHub y sube este contenido.
2. Crea una rama `feature/demo` y realiza un cambio pequeño (p.ej., añade el endpoint `/ping`).
3. Abre una Pull Request.
4. Copia el *diff* si lo necesitas y abre **Copilot Chat**.
5. Ejecuta el prompt orquestador de `./.github/prompts/pr-orchestrator.prompt.md`.
6. Revisa el resultado dividido por agente: **código**, **seguridad**, **resumen**.

## Nota
Este proyecto usa el template mínimo y Swagger (Swashbuckle) para documentación en desarrollo.

---
© 2026 — Plantilla educativa para demostraciones de orquestación con GitHub Copilot.


---
## Agente de QA incluido

Se añade el agente **QA Tester** (`.github/agents/qa-tester.agent.md`) y el prompt `./.github/prompts/qa-checklist.prompt.md`.

### Uso sugerido en Copilot Chat
- Ejecuta el orquestador en `./.github/prompts/pr-orchestrator.prompt.md`.
- Revisa la **Sección 4: QA** con el plan de pruebas y checklist.

### Opcional: pruebas rápidas (smoke)
```bash
# En Linux/macOS
chmod +x scripts/smoke_tests.sh
BASE_URL=https://localhost:5001 ./scripts/smoke_tests.sh
```

### Postman
Importa `tests/collections/copilot-demo.postman_collection.json` y ajusta `baseUrl` si cambias el puerto.


---
## Orquestación manual desde VS Code (Copilot Chat)

1. **Instala extensiones recomendadas** cuando VS Code te lo sugiera (ver `.vscode/extensions.json`).
2. **Lanza la API**: `Terminal > Run Task > watch` o `F5`.
3. **Abre Copilot Chat** (icono del panel lateral).
4. **Adjunta el contexto**:
   - Ejecuta `Terminal > Run Task > diff:current-branch-vs-main` y copia el diff.
   - Opcional: usa `scripts/get_diff.sh` (Linux/macOS) o `scripts/get_diff.ps1` (Windows).
5. **Pega el orquestador**:
   - Usa el archivo `./.github/prompts/pr-orchestrator-full.md` (self-contained) **o** el snippet `orcpr`.
6. **Pega el diff** y pide el resultado en 4 secciones (código, seguridad, resumen, QA).
7. **Refina**: solicita ejemplos `curl` y casos Gherkin para los endpoints afectados.

### Consejos
- Para probar rápido: `tests/http/api.http` (requiere extensión REST Client).
- Si el puerto cambia, actualiza `{{baseUrl}}` en REST Client o en `settings.json`.
