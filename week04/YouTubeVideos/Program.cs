using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    // Comment class
    public class Comment
    {
        public string CommenterName { get; private set; }
        public string Text { get; private set; }

        public Comment(string commenterName, string text)
        {
            CommenterName = commenterName ?? "";
            Text = text ?? "";
        }
    }

    // Video class
    public class Video
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int LengthSeconds { get; private set; }

        private readonly List<Comment> _comments = new List<Comment>();

        public Video(string title, string author, int lengthSeconds)
        {
            Title = title ?? "Untitled";
            Author = author ?? "Unknown";
            LengthSeconds = Math.Max(0, lengthSeconds);
        }

        public void AddComment(Comment comment)
        {
            if (comment != null) _comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return _comments.Count;
        }

        public IReadOnlyList<Comment> GetComments()
        {
            return _comments.AsReadOnly();
        }
    }

    // Program
    class Program
    {
        static void Main(string[] args)
        {
            List<Video> videos = CreateSampleVideos();

            // Display all videos
            foreach (var video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length (seconds): {video.LengthSeconds}");
                Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
                Console.WriteLine("Comments:");
                foreach (var c in video.GetComments())
                {
                    Console.WriteLine($"  - {c.CommenterName}: {c.Text}");
                }
                Console.WriteLine(new string('-', 40));
            }

            // Keep console open if run by double-click (optional)
            // Console.WriteLine("Press Enter to exit...");
            // Console.ReadLine();
        }

        static List<Video> CreateSampleVideos()
        {
            var videos = new List<Video>();

            var v1 = new Video("How to Cook Fufu", "Chef Kiongo", 320);
            v1.AddComment(new Comment("Serge", "Very helpful tutorial!"));
            v1.AddComment(new Comment("Amina", "I tried it and it worked."));
            v1.AddComment(new Comment("Patrick", "Nice video!"));
            videos.Add(v1);

            var v2 = new Video("Learn C# in 10 Minutes", "CodeMaster", 600);
            v2.AddComment(new Comment("John", "This was fast and clear."));
            v2.AddComment(new Comment("Maria", "Thanks for the explanation."));
            v2.AddComment(new Comment("Kevin", "Great intro!"));
            videos.Add(v2);

            var v3 = new Video("Tour of Lubumbashi", "TravelDRC", 450);
            v3.AddComment(new Comment("Sarah", "Beautiful city!"));
            v3.AddComment(new Comment("Michel", "Thanks for sharing."));
            v3.AddComment(new Comment("Linda", "Loved the landmarks."));
            videos.Add(v3);

            return videos;
        }
    }
}
