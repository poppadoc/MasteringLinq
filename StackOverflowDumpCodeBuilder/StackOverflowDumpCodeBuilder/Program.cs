﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace StackOverflowDumpCodeBuilder
{   
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<User> users = LoadUsers();
            //IEnumerable<Badge> badges = LoadBadges();
            //IEnumerable<Comment> comments = LoadComments();
            //IEnumerable<Post> posts = LoadPosts();
            //IEnumerable<Vote> votes = LoadVotes();
        }

        private static IEnumerable<User> LoadUsers()
        {
            var xdoc = XDocument.Load(@"..\..\..\Files\users.xml");
            var userMapper = new UserMapper();

            var users = userMapper.Map(xdoc.Descendants("row"));
            var userDictionary = new Dictionary<int, User>();
            foreach (User user in users)
            {
                userDictionary.Add(user.Id, user);
            }
            
            var badges = LoadBadges();
            foreach (Badge badge in badges)
            {
                if (userDictionary.ContainsKey(badge.UserId))
                {
                var user = userDictionary[badge.UserId];                
                user.AddBadge(badge);    
                }                
            }
          
            return users;
        }

        private static IEnumerable<Comment> LoadComments()
        {            
            var xdoc = XDocument.Load(@"..\..\..\Files\comments.xml");            
            var commentMapper = new CommentMapper();
            return commentMapper.Map(xdoc.Descendants("row"));
        }

        private static IEnumerable<Badge> LoadBadges()
        {
            var xdoc = XDocument.Load(@"..\..\..\Files\badges.xml");
            var badgeMapper = new BadgeMapper();
            return badgeMapper.Map(xdoc.Descendants("row"));                        
        }

        private static IEnumerable<Post> LoadPosts()
        {
            var xdoc = XDocument.Load(@"..\..\..\Files\posts.xml");
            var badgeMapper = new PostMapper();
            return badgeMapper.Map(xdoc.Descendants("row"));
        }

        private static IEnumerable<Vote> LoadVotes()
        {
            var xdoc = XDocument.Load(@"..\..\..\Files\votes.xml");
            var voteMapper = new VoteMapper();
            return voteMapper.Map(xdoc.Descendants("row"));            
        }
    }
}