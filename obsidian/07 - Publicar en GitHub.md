# Publicar en GitHub (nuevo repositorio)

## 1) Crear repositorio remoto
En GitHub, crea un repo nuevo, por ejemplo:
- `tabla-generica-primeesc-notes`

Sin README inicial (para evitar conflictos de primer push).

## 2) Inicializar repo local
Ejecuta en la raíz del proyecto:

- `git init`
- `git add .`
- `git commit -m "docs: apuntes obsidian de tabla generica PrimeNG"`
- `git branch -M main`
- `git remote add origin https://github.com/<tu-usuario>/tabla-generica-primeesc-notes.git`
- `git push -u origin main`

## 3) Si usas GitHub CLI (opcional)
- `gh repo create tabla-generica-primeesc-notes --public --source . --remote origin --push`

## 4) Recomendación para Obsidian
Abre la carpeta `obsidian/` como bóveda y usa el grafo para navegar por notas.
