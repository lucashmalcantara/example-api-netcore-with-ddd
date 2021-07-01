# Exemplo de implementação de uma API .NET Core com conceitos de DDD



# Decisões de projeto

- Separação do projeto em DLLs diferentes: para que cada camada faça referências apenas para as bibliotecas necessárias.
- Com exceção da camada de aplicação e testes, as demais camadas foram criadas como Class Library (.NET Standard) para garantir maior compatibilidade na referência entre projetos.
  - Nota: importante clicar com o botão direito no projeto criado e acessar a opção Properties para alterar o Target framework para o mais atualizado, visto que o Visual Studio pode criar o projeto com uma versão antiga.
- Camadas:
  - *Domain*: contém o domínio da aplicação, contém as entidades e interfaces necessárias para atender ao negócio.
  - *Infraestructure*: código não relacionado com o domínio que resolve problema comuns como I/O, rede, acesso ao banco e assim por diante.
  - *Services*: representa os serviços do domínio (*domain services*), contém as regras de negócio.
  - Api: engloba tanto a camada de aplicação quanto a camada de serviços de aplicação (*application services*).
- O uso de *repositories* nos *controllers* da aplicação é totalmente aceitável e reduz complexidade, pois menos testes e camadas serão necessárias.
- Assume-se a biblioteca FluentValidation como parte integrante do domínio, dada suas características de facilitar as validações.



# Principais bibliotecas utilizadas

- Documentação da API: Swagger
  - Nuget: Swashbuckle.AspNetCore
  - Para o correto funcionamento, demanda habilitar a opção *XML documentation file*. Para isso, siga os passos: (1) Botão direito no nome do projeto; (2) opção *Properties*; (3) guia *Build*; (4)  habilitar a opção ***XML documentation file***; (5) Adicionar o valor **1591** no campo ***Supress warnings*** separado por ponto e vírgula.
    - Referência: [XML Comments Swagger .Net Core](https://medium.com/c-sharp-progarmming/xml-comments-swagger-net-core-a390942d3329)

# Referências

[Converting integration tests to .NET Core 3.0](https://andrewlock.net/converting-integration-tests-to-net-core-3/)
[Integration Testing with ASP.NET Core 3.1 - Remove the Boiler Plate](https://adamstorr.azurewebsites.net/blog/integration-testing-with-aspnetcore-3-1-remove-the-boiler-plate)