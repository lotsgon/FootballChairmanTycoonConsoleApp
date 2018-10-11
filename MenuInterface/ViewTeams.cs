using EasyConsole;
using FootballChairmanTycoonConsoleApp;
using System.Collections.Generic;

namespace Demo.Pages
{
    class ViewTeams : MenuPage
    {
        public ViewTeams(Program program, List<FootballClub> clubs)
            : base("View Teams", program)
        {
            foreach (FootballClub club in clubs)
            {
                program.AddPage(new TeamView(program, club));
                base.Menu.Add(new Option($"{club.Name}", () => NavigateToClubPage(program, club)));
            }
        }

        private void NavigateToClubPage(Program program, FootballClub club)
        {
            program.AddPage(new TeamView(program, club));
            program.NavigateTo <TeamView>();
        }
    }
}
