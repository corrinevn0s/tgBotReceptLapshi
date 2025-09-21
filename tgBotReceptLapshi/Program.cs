using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tgBotReceptLapshi.Models;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("7968756740:AAE1od8cOUtDXTMkq460BJo705ssH15hQQU", cancellationToken: cts.Token);
var me = await bot.GetMe();
//список блюд
List<Dish> Dishes = new List<Dish>
{
    new Dish()
    {
        Name = "Бомж спагетти",
        Look = "https://www.iguides.ru/upload/iblock/b16/hx016ec36v6wuio3iowevad4xrc34b4g.jpg",
        Recipe = "Доширак с сосисками и майонезом\n\n" +
        "Ингредиенты:\n" +
        "• Лапша быстрого приготовления — 1 уп.\n" +
        "• Сосиски — 2–3 шт.\n" +
        "• Майонез — 1–2 ст. л.\n" +
        "• Кипяток, специи из пакетика — по вкусу\n\n" +
        "Шаги:\n" +
        "1) Вскипятить воду. Лапшу переложить в контейнер и всыпать сухую приправу (масло/соус пока отложить).\n" +
        "2) Залить кипятком до отметки, накрыть и настоять 3–5 минут.\n" +
        "3) Пока настаивается, нарезать сосиски кружочками.\n" +
        "4) По желанию слить лишний бульон, добавить сосиски, масло/соус из пакетика и перемешать.\n" +
        "5) Выдавить сверху майонез зигзагом и перемешать перед едой."
    },
    new Dish()
    {
        Name = "Домашний супчик", // Очень вкусный рил
        Look = "https://i2015.otzovik.com/2015/10/21/2524247/img/44962079.jpg",
        Recipe = "Суп с лапшой Ролтон\n\n" +
        "Ингредиенты:\n" +
        "• Лапша быстрого приготовления (Ролтон) — 1 уп.\n" +
        "• Картофель — 2–3 шт.\n" +
        "• Морковь — 1 шт.\n" +
        "• Зелень, специи из пакетика — по вкусу\n\n" +
        "Шаги:\n" +
        "1) Нарезать картофель кубиками, морковь натереть.\n" +
        "2) Вскипятить ~1,5 л воды, добавить овощи и варить 10 минут.\n" +
        "3) Всыпать лапшу и приправу, варить ещё 5 минут.\n" +
        "4) Добавить зелень, по желанию масло.\n" +
        "5) Настоять пару минут и подавать горячим."
    },
    new Dish()
    {
        Name = "Сладенькое",
        Look = "https://cs12.pikabu.ru/post_img/2021/12/07/7/og_og_1638873533292978843.jpg",
        Recipe = "Бутер с маслом и сахаром ☠\n \n" +
        "Ингрeдиенты:\n" +
        "• Хлеб - 1 слайс.\n" +
        "• Масло - 10 грамм. \n" +
        "• Сахар - 1 чайная ложка. \n\n" +
        "Шаги: \n" +
        "1) Намазать масло на ломтик хлеба.\n" +
        "2) Посыпать сахаром сверху. \n" +
        "3) Радоваться."
    }
};
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop the bot

// method that handle messages received by the bot:

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text == "/start")
    {
        var sent = await bot.SendMessage(msg.Chat, "Привет! Хочешь кушать?",
                                 replyMarkup: new string[] { "Да,бро", "Нет,бро" });
    }
    
    // Добавляем данный фрагмент
    if (msg.Text == "Нет,бро")
    {
        await bot.SendMessage(msg.Chat, "Лучше поешь!");
    }
    if (msg.Text == "Да,бро")
    {
        
        var sent = await bot.SendMessage(msg.Chat, "Выбери блюдо",
                                 replyMarkup: Dishes.Select(i => $"Потрясающий {i.Name}").ToArray());

    }
    if (msg.Text == "Потрясающий Бомж спагетти")
    {
        var dish = Dishes.FirstOrDefault(i => i.Name == "Бомж спагетти");
        if (dish is null)
        {
            await bot.SendMessage(msg.Chat, "Ошибка блюд нет");
        }
        await PrintDish(msg, dish);
    }
    if (msg.Text == "Потрясающий Домашний супчик")
    {
        var dish = Dishes.FirstOrDefault(i => i.Name == "Домашний супчик");
        if (dish is null)
        {
            await bot.SendMessage(msg.Chat, "Ошибка блюд нет");
        }
        await PrintDish(msg, dish);
    }
    if (msg.Text == "Потрясающий Сладенькое")
    {
        var dish = Dishes.FirstOrDefault(i => i.Name == "Сладенькое");
        if (dish is null)
        {
            await bot.SendMessage(msg.Chat, "Ошибка блюд нет");
        }
        await PrintDish(msg, dish);
    }
}
async Task PrintDish (Message msg,Dish dish)
{
    var message = await bot.SendPhoto(msg.Chat,
            InputFile.FromUri(dish.Look),
            $"ВЫ ВЫБРАЛИ ПОТРЯСАЮЩЕЕ БЛЮДО {
                dish.Name} \n Рецепт😋: \n {dish.Recipe}"
            );
}