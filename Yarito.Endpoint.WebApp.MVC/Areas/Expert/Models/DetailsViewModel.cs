using System.ComponentModel.DataAnnotations;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;

namespace Yarito.Endpoint.WebApp.MVC.Areas.Expert.Models
{
    public class DetailsViewModel
    {
        public RequestFullDto? Request { get; set; }
        public BidFullDto? Bid { get; set; }
        public AppUserSummaryDto? Customer { get; set; }
        public int RequestId { get; set; }


        [Required(ErrorMessage = "شما باید قیمتی را برای این پیشنهاد ثبت کنید")]
        [Range(typeof(decimal), "0", "1000000000000", ErrorMessage = "قیمت پیشنهادی نامعتبر است.")]
        public decimal ProposedPrice { get; init; }

        [Required(ErrorMessage = "شما باید تاریخی را برای مراجعه مشخص کنید")]
        public DateTime ProposedVisitDateTime { get; init; }

        [StringLength(1000, ErrorMessage = "توضیحات باید حداکثر 1000 کاراکتر باشد.")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; init; }
    }
}
