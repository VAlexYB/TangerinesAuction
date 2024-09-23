using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TangerinesAuction.Core.Abstractions.Services;
using TangerinesAuction.Core.Models;
using TangerinesAuction.Infrastructure.Settings;

namespace TangerineAuction.Infrastructure.Services
{
    public class NotificationService : INotificationsService
    {
        private readonly string _apiKey;
        private readonly string _listId;
        private readonly string _auctionName;
        private readonly string _auctionEmail;

        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient, IOptions<UniSenderSettings> uniSenderSettings, IOptions<AuctionSettings> auctionSettings)
        {
            _httpClient = httpClient;

            var uniSender = uniSenderSettings.Value;
            var auction = auctionSettings.Value;

            _apiKey = uniSender.ApiKey;
            _listId = uniSender.ListId;
            _auctionName = auction.Name;
            _auctionEmail = auction.Email;
        }

        public async Task SendAuctionWinNotificationAsync(User user, Tangerine tangerine)
        {
            var email = user.Email;
            var subject = "You win the auction!";
            var body = $"<strong>Congratulations {user.Email}</strong>, you won the auction for Tangerine {tangerine.Id}!";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendBidOutbidNotificationAsync(User user, Tangerine tangerine)
        {
            var email = user.Email;
            var subject = "Your bid was overbdid!";
            var body = $"<strong>Your bid on Tangerine {tangerine.Id}</strong> has been outbid.";

            await SendEmailAsync(email, subject, body);
        }

        private async Task SendEmailAsync(string emailTo, string subject, string body)
        {
            var requestUrl = $"https://api.unisender.com/ru/api/sendEmail?format=json&api_key={_apiKey}" +
                         $"&email={emailTo}" +
                         $"&sender_name={_auctionName}" +
                         $"&sender_email={_auctionEmail}" +
                         $"&subject={subject}" +
                         $"&body={body}" +
                         $"&list_id={_listId}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Ошибка в отправке сообщения через UniSender.");
            }
        }
    }
}
