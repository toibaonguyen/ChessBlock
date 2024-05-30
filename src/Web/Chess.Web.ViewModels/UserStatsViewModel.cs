namespace Chess.Web.ViewModels
{
    using Chess.Data.Models;
    using Chess.Services.Mapping;

    public class UserStatsViewModel : IMapFrom<StatisticEntity>
    {
        public int Played { get; set; }

        public int Won { get; set; }

        public int Drawn { get; set; }

        public int Lost { get; set; }

        public int EloRating { get; set; }
    }
}
