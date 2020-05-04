﻿using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Com.Airbnb.Lottie;
using System.Collections.Generic;

namespace Globo.Droid
{
    [Activity(Label = "Globo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, NoHistory = true)]
    public class SplashActivity : Activity, Animator.IAnimatorListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashScreen);

            LottieAnimationView animationView = FindViewById<LottieAnimationView>(Resource.Id.animation_view);

            animationView.AddAnimatorListener(this);
            animationView.PlayAnimation();

        }
        public override void OnBackPressed() { }

        public void OnAnimationCancel(Animator animation)
        {
        }

        public void OnAnimationEnd(Animator animation)
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));

        }

        public void OnAnimationRepeat(Animator animation)
        {
        }

        public void OnAnimationStart(Animator animation)
        {

        }
    }
}