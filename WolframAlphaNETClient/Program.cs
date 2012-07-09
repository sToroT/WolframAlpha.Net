﻿using System;
using System.Linq;
using WolframAlphaNET;
using WolframAlphaNET.Objects;

namespace RestSharpWolframAlpha
{
    public class Program
    {
        private const string AppId = "INSERT YOUR APPID HERE";

        static void Main(string[] args)
        {
            //Create the Engine.
            WolframAlpha wolfram = new WolframAlpha(AppId);
            wolfram.ScanTimeout = 0.1f; //We set ScanTimeout really low to get a quick answer. See RecalculateResults() below.

            //We search for something. Notice that we spelled it wrong.
            QueryResult results = wolfram.Query("Who is Danald Duck?");

            //This fetches the pods that did not complete. It is only here to show how to use it.
            //This returns the pods, but also adds them to the original QueryResults.
            results.RecalculateResults();

            //Here we output the Wolfram|Alpha results.
            if (results.Error != null)
                Console.WriteLine("Woops, where was an error:" + results.Error.Msg);

            if (results.DidYouMeans.HasElements())
            {
                foreach (DidYouMean didYouMean in results.DidYouMeans)
                {
                    Console.WriteLine("Did you mean: " + didYouMean.Value);
                }
            }

            Console.WriteLine();

            //Results are split into "pods" that contain information. Those pods can also have subpods.
            Pod primaryPod = results.GetPrimaryPod();

            if (primaryPod != null)
            {
                Console.WriteLine(primaryPod.Title);
                if (primaryPod.SubPods.HasElements())
                {
                    foreach (SubPod subPod in primaryPod.SubPods)
                    {
                        Console.WriteLine(subPod.Title);
                        Console.WriteLine(subPod.Plaintext);
                    }
                }
            }

            if (results.Warnings != null)
            {
                if (results.Warnings.Translation != null)
                    Console.WriteLine("Translation: " + results.Warnings.Translation.Text);

                if (results.Warnings.SpellCheck != null)
                    Console.WriteLine("Spellcheck: " + results.Warnings.SpellCheck.Text);
            }

            Console.ReadLine();
        }
    }
}
