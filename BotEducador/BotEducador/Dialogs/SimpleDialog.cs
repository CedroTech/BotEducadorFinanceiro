using System;
using System.Threading.Tasks;
using FakeQuoteSDK;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotEducador.Dialogs
{
    [Serializable]
    public class SimpleDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text == "consultar ativo")
            {
                GetQuoteInformation(context);
            }
            else
            {
                switch (activity.Text)
                {
                    case "oi":
                        await context.PostAsync("Olá. Tudo bem? Posso ajudar com informações sobre ações, o que você deseja saber?");
                        break;

                    case "tchau":
                        await context.PostAsync("Tchau! Até mais!");
                        break;

                    case "ajuda":
                        await context.PostAsync("É bem simples, sou um assistente digital e posso te ajudar com informações sobre ativos e investimentos.");
                        await context.PostAsync("Me pergunte o que você gostaria de saber e vou fazer o meu melhor para responder!");
                        break;

                    case "obrigado":
                        await context.PostAsync("Foi um prazer poder ajudar! Se precisar de mais alguma coisa é só falar. ;)");
                        break;

                    case "o que são ações?":
                        await context.PostAsync("Ações são pequenas partes de uma empresa. Ou seja, todos os que são donos de uma ação, são sócios de um pedaço da empresa.");
                        break;

                    case "como lucrar?":
                        await context.PostAsync("Você pode ganhar de três formas. Com a alta das ações que comprar, quando a empresa distribui lucros para os acionistas (dividendos) e alugando suas ações.");
                        break;

                    case "posso investir pela internet?":
                        await context.PostAsync("Várias corretoras oferecem o serviço de home broker, uma ferramenta que permite a compra e a venda de ações diretamente pela internet. É rápido, transparente e muito seguro. Com este serviço, você pode acompanhar o mercado e negociar suas ações de casa, no escritório e até durante uma viagem.");
                        break;

                    case "qual o melhor momento para investir?":
                        await context.PostAsync("O momento ideal é quando você estiver seguro de que entendeu a mecânica básica do mercado acionário, ou seja, é uma aplicação com horizonte de resgate de médio e longo prazos, tem bom potencial de rentabilidade e traz também riscos de flutuação no valor investido.");
                        break;

                    case "quanto posso ganhar?":
                        await context.PostAsync("As ações são investimentos de renda variável, ou seja, não há uma rentabilidade média predeterminada. Antes de investir seu dinheiro, lembre-se que ação é um investimento de risco e para formação de patrimônio de longo prazo. A curto prazo, assim como pode valorizar, também pode desvalorizar.");
                        break;

                    case "quais as vantagens de investir?":
                        await context.PostAsync("Não é preciso muito dinheiro para começar. Você recebe dividendos periodicamente, tem potencial de boa rentabilidade no longo prazo, pode comprar ou vender suas ações a qualquer momento e é possível alugar suas ações fazendo um empréstimo de ativos e ganhar um rendimento extra.");
                        break;

                    default:
                        await context.PostAsync("Me desculpe, não consegui entender o que você disse. Ainda estou em fase de aprendizado, mas tente falar de maneira mais clara eu talvez eu consida entender. :D");
                        break;
                }

                context.Wait(MessageReceivedAsync);
            }
        }

        private void GetQuoteInformation(IDialogContext context)
        {
            PromptDialog.Text(context, GetQuote_AfterUserInputAsync, "Qual o ativo ou empresa você deseja consultar?");
        }

        private async Task GetQuote_AfterUserInputAsync(IDialogContext context, IAwaitable<string> result)
        {
            string quote = await result;

            QuoteResult quoteResult = FakeQuote.GetPrice(quote);

            await context.PostAsync($"O preço do ativo {quoteResult.Name} é {quoteResult.Value:C2}.");

            context.Wait(MessageReceivedAsync);
        }
    }
}