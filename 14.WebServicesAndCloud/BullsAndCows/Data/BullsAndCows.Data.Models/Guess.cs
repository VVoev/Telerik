﻿namespace BullsAndCows.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Web.Common.Constants;

    public class Guess
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int BullsCount { get; set; }

        public int CowsCount { get; set; }

        [Required]
        [MinLength(GameConstants.GuessNumberLength)]
        [MaxLength(GameConstants.GuessNumberLength)]
        public string Number { get; set; }

        public DateTime DateMade { get; set; }
    }
}
