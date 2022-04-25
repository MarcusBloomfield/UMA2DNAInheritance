using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;
using UMA;

public static class DNALookUpTable
{
    public static Dictionary<string, float[]> dnaConstraints = new Dictionary<string, float[]>
    {
        // General
        { "height", new float[2] { .44f, .44f } },
        { "MuscleTones", new float[2] { 0f, .44f } },

        //Head
        { "headSize", new float[2] {.44f, .5f } },
        { "headWidth", new float[2] { .44f, .55f } },
        { "neckThickness", new float[2] { .2f, .44f } },

        //Arms
        { "forearmLength", new float[2] { .4f, .5f } },
        { "forearmWidth", new float[2] { .44f, .44f } },
        { "armLength", new float[2] { .48f, .54f } },
        { "armWidth", new float[2] { .4f, .6f } },
        { "handsSize", new float[2] { .44f, .5f } },

        //Legs
        { "feetSize", new float[2] { .44f, .55f } },
        { "legSeparation", new float[2] { .44f, .55f } },
        { "upperMuscle", new float[2] { .44f, .55f } },
        { "lowerMuscle", new float[2] { .44f, .55f } },
        { "upperWeight", new float[2] { .44f, .55f } },
        { "lowerWeight", new float[2] { .44f, .55f } },
        { "legsSize", new float[2] { .44f, .55f } },
        { "gluteusSize", new float[2] { .44f, .55f } },

        //Core
        { "belly", new float[2] { .44f, .55f } },
        { "waist", new float[2] { .44f, .55f } },

        //Face
        { "earsSize", new float[2] { .44f, .55f } },
        { "earsPosition", new float[2] { .44f, .55f } },
        { "earsRotation", new float[2] { .44f, .55f } },
        { "noseSize", new float[2] { .44f, .55f } },
        { "noseCurve", new float[2] { .44f, .55f } },
        { "noseWidth", new float[2] { .44f, .55f } },
        { "noseInclination", new float[2] { .44f, .55f } },
        { "nosePosition", new float[2] { .44f, .55f } },
        { "nosePronounced", new float[2] { .44f, .55f } },
        { "noseFlatten", new float[2] { .44f, .55f } },
        { "chinSize", new float[2] { .44f, .55f } },
        { "chinPronounced", new float[2] { .44f, .55f } },
        { "chinPosition", new float[2] { .44f, .55f } },
        { "mandibleSize", new float[2] { .44f, .55f } },
        { "jawSize", new float[2] { .44f, .55f } },
        { "jawPosition", new float[2] { .44f, .55f } },
        { "cheekSize", new float[2] { .44f, .55f } },
        { "cheekPosition", new float[2] { .44f, .55f } },
        { "lowCheekPosition", new float[2] { .44f, .55f } },
        { "foreheadSize", new float[2] { .44f, .55f } },
        { "foreheadPosition", new float[2] { .44f, .55f } },
        { "lipsSize", new float[2] { .44f, .55f } },
        { "mouthSize", new float[2] { .44f, .55f } },
        { "eyeRotation", new float[2] { .44f, .55f } },
        { "eyeSize", new float[2] { .44f, .55f } },
        { "breastSize", new float[2] { .44f, .55f } },
        { "eyeSpacing", new float[2] { .44f, .55f } }
    };
}
