# Backend Challenge - Pokémons

Este é o repositório de um teste técnico de desenvolvimento backend para a Kotas, onde a tarefa era criar uma API para listar, capturar e detalhar Pokémon, consumindo a PokeAPI externa.
A aplicação foi desenvolvida utilizando C# com .NET 8, Entity Framework Core com SQLite e HttpClient para consumir a PokeAPI.

# Tecnologias Utilizadas

- C# com .NET 8
- Entity Framework Core com SQLite
- PokeAPI para buscar dados de Pokémon
- Base64 para conversão de imagens

# Passo a Passo da Implementação

1. Configuração Inicial do Projeto

1.1 - Criei um novo projeto ASP.NET Core Web API utilizando o .NET 8.
1.2 - Adicionei o Entity Framework Core para persistência de dados, utilizando SQLite como banco de dados.
1.3 - Defini a estrutura básica da API com um controller para as operações principais, como listagem, cadastro de mestres e captura de Pokémon.

2. Banco de Dados e Modelo de Dados

2.1 - Modelo de Pokémon
2.2 - O modelo Pokemon foi criado com as propriedades básicas de nome e id.
2.3 - Para o gerenciamento de capturas, foi criada uma tabela de Capture, que relaciona o Pokémon capturado com o mestre Pokémon.

- Problema e Decisão \*
  Inicialmente, não estava claro se seria necessário persistir os dados completos dos Pokémon, ou se apenas os IDs seriam suficientes. Após análise, decidi persistir somente os dados essenciais, como TrainerId, PokemonId, e PokemonName, enquanto os detalhes completos dos Pokémon seriam obtidos diretamente da PokeAPI.

3. Consumindo a PokeAPI

3.1 - A PokeAPI fornece dados completos sobre os Pokémon, incluindo informações como nome, id, evoluções e imagem.
3.2 Utilizei HttpClient para fazer chamadas à PokeAPI, buscando detalhes de cada Pokémon, suas evoluções e sprites.

- Problema e Decisão \*

  3.3 - Decidi utilizar o endpoint /pokemon/{id} para buscar os dados principais de um Pokémon e o endpoint /pokemon-species/{id} para obter informações sobre as evoluções.
  3.4 - Para a imagem do Pokémon, foi necessário fazer uma conversão da URL da imagem para Base64, para que a resposta da API fosse padronizada e não dependesse de recursos externos.

4. Endpoints e Funcionalidades

# GET /pokemons/random

Este endpoint foi criado para retornar 10 Pokémon aleatórios, consumindo a PokeAPI.
Cada Pokémon inclui seu nome, sprite convertido para Base64, e uma lista das evoluções.
Para isso, foi criada uma função que retorna 10 Pokémon aleatórios, utilizando IDs entre 1 e 1000, já que a PokeAPI possui Pokémon com IDs até 1000.

# GET /pokemons/{id}

Este endpoint retorna os detalhes completos de um Pokémon específico, incluindo evoluções e sprite.
Ele utiliza a mesma lógica de consumir a PokeAPI e converter a imagem para Base64.

# POST /trainers

Para o cadastro de mestres Pokémon, foi criado um endpoint que recebe dados como nome, idade e CPF.
Esses dados são armazenados no banco de dados SQLite.

# POST /pokemons/capture

Este endpoint recebe uma solicitação para capturar um Pokémon, associando o Pokémon com o mestre Pokémon.
A captura é registrada na tabela Capture do banco de dados.

# GET /pokemons/captured

Este endpoint retorna a lista de Pokémon capturados, com informações sobre o treinador e o Pokémon capturado.
A lista é obtida diretamente do banco de dados SQLite.

5. Desafios e Soluções

Desafio 1: Consumir a PokeAPI e Manter a Performance
Como a PokeAPI é externa e pode ter uma latência significativa, foi necessário garantir que o sistema não fosse dependente dessa latência para cada requisição.
A solução foi consumir os dados apenas quando necessário (por exemplo, quando um Pokémon fosse capturado ou quando fosse solicitado diretamente), sem armazenar os dados completos de todos os Pokémon.
Desafio 2: Convertendo a Imagem para Base64
Para a conversão da imagem do Pokémon, utilizei o método Convert.ToBase64String para garantir que a imagem fosse representada como uma string em Base64.
Uma consideração importante foi que, ao utilizar imagens em Base64, o tamanho da resposta aumenta, o que pode impactar a performance dependendo da quantidade de dados e imagens retornadas.
Desafio 3: Evitar Dados Duplicados
Para garantir que um Pokémon não fosse capturado mais de uma vez, a tabela de capturas foi configurada para ter uma chave composta (TrainerId e PokemonId), evitando a duplicação de registros.

# Instruções de Instalação e Uso

- Requisitos \*
  1 - .NET 8 ou superior
  2 - SQLite (será criado automaticamente pelo Entity Framework)

- Passos para Executar

1 - Clone o repositório:

git clone <URL>

2 - Navegue até a pasta do projeto:

cd <nome_do_repositorio>

3 - Construa a imagem Docker:

docker build -t pokemons-api .

4 - Execute o Container:

docker run -p 5000:80 pokemons-api

# Exemplos de Requisição

GET /pokemons/random
Retorna uma lista de 10 Pokémon aleatórios, com suas evoluções e imagens em Base64.

POST /trainers
Exemplo de corpo da requisição:

{
"name": "Brock",
"age": 21,
"cpf": "12345678901"
}

POST /pokemons/capture
Exemplo de corpo da requisição:

{
"trainerId": 1,
"pokemonId": 25
}

GET /pokemons/captured
Retorna todos os Pokémon capturados.

# Conclusão

O sistema desenvolvido permite que os usuários capturem Pokémon, vejam detalhes dos Pokémon (incluindo evoluções e imagem em Base64), e gerenciem seus mestres Pokémon. A solução foi desenhada para ser simples e escalável, consumindo dados dinâmicos da PokeAPI quando necessário.
