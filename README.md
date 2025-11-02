# SubstanciasAPI

API para gerenciar substâncias com autenticação via Keycloak, utilizando PostgreSQL como banco de dados.

---

## 🚀 Configuração do Ambiente

### 1. Keycloak

- **URL do Keycloak:** `http://localhost:8080`
- **Nome do Realm:** `appAPI`
- **Nome do Cliente da API:** `nome-nova-api`
- **ClientId:** `nova-api`
- **Tipo de Cliente:** `Confidential`
- **Secret do Client:** `bXrgHfjPLVpsHWHGFAsR8WrTTCIAiujQ`
- **URL do Realm (Authority):** `http://localhost:8080/realms/appAPI`
- **RequireHttpsMetadata:** `false`

> **Observação sobre token:**  
> O client `minha-api` tem um mapper de audience configurado.  
> Isso garante que os tokens JWT emitidos contenham:
> - `nova-api` → necessário para a API aceitar o token
> - `account` → usado internamente pelo Keycloak  
> 
> Quem for testar a API precisa gerar o token a partir do client correto e garantir que contenha `aud: nova-api`.

---

### 2. Banco de Dados (PostgreSQL)

- **String de conexão:**  
```text
Host=localhost;Port=5432;Database=substancias_db;Username=postgres;Password=postgres
Portas da aplicação:

https://localhost:7179

http://localhost:5165

📦 Endpoints da API
Substancias
Obter todas
http
Copiar código
GET /api/substancia
Authorization: Bearer <token>
Obter por ID
http
Copiar código
GET /api/substancia/{id}
Authorization: Bearer <token>
Exemplo de resposta:

json
Copiar código
{
  "id": 3,
  "nome": "Ibuprofeno",
  "codigo": 1235,
  "descricao": "Anti-inflamatório",
  "notas": "Uso moderado",
  "categoria": {
    "id": 4,
    "nome": "Antiinflamatórios"
  },
  "propriedades": [
    {
      "propriedadeId": 1,
      "nomePropriedade": "É seguro para gestantes?",
      "valorBool": true,
      "valorDecimal": null
    },
    {
      "propriedadeId": 2,
      "nomePropriedade": "Dosagem (mg)",
      "valorBool": null,
      "valorDecimal": 400
    },
    {
      "propriedadeId": 3,
      "nomePropriedade": "Pode ser administrado em crianças?",
      "valorBool": true,
      "valorDecimal": null
    },
    {
      "propriedadeId": 4,
      "nomePropriedade": "Via de administração",
      "valorBool": null,
      "valorDecimal": null
    },
    {
      "propriedadeId": 5,
      "nomePropriedade": "Pode causar sonolência?",
      "valorBool": false,
      "valorDecimal": null
    }
  ]
}
Criar Substancia
http
Copiar código
POST /api/substancia
Authorization: Bearer <token>
Content-Type: application/json
Exemplo JSON:

json
Copiar código
{
  "nome": "Ibuprofeno",
  "codigo": 1235,
  "descricao": "Anti-inflamatório",
  "notas": "Uso moderado",
  "categoriaId": 4,
  "propriedades": [
    {
      "propriedadeId": 1,
      "valorBool": true,
      "valorDecimal": null
    },
    {
      "propriedadeId": 2,
      "valorBool": null,
      "valorDecimal": 400
    },
    {
      "propriedadeId": 3,
      "valorBool": true,
      "valorDecimal": null
    },
    {
      "propriedadeId": 4,
      "valorBool": null,
      "valorDecimal": null
    },
    {
      "propriedadeId": 5,
      "valorBool": false,
      "valorDecimal": null
    }
  ]
}
Dica: use codigo único crescente para cada substância criada.

Atualizar Substancia
http
Copiar código
PUT /api/substancia/{id}
Authorization: Bearer <token>
Content-Type: application/json
Exemplo JSON de update (mantendo sequência de IDs únicos):

json
Copiar código
{
  "nome": "Ibuprofeno",
  "codigo": 1235,
  "descricao": "Anti-inflamatório",
  "notas": "Uso moderado",
  "categoriaId": 4,
  "propriedades": [
    {
      "propriedadeId": 1,
      "valorBool": true,
      "valorDecimal": null
    },
    {
      "propriedadeId": 2,
      "valorBool": null,
      "valorDecimal": 400
    },
    {
      "propriedadeId": 3,
      "valorBool": true,
      "valorDecimal": null
    },
    {
      "propriedadeId": 4,
      "valorBool": null,
      "valorDecimal": null
    },
    {
      "propriedadeId": 5,
      "valorBool": false,
      "valorDecimal": null
    }
  ]
}
Deletar Substancia
http
Copiar código
DELETE /api/substancia/{id}
Authorization: Bearer <token>
🔒 Autenticação
Todos os endpoints requerem Bearer token JWT emitido pelo Keycloak.
Exemplo de header:

http
Copiar código
Authorization: Bearer <token>
💡 Observações
Certifique-se de que a CategoriaId exista no banco antes de criar ou atualizar uma substância.

Sempre criptografe campos sensíveis (nome, descricao, notas) conforme implementado nos métodos da API.

Para evitar ciclos no JSON, utilize DTOs para leitura e escrita.

⚡ Iniciando a aplicação
Configure a string de conexão no appsettings.json.

Configure o Keycloak conforme os dados acima.

Execute a aplicação com dotnet run ou via Visual Studio.

Acesse Swagger para testar os endpoints:
https://localhost:7179/swagger/index.html
