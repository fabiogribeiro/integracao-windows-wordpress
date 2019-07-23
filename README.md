# Repositório

Este repositório é um programa para Windows feito com o WPF framework para contactar com uma loja Woocommerce.

## Descrição


O programa pretende demonstrar a interação de um utilizador a fazer operações numa loja online. É possível adicionar produtos, ou encomendas, como também é atualizado o estado de encomendas da loja se forem alteradas (esta última só é feita do painel de administrador). Há um pedido ao website a cada alguns segundos regularmente para manter a consistência, se bem que há outras possibilidades numa situação real.

É utilizada uma base de dados SQLite para simplicar a execução do programa noutros computadores. No entando, utilizando o Entity Framework o código fica praticamente o mesmo utilizando outra base de dados como, por exemplo, SQL server.

## Código

Existe uma MainWindow que é resposável por regularmente atualizar duas páginas: Encomendas e Produtos. Estas, por sua vez, lidam com a lógica que atualiza a informação. Há também uma pagina para criar um produto. Além destes foi introduzida uma classe APIController para interagir com o website de forma abstrata e apresentar objetos relevantes e conhecidos à base de dados.

### Nota

O download está localizado nas releases. Para executar pode ser necessário atualizar o .NET Framework.
