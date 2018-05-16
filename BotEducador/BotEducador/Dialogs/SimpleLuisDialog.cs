using System;
using System.Threading.Tasks;
using BotEducador.Misc;
using FakeQuoteSDK;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace BotEducador.Dialogs
{
    [Serializable]
    [LuisModel("", "")]
    public class SimpleLuisDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("Me desculpe, não consegui entender o que você disse. Ainda estou em fase de aprendizado, mas tente falar de maneira mais clara eu talvez eu consida entender. :D");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Basica_Saudacao")]
        public async Task IntentGreeting(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("Olá. Tudo bem? Posso ajudar com informações sobre ações, o que você deseja saber?");

            IMessageActivity replyActivity = context.MakeMessage();
            replyActivity.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyActivity.Attachments = BotHelpers.GetMainMenuCarousel();

            await context.PostAsync(replyActivity);

            context.Wait(MessageReceived);
        }

        [LuisIntent("Basica_Despedir")]
        public async Task IntentGoodbye(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("Tchau! Até mais!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Basica_Ajuda")]
        public async Task IntentHelp(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("É bem simples, sou um assistente digital e posso te ajudar com informações sobre ativos e investimentos.");
            await context.PostAsync("Me pergunte o que você gostaria de saber e vou fazer o meu melhor para responder!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Basica_Agradecer")]
        public async Task IntentThanks(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("Foi um prazer poder ajudar! Se precisar de mais alguma coisa é só falar. ;)");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Acao_Definicao")]
        public async Task QuoteDefinition(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("Ações são pequenas partes de uma empresa. Ou seja, todos os que são donos de uma ação, são sócios de um pedaço da empresa.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Investir_ComoLucrar")]
        public async Task IntentHowToProfit(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("Você pode ganhar de três formas. Com a alta das ações que comprar, quando a empresa distribui lucros para os acionistas (dividendos) e alugando suas ações.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Investir_Internet")]
        public async Task IntentOnlineInvestment(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("Várias corretoras oferecem o serviço de home broker, uma ferramenta que permite a compra e a venda de ações diretamente pela internet. É rápido, transparente e muito seguro. Com este serviço, você pode acompanhar o mercado e negociar suas ações de casa, no escritório e até durante uma viagem.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Investir_MelhorMomento")]
        public async Task IntentWhenToInvest(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("O momento ideal é quando você estiver seguro de que entendeu a mecânica básica do mercado acionário, ou seja, é uma aplicação com horizonte de resgate de médio e longo prazos, tem bom potencial de rentabilidade e traz também riscos de flutuação no valor investido.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Investir_TamanhoLucro")]
        public async Task IntentHowMuchProfit(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("As ações são investimentos de renda variável, ou seja, não há uma rentabilidade média predeterminada. Antes de investir seu dinheiro, lembre-se que ação é um investimento de risco e para formação de patrimônio de longo prazo. A curto prazo, assim como pode valorizar, também pode desvalorizar.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Investir_Vantagens")]
        public async Task IntentAdvantagesOfInvesting(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            await context.PostAsync("Não é preciso muito dinheiro para começar. Você recebe dividendos periodicamente, tem potencial de boa rentabilidade no longo prazo, pode comprar ou vender suas ações a qualquer momento e é possível alugar suas ações fazendo um empréstimo de ativos e ganhar um rendimento extra.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Acao_Consultar")]
        public async Task IntentGetQuoteValue(IDialogContext context, IAwaitable<IMessageActivity> item, LuisResult result)
        {
            if (result.TryFindEntity("Ativo", out EntityRecommendation quote))
            {
                await GetQuote_PrintQuoteValueAsync(context, quote.Entity);
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
            context.Wait(MessageReceived);
        }
    }
}