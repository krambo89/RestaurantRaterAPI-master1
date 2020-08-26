using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    // Restaurant Entity (The class that gets stored in the database)
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        
        public double Rating
        {
            get
            {
                //Calculate a total average score based on Ratings
                double totalAverageRating = 0;

                //Add all Ratings together to get total average
                foreach(var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                //Return Average if count is above zero
                return (Ratings.Count > 0) ? totalAverageRating / Ratings.Count : 0;
            }
        }


        //Average Food Rating
        public double FoodRating
        {
            get
            {
                double totalFoodScore = 0;
                foreach(var rating in Ratings)
                {
                    totalFoodScore += rating.FoodScore;
                }
                return (Ratings.Count > 0) ? totalFoodScore / Ratings.Count : 0;
            }
        }


        //Average Environment Rating
        public double EnvironmentRating
        {
            get
            {
                IEnumerable<double> scores = Ratings.Select(rating => rating.EnvironmentScore);

                double totalEnvironmentScore = scores.Sum();

                return (Ratings.Count > 0) ? totalEnvironmentScore / Ratings.Count : 0;
            }
        }


        //Average Cleanliness Rating
        public double CleanlinessRating
        {
            get
            {
                double totalCleanlinessRating = 0;
                foreach (var rating in Ratings)
                {
                    totalCleanlinessRating += rating.CleanlinessScore;
                }
                return (Ratings.Count > 0) ? totalCleanlinessRating / Ratings.Count : 0;
            }
        }



        // public bool IsRecommended => Rating > 3.5;
        public bool IsRecommended
        {
            get
            {
                return Rating > 8.5;
            }
        }

        // All of the associated rating objects from the database
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}