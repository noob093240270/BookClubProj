﻿namespace BookClubProj.Server.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<ReadBook> UsersReaded { get; set; } = new List<ReadBook>();
    }
}
