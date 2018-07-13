using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace premier_league_genetic_algorithm.Models
{
    public class Player
    {
        public int id { get; set; }        
        public string web_name { get; set; }
        public int team_code { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }                
        public int now_cost { get; set; }        
        public int? chance_of_playing_this_round { get; set; }
        public int? chance_of_playing_next_round { get; set; }
        public string value_form { get; set; }
        public string value_season { get; set; }                                
        public bool in_dreamteam { get; set; }
        public int dreamteam_count { get; set; }
        public string selected_by_percent { get; set; }
        public string form { get; set; }
        public int transfers_out { get; set; }
        public int transfers_in { get; set; }
        public int transfers_out_event { get; set; }
        public int transfers_in_event { get; set; }
        public int loans_in { get; set; }
        public int loans_out { get; set; }
        public int loaned_in { get; set; }
        public int loaned_out { get; set; }
        public int total_points { get; set; }
        public int event_points { get; set; }
        public double points_per_game { get; set; }
        public string ep_this { get; set; }
        public string ep_next { get; set; }
        public bool special { get; set; }
        public int minutes { get; set; }
        public int goals_scored { get; set; }
        public int assists { get; set; }
        public int clean_sheets { get; set; }
        public int goals_conceded { get; set; }
        public int own_goals { get; set; }
        public int penalties_saved { get; set; }
        public int penalties_missed { get; set; }
        public int yellow_cards { get; set; }
        public int red_cards { get; set; }
        public int saves { get; set; }
        public int bonus { get; set; }
        public int bps { get; set; }
        public string influence { get; set; }
        public string creativity { get; set; }
        public string threat { get; set; }
        public float ict_index { get; set; }
        public int ea_index { get; set; }
        public Role element_type { get; set; }
        public int team { get; set; }
    }
}
