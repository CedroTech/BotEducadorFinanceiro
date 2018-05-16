using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotEducador.Misc
{
    public static class BotHelpers
    {
        public static List<Attachment> GetMainMenuCarousel()
        {
            List<HeroCard> cards = new List<HeroCard>
            {
                new HeroCard
                {
                    Images = new List<CardImage> { new CardImage { Url = "https://udia.blob.core.windows.net/imghost/Acao_Definicao.png" } },
                    Title = "O que é uma ação?",
                    Text = "Deseja saber mais sobre ações? Veja aqui.",
                    Buttons = new List<CardAction>
                    {
                        new CardAction
                        {
                            Type = ActionTypes.ImBack,
                            Title = "Saber Mais",
                            Value = "O que é uma ação?"
                        }
                    }
                },
                new HeroCard
                {
                    Images = new List<CardImage> { new CardImage { Url = "https://udia.blob.core.windows.net/imghost/Investir_ComoLucrar.png" } },
                    Title = "Como ter retorno?",
                    Text = "Utilize esta opção e saiba como ter retorno.",
                    Buttons = new List<CardAction>
                    {
                        new CardAction
                        {
                            Type = ActionTypes.ImBack,
                            Title = "Saber Mais",
                            Value = "Como lucrar com ações?"
                        }
                    }
                },
                new HeroCard
                {
                    Images = new List<CardImage> { new CardImage { Url = "https://udia.blob.core.windows.net/imghost/Investir_Internet.png" } },
                    Title = "Investir pela internet?",
                    Text = "Quer investir online? Saiba como.",
                    Buttons = new List<CardAction>
                    {
                        new CardAction
                        {
                            Type = ActionTypes.ImBack,
                            Title = "Saber Mais",
                            Value = "Posso investir pela internet?"
                        }
                    }
                },
                new HeroCard
                {
                    Images = new List<CardImage> { new CardImage { Url = "https://udia.blob.core.windows.net/imghost/Investir_MelhorMomento.png" } },
                    Title = "Quando investir?",
                    Text = "Saiba sobre o melhor momento para investir.",
                    Buttons = new List<CardAction>
                    {
                        new CardAction
                        {
                            Type = ActionTypes.ImBack,
                            Title = "Saber Mais",
                            Value = "Qual o melhor momento para investir?"
                        }
                    }
                },
                new HeroCard
                {
                    Images = new List<CardImage> { new CardImage { Url = "https://udia.blob.core.windows.net/imghost/Investir_TamanhoLucro.png" } },
                    Title = "Quanto posso ganhar?",
                    Text = "Está curioso sobre o retorno do seu investimento?",
                    Buttons = new List<CardAction>
                    {
                        new CardAction
                        {
                            Type = ActionTypes.ImBack,
                            Title = "Saber Mais",
                            Value = "Qual o lucro se eu investir em ações?"
                        }
                    }
                },
                new HeroCard
                {
                    Images = new List<CardImage> { new CardImage { Url = "https://udia.blob.core.windows.net/imghost/Investir_Vantagens.png" } },
                    Title = "Quais as vantagens de investir?",
                    Text = "Por que investir? Saiba as vantagens!",
                    Buttons = new List<CardAction>
                    {
                        new CardAction
                        {
                            Type = ActionTypes.ImBack,
                            Title = "Saber Mais",
                            Value = "Quais as vantagens de investir em ações?"
                        }
                    }
                }
            };

            List<Attachment> attachments = new List<Attachment>();

            cards.ForEach(c => attachments.Add(c.ToAttachment()));

            return attachments;            
        }
    }
}