using System;
using System.Linq;
using System.Threading.Tasks;
using FakeQuoteSDK;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Cognitive.LUIS;

namespace BotEducador.Dialogs
{
    [Serializable]
    public class NaturalLanguageDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            LuisClient luis = new LuisClient("", "");

            LuisResult luisResult = await luis.Predict(activity.Text);

            await HandleLuisIntentAsync(context, luisResult);
        }

        private async Task HandleLuisIntentAsync(IDialogContext context, LuisResult luisResult)
        {

            if (luisResult.TopScoringIntent.Name == "Acao_Consultar")
            {
                await GetQuoteInformation(context, luisResult);
            }
            else
            {
                switch (luisResult.TopScoringIntent.Name)
                {
                    case "Basica_Saudacao":
                        await context.PostAsync("Olá. Tudo bem? Posso ajudar com informações sobre ações, o que você deseja saber?");
                        break;

                    case "Basica_Despedir":
                        await context.PostAsync("Tchau! Até mais!");
                        break;

                    case "Basica_Ajuda":
                        await context.PostAsync("É bem simples, sou um assistente digital e posso te ajudar com informações sobre ativos e investimentos.");
                        await context.PostAsync("Me pergunte o que você gostaria de saber e vou fazer o meu melhor para responder!");
                        break;

                    case "Basica_Agradecer":
                        await context.PostAsync("Foi um prazer poder ajudar! Se precisar de mais alguma coisa é só falar. ;)");
                        break;

                    case "Acao_Definicao":
                        await context.PostAsync("Ações são pequenas partes de uma empresa. Ou seja, todos os que são donos de uma ação, são sócios de um pedaço da empresa.");
                        break;

                    case "Investir_ComoLucrar":
                        await context.PostAsync("Você pode ganhar de três formas. Com a alta das ações que comprar, quando a empresa distribui lucros para os acionistas (dividendos) e alugando suas ações.");
                        break;

                    case "Investir_Internet":
                        await context.PostAsync("Várias corretoras oferecem o serviço de home broker, uma ferramenta que permite a compra e a venda de ações diretamente pela internet. É rápido, transparente e muito seguro. Com este serviço, você pode acompanhar o mercado e negociar suas ações de casa, no escritório e até durante uma viagem.");
                        break;

                    case "Investir_MelhorMomento":
                        await context.PostAsync("O momento ideal é quando você estiver seguro de que entendeu a mecânica básica do mercado acionário, ou seja, é uma aplicação com horizonte de resgate de médio e longo prazos, tem bom potencial de rentabilidade e traz também riscos de flutuação no valor investido.");
                        break;

                    case "Investir_TamanhoLucro":
                        await context.PostAsync("As ações são investimentos de renda variável, ou seja, não há uma rentabilidade média predeterminada. Antes de investir seu dinheiro, lembre-se que ação é um investimento de risco e para formação de patrimônio de longo prazo. A curto prazo, assim como pode valorizar, também pode desvalorizar.");
                        break;

                    case "Investir_Vantagens":
                        await context.PostAsync("Não é preciso muito dinheiro para começar. Você recebe dividendos periodicamente, tem potencial de boa rentabilidade no longo prazo, pode comprar ou vender suas ações a qualquer momento e é possível alugar suas ações fazendo um empréstimo de ativos e ganhar um rendimento extra.");
                        break;

                    case "None":
                    default:
                        await context.PostAsync("Me desculpe, não consegui entender o que você disse. Ainda estou em fase de aprendizado, mas tente falar de maneira mais clara eu talvez eu consida entender. :D");
                        break;
                }

                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task GetQuoteInformation(IDialogContext context, LuisResult luisResult)
        {
            Microsoft.Cognitive.LUIS.Entity ativo = luisResult.GetAllEntities().FirstOrDefault(e => e.Name == "Ativo");

            if (ativo != null)
            {
                await GetQuote_PrintQuoteValueAsync(context, ativo.Value);
            }
            else
            {
                PromptDialog.Text(context, GetQuote_AfterUserInputAsync, "Qual o ativo ou empresa você deseja consultar?");
            }            
        }

        private async Task GetQuote_AfterUserInputAsync(IDialogContext context, IAwaitable<string> result)
        {
            string quote = await result;
            await GetQuote_PrintQuoteValueAsync(context, quote);
        }

        private async Task GetQuote_PrintQuoteValueAsync(IDialogContext context, string quote)
        {
            QuoteResult quoteResult = FakeQuote.GetPrice(quote);
            await context.PostAsync($"O preço do ativo {quoteResult.Name} é {quoteResult.Value:C2}.");
            context.Wait(MessageReceivedAsync);
        }
    }
}