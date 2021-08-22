using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class MobileNotifications : MonoBehaviour
{
    //list 15
    private List<string> L_Tittle = new List<string>() 
    { "Dababy CAR", "I am Convertible", "Yea-Yea", "Big boy here!", "Let’s go" , "Leeeees Goooo", "Today i'm davil !", "Let's go Drive !", "Someone Better the you !?",
    "Don't worry !", "Can you beat new record  ?" , "Are we Friends ?", "Let's go Drive", "Night City", "So hot !"};

    //COUNT 15
    private List<string> L_Content = new List<string>()
    { "Dababy Car loved to sleep on a bed of...", "Potato wedges probably are not best for relationships.", "Dababy Car Faster the you think", "Is that a pencil?",
        "Let’s go I'm sick of waiting!", "What a big boy he is!", "Do you like animals?", "The eggs are sold by the dozen.", "What a big DaBABY ?!",
        "The tortoise jumped into the lake with dreams of becoming a sea turtle." , "It was her first experience training a rainbow unicorn." , "Do you like my random messages ?",
    "I hear that Dababy Car is very pretty.", "I am going to Russia to learn BABUSHKA next year." , "Don’t get too excited!"};


    private System.Random rand = new System.Random();
    private void Awake()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Name = "All Notifications",
            Description = "All notifications about game (News, gameplay etc...)",
            Id = "allNotif",
            Importance = Importance.High,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void OnApplicationPause(bool pause)
    {
#if UNITY_ANDROID

        // Off all Notif

        if (pause)
        {

            //Random
            int randNum = rand.Next(20, 45);
            int randList = rand.Next(0, L_Tittle.Count);

            DateTime timeNotify = DateTime.Now.AddSeconds(randNum);

            //Send Notif
            AndroidNotification And_Notif = new AndroidNotification()
            {
                Title = L_Tittle[randList],
                Text = L_Content[randList],
                FireTime = timeNotify,
               // SmallIcon = "icon_small",
                LargeIcon = "icon_large"
            };

            AndroidNotificationCenter.SendNotification(And_Notif, channelId: "allNotif");
        }

#endif

    }
}
