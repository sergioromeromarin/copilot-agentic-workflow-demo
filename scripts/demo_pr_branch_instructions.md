
# Instrucciones para crear una Pull Request de demostración

```bash
# 1) Inicializa Git y sube el repo a GitHub
cd copilot-agentic-workflow-demo
git init
git add .
git commit -m "Initial commit — Demo base"
# Cambia <org>/<repo>
git remote add origin https://github.com/<org>/<repo>.git
git branch -M main
git push -u origin main

# 2) Crea una rama de trabajo y un cambio pequeño
git checkout -b feature/demo
# Edita CopilotDemo.Api/Program.cs (por ejemplo, modifica el endpoint /ping)
# o añade validaciones menores

git add CopilotDemo.Api/Program.cs
git commit -m "feat: ajuste menor para demo de PR"
git push -u origin feature/demo

# 3) Abre una Pull Request en GitHub
# 4) Usa Copilot Chat con el prompt orquestador
```
