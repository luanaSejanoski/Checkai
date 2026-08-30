# ✅ Checkaí

API para gerenciamento e acompanhamento de hábitos.

## 🛠️ Tecnologias utilizadas

- 💻 C#
- 🌐 ASP.NET Core
- 🗄️ Entity Framework Core
- 🪶 SQLite
- 🔐 JWT (JSON Web Token)
- 📖 Swagger

## 🚀 Funcionalidades

- 👤 Cadastro e login de usuários
- 🔐 Autenticação e autorização com JWT
- 📝 Criação de hábitos
- 📋 Listagem de hábitos
- 🔎 Busca de hábito por ID
- ✏️ Alteração de hábitos
- 🗑️ Remoção de hábitos
- ☑️ Marcação de hábitos como concluídos
- ↩️ Desfazer conclusão de um hábito
- 🔥 Cálculo da sequência de dias consecutivos
- 📅 Histórico de hábitos
- ⏳ Cálculo de dias restantes
- 📊 Cálculo do progresso do hábito
- 🌅 Listagem dos hábitos concluídos no dia

## 🏗️ Arquitetura

O projeto utiliza uma separação em camadas:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
AppDbContext
    ↓
SQLite
```

## ▶️ Como executar o projeto

### 📌 Pré-requisitos

- 💻 .NET SDK
- 🧰 Git

### 📥 Clonando o projeto

```bash
git clone URL_DO_REPOSITORIO
cd Checkai
```