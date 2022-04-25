using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using marcus;

public class UMADNAInheritance : MonoBehaviour
{
    [SerializeField] SharedColorTable hairColorTable;
    [SerializeField] SharedColorTable skinColorTable;
    [SerializeField] List<UMATextRecipe> maleRecipes = new List<UMATextRecipe>();
    [SerializeField] List<UMATextRecipe> femaleRecipes = new List<UMATextRecipe>();

    [SerializeField] List<DynamicCharacterAvatar> parents = new List<DynamicCharacterAvatar>();
    [SerializeField] List<DynamicCharacterAvatar> children = new List<DynamicCharacterAvatar>();
    [SerializeField] GameObject avatarPrefab;

    //creates two parents
    //randomises their dna
    //gives their dna to a child

    public void CreateTestFamily()
    {
        KillPeople(parents, children);
        CreateParents();
        CreateChildren();
    }
    public void CreateParents()
    {
        //the sex of the avatar needs to be set first then the dna can be set
        parents = SpawnAvatars(2, new Vector3(2, 0, -1));
        SetHalfOfAvatarsToFemale(parents);
        SetRandomHairStyleByGender(parents);
        SetRandomFeatureColor(parents, hairColorTable, "Hair");
        SetRandomFeatureColor(parents, skinColorTable, "Skin");
        RandomizeDna(parents);
    }
    public void CreateChildren()
    {
        if (parents.Count > 0)
        {
            //the sex of the avatar needs to be set first then the dna can be set
            children = SpawnAvatars(Random.Range(1, 3), new Vector3(-2, 0, -1));
            RandomizeSex(children);
            SetRandomHairStyleByGender(children);
            InheritFeature(parents, children, "Hair");
            InheritFeature(parents, children, "Skin");
            InheritDNA(children);
        }
        else throw new System.NullReferenceException("The parents list is empty!");
    }
    void InheritFeature(List<DynamicCharacterAvatar> parents, List<DynamicCharacterAvatar> children, string featureName)
    {
        OverlayColorData parentOne = parents[0].GetColor(featureName);
        OverlayColorData parentTwo = parents[1].GetColor(featureName);

        foreach (DynamicCharacterAvatar item in children)
        {
            OverlayColorData child = item.GetColor(featureName);

            child.color = parentOne.color + parentTwo.color / 2;

            item.SetColor(featureName, child);
        }
    }
    void SetRandomFeatureColor(List<DynamicCharacterAvatar> avatars, SharedColorTable sharedColorTable, string featureName)
    {
        foreach (var avatar in avatars) 
        { 
            avatar.SetColor(featureName, sharedColorTable.colors[Random.Range(0, skinColorTable.colors.Length)]);
        }
    }
    void SetRandomHairStyleByGender(List<DynamicCharacterAvatar> avatars)
    {
        foreach (var avatar in avatars)
        {
            if (avatar.activeRace.name == "HumanMale")
            {
                avatar.SetSlot(maleRecipes[Random.Range(0, maleRecipes.Count)]);
            }
            else
            {
                avatar.SetSlot(femaleRecipes[Random.Range(0, femaleRecipes.Count)]);
            }
        }
    }
    void InheritDNA(List<DynamicCharacterAvatar> children)
    {
        foreach (DynamicCharacterAvatar child in children)
        {
            child.name = " Child";
            Dictionary<string, DnaSetter> childDNA = child.GetDNA();
            Dictionary<string, float> combinedDNA = CombineParentDNA(parents);

            foreach (KeyValuePair<string, DnaSetter> item in childDNA)
            {
                if (combinedDNA.ContainsKey(item.Key))
                {
                    childDNA[item.Key].Set(combinedDNA[item.Key]);
                }
            }

            child.BuildCharacter(true);
            child.ForceUpdate(true, true, true);
        }
    }
    Dictionary<string, float> CombineParentDNA(List<DynamicCharacterAvatar> parents)
    {
        // could reference actual dna inheritance models in order to get dna features in a more realistic way

        Dictionary<string, float> combinedDNAValues  = new Dictionary<string, float>();

        Dictionary<string, DnaSetter> fatherDNA = parents[0].GetDNA();
        Dictionary<string, DnaSetter> motherDNA = parents[1].GetDNA();

        foreach (KeyValuePair<string, DnaSetter> item in motherDNA)
        {
            if (fatherDNA.ContainsKey(item.Key))
            {
                if (DNALookUpTable.dnaConstraints.ContainsKey(item.Key)) 
                {
                    float[] constraints = DNALookUpTable.dnaConstraints[item.Key];
                    float dnaValue = item.Value.Value + fatherDNA[item.Key].Value / 2;
                    float mappedDNAValue = MathTools.map(dnaValue, 0, 1, constraints[0], constraints[1]);
                    combinedDNAValues.Add(item.Key, mappedDNAValue);
                }
            }
        }

        return combinedDNAValues;
    }
    void RandomizeDna(List<DynamicCharacterAvatar> avatars)
    {
        foreach (var avatar in avatars)
        {
            Dictionary<string, DnaSetter> setters = avatar.GetDNA();

            foreach (KeyValuePair<string, DnaSetter> item in setters)
            {
                if (setters.ContainsKey(item.Key))
                {
                    if (DNALookUpTable.dnaConstraints.ContainsKey(item.Key))
                    {
                        float[] constraints = DNALookUpTable.dnaConstraints[item.Key];
                        float dnaValue = RandomDNA();
                        float mappedDNAValue = MathTools.map(dnaValue, 0, 1, constraints[0], constraints[1]);
                        item.Value.Set(mappedDNAValue);
                    }
                }
            }

            avatar.BuildCharacter(true);
            avatar.ForceUpdate(true, true, true);
        }
    }
    float RandomDNA() => 0.35f + (Random.value * 0.3f);
    void SetHalfOfAvatarsToFemale(List<DynamicCharacterAvatar> avatars)
    {
        for (int i = 0; i < avatars.Count; i++)
        {
            if (i % 2 == 0)
            {
                avatars[i].name = " Mother";
                SetFemale(avatars[i]);
            }
            else
            {
                avatars[i].name = " Father";
                SetMale(avatars[i]);
            }
        }
    }
    void RandomizeSex(List<DynamicCharacterAvatar> avatars)
    {
        foreach (var avatar in avatars)
        {
            if (Random.Range(0,100) > 50) SetFemale(avatar);
            else SetMale(avatar);
        }
    }
    List<DynamicCharacterAvatar> SpawnAvatars(int amount, Vector3 spawnOrigin)
    {
        List<DynamicCharacterAvatar> avatars = new List<DynamicCharacterAvatar>();
        for (int i = 0; i < amount; i++)
        {
           var avatar = CreateFromPrefab(avatarPrefab, GetRandomSpawnPosition(spawnOrigin));
           avatars.Add(avatar);
        }
        return avatars;
    }
    DynamicCharacterAvatar CreateFromPrefab(GameObject prefab, Vector3 spawnPosition)
    {
        GameObject go = GameObject.Instantiate(prefab);
        DynamicCharacterAvatar dca = go.GetComponent<DynamicCharacterAvatar>();
        go.transform.localPosition = spawnPosition;
        go.SetActive(true);
        return dca;
    }
    void SetFemale(DynamicCharacterAvatar avatar) => avatar.ChangeRace("HumanFemale", DynamicCharacterAvatar.ChangeRaceOptions.useDefaults, true);
    void SetMale(DynamicCharacterAvatar avatar) => avatar.ChangeRace("HumanMale", DynamicCharacterAvatar.ChangeRaceOptions.useDefaults, true);
    Vector3 GetRandomSpawnPosition(Vector3 origin) => new Vector3(origin.x + Random.Range(-1.0f, 1.0f), 0, 0);
    void KillPeople(List<DynamicCharacterAvatar> parents, List<DynamicCharacterAvatar> children)
    {
        ListTools.DestoryListofGameObjects(parents.ConvertAll(x => x.gameObject));
        ListTools.DestoryListofGameObjects(children.ConvertAll(x => x.gameObject));
    }
}
