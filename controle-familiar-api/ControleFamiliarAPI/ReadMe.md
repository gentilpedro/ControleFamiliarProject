# 💰 Controle Financeiro API

API desenvolvida em **.NET 9** para gerenciamento de controle financeiro, permitindo cadastro de pessoas, categorias, transações e geração de relatórios.

Este projeto foi desenvolvido como **teste técnico**, com foco em:

* Organização de código
* Regras de negócio
* Estrutura em camadas
* Documentação com OpenAPI (Scalar)

---

# 🚀 Tecnologias utilizadas

* [.NET 9](https://dotnet.microsoft.com/)
* ASP.NET Core Web API
* Entity Framework Core
* Scalar (OpenAPI UI moderna)


---

# 📦 Como rodar o projeto

---

## 2️⃣ Restaurar dependências

```bash
dotnet restore
```

---

## 3️⃣ Executar a aplicação

```bash
dotnet run
```

---

## 4️⃣ Acessar a documentação

Após rodar o projeto, acesse:

```bash
https://localhost:{porta}/scalar/v1
```

Ou:

```bash
https://localhost:{porta}/scalar
```


---

# 📚 Funcionalidades

## 👤 Pessoas

* Criar pessoa
* Listar pessoas
* Remover pessoa

---

## 🏷️ Categorias

* Criar categoria
* Listar categorias
* Remover categoria

Finalidades possíveis:

* Receita
* Despesa
* Ambas

---

## 💰 Transações

* Criar transação
* Listar transações

### Regras de negócio:

* Valor deve ser positivo
* Menores de idade (< 18 anos) só podem ter despesas
* Categoria deve ser compatível com o tipo da transação

---

## 📊 Relatórios

### Totais por pessoa

Retorna:

* Total de receitas
* Total de despesas
* Saldo (receita - despesa)

---

### Totais por categoria (opcional)

Retorna:

* Total agrupado por categoria

---

# 🧱 Estrutura do projeto

```bash
Controllers/
Services/
DTOs/
Models/
Data/
```

---

# 🔎 Exemplos de requisição

## Criar pessoa

```json
POST /api/pessoas

{
  "nome": "Pedro",
  "idade": 30
}
```

---

## Criar categoria

```json
POST /api/categorias

{
  "descricao": "Salário",
  "finalidade": "Receita"
}
```

---

## Criar transação

```json
POST /api/transacoes

{
  "descricao": "Pagamento",
  "valor": 1500,
  "tipo": "Receita",
  "pessoaId": 1,
  "categoriaId": 1
}
```

---

# 📖 Documentação da API

A API está documentada utilizando **Scalar**, que consome o padrão OpenAPI.

A documentação inclui:

* Descrição dos endpoints
* Exemplos de requisição
* Regras de negócio
* Tipos de resposta

---

# ⭐ Diferenciais do projeto

* Estrutura em camadas (Controller + Service)
* Regras de negócio bem definidas
* Uso de DTOs
* Documentação OpenAPI completa
* Código limpo e organizado

---

# 👨‍💻 Autor

Desenvolvido por **Pedro Gentil**

---
