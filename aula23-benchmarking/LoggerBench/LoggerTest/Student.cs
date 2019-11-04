using System;

namespace LoggerTest
{
    public class Student
    {
        public int Nr { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public string GithubId { get; set; }
        public DateTime BirthDate { get; set; }

        public Student(int nr, String name, int group, string githubId, DateTime b)
        {
            this.Nr = nr;
            this.Name = name;
            this.Group = group;
            this.GithubId = githubId;
            this.BirthDate = b;
        }
    }

}
