# Versionamento API

_Projeto que serve de referência para o versionamento de API .net_.

É um projeto proposto para ser clonado
e reutilizado como base para o versionamento de API.

> _Objetivo: exemplificar o versionamento de API_.

## Iniciando

- `git clone https://github.com/hugo-cesar/versionamento-api.git`
- `cd versionamento-api`

## Pré-requisitos

- `dotnet --version`<br>
  Você deverá ver a indicação da versão do dotnet instalada. Para o projeto executar é necessária a versão 5.
  Instruções para instalação pode ser encontrada em: [dotnet-5](https://docs.microsoft.com/pt-br/dotnet/core/install/windows?tabs=net50)

## Executando a aplicação

Abra o projeto na sua IDE favorita, escolha uma das API's (Set as Startup Project) e a execute. Ao fazer isso as api's serão apresentadas no navegador via swagger, 
caso isso não oconteça, basta abrir o navegador e colocar _https://localhost:5001/api-docs/index.html_. Agora basta escolhar a versão/endpoint que desejar e executar 
para verificar o resultado do versionamento. 
