# e.Mix - Desafio Consulta CEP

Este projeto consiste em uma Web API desenvolvida com .NET 8 para consulta de endereços via CEP. A API permite consultar um CEP na API externa [ViaCEP](https://viacep.com.br/) e armazenar os resultados em um banco de dados em memória, possibilitando consultas futuras sem a necessidade de acessar a API externa novamente.

## Tecnologias Utilizadas

- **.NET 8** - Plataforma para desenvolvimento da API.
- **Entity Framework Core** - ORM utilizado para persistência em banco de dados em memória.
- **Refit** - Cliente HTTP para consumo da API ViaCEP.
- **AutoMapper** - Biblioteca para mapeamento de objetos.
- **xUnit & Moq** - Testes unitários.
- **Swagger** - Documentação e testes da API.

## Funcionalidades

### Endpoints

1. **`GET /cep/history`** - Retorna todos os endereços consultados e armazenados no banco de dados em memória.
2. **`POST /cep`** - Recebe um `ZipCode` no corpo da requisição, verifica se o CEP já foi consultado e retorna os dados armazenados. Caso contrário, busca na API ViaCEP, armazena no banco de dados e retorna o resultado.

### Validação de Entrada
O `ZipCode` é validado utilizando **DataAnnotations** para garantir que o formato informado seja válido.

## Configuraço do Ambiente

Para rodar o projeto localmente, siga os passos abaixo:

1. **Instale o .NET 8 SDK**
   - Baixe e instale o SDK do .NET 8 no site oficial: [https://dotnet.microsoft.com/pt-br/download](https://dotnet.microsoft.com/pt-br/download)

2. **Clone o repositório**
   ```sh
   git clone https://github.com/luca0898/emix-desafio.git
   cd seu-repositorio
   ```

3. **Restaure as dependências**
   ```sh
   dotnet restore
   ```

4. **Execute a API**
   ```sh
   dotnet run
   ```

## Testes Unitários

Para rodar os testes unitários, utilize o comando:
```sh
dotnet test
```
Isso executa os testes criados com **xUnit** e **Moq**, garantindo a qualidade do serviço.

## Uso do Swagger

O projeto possui o Swagger configurado para exploração e execução dos endpoints de forma interativa. Para acessá-lo:

1. Inicie a API com `dotnet run`
2. Acesse no navegador: [http://localhost:5000/swagger](http://localhost:5000/swagger)

No Swagger, você pode testar os endpoints diretamente na interface gráfica.

---
Este projeto foi desenvolvido para o desafio da empresa **e.Mix**, demonstrando boas práticas de arquitetura, testes e uso de tecnologias modernas no ecossistema .NET.

