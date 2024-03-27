using CalculoFreteApi.Models;

namespace CalculoFreteApi.Business
{
    public class Frete
    {
        #region public methods
        public double RetornaValorDoFrete(ProdutoModel produto, double distancia)
        {
            #region declaração de variáveis 
            const double adValorem = 0.3 / 100; // 0.3%;
            const double gris = 0.3 / 100; // 0.3%;
            const int fatorCubagem = 300;
            double pesoCubado;
            double acrescimoAdValorem;
            double acrescimoGris;
            double fretePeso;
            double valorPorKm;
            double valorTotalPorDistancia;
            double valorPorPeso;
            double valorTotalPorPesoEDistancia;
            double valorDoFrete;
            #endregion

            //Calcula o valor do peso cubado
            pesoCubado = CalculaCubagemDaCarga(produto, fatorCubagem);
            //Retorna o maior valor entre o o peso do produto e o peso cubado
            fretePeso = RetornaFretePeso(produto.peso, pesoCubado);
            //Calcula o acréscimo baseado no valor do adValorem e do produto
            acrescimoAdValorem = CalculaAcrescimoAdValorem(adValorem, produto.valor);
            //Calcula o acréscimo baseado no valor do gris e do produto
            acrescimoGris = CalculaAcrescimoGris(gris, produto.valor);
            //Calcula o valor por Km baseado na distancia
            valorPorKm = CalculaValorPorKm(distancia);
            //Calcula o valor por km vezes a distancia
            valorTotalPorDistancia = CalculaValorPorKmVezesDistancia(distancia, valorPorKm);
            //Calcula o valor baseado no peso
            valorPorPeso = CalculaValorPorPeso(fretePeso);
            //Multiplica o valorPorPeso pelo valorTotalPorDistancia
            valorTotalPorPesoEDistancia = CalculaValorPorPesoVezesOValorTotalPorDistancia(valorPorPeso, valorTotalPorDistancia);
            //Finalmente, soma tudo e calcula o valor do frete
            valorDoFrete = CalculaValorDoFrete(acrescimoAdValorem, acrescimoGris, fretePeso, valorTotalPorPesoEDistancia);


            return valorDoFrete;
        }
        #endregion

        #region private methods
        private double CalculaAcrescimoAdValorem(double adValorem, double valorProduto)
            => valorProduto * adValorem;
        private double CalculaCubagemDaCarga(ProdutoModel produto, int fatorCubagem)
            => produto.comprimento * produto.altura * produto.largura * fatorCubagem;
        private double RetornaFretePeso(double pesoProduto, double pesoCubado)
        {
            double fretePeso;
            fretePeso = pesoCubado > pesoProduto ? pesoCubado : pesoProduto;

            return fretePeso;
        }
        private double CalculaAcrescimoGris(double gris, double valorProduto)
            => valorProduto * gris;
        private double CalculaValorPorKm(double distancia)
        {
            double valorPorKM = 0;

            if (distancia <= 100)
                valorPorKM = 2.10;
            

            if (distancia >= 101 && distancia <= 1000)
                valorPorKM = 1.10;
            

            if (distancia >= 1001 && distancia <= 3000)
                valorPorKM = 0.50;
           

            if (distancia > 3000)
                valorPorKM = 0.30;
            
            return valorPorKM;
        }
        private double CalculaValorPorKmVezesDistancia(double distancia, double valorPorKM)
            => distancia * valorPorKM;
        private double CalculaValorPorPeso(double peso)
        {
            double valorPorPeso = 0;

            if (peso >= 1000)
                valorPorPeso = 1;
            

            if (peso >= 1000 && peso <= 10000)
                valorPorPeso = 0.5;
            

            if (peso <= 20000)
                valorPorPeso = 0.25;
            
            return valorPorPeso;
        }
        private double CalculaValorPorPesoVezesOValorTotalPorDistancia(double valorPorPeso, double valorTotalPorDistancia)
            => valorPorPeso * valorTotalPorDistancia;
        private double CalculaValorDoFrete(double acrescimoAdValorem, double acrescimoGris, double fretePeso, double valorTotalPorPesoEDistancia)
            => acrescimoAdValorem + acrescimoGris + fretePeso + valorTotalPorPesoEDistancia;
        #endregion
    }
}
