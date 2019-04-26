using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGenerator : MonoBehaviour
{
    public SkinnedMeshRenderer BaseMesh;
    private float RandomNumber;
    public Slider StomachSlider;
    public List<Slider> FaceSliders;
    public List<Slider> BodySliders;
    public GameObject BaseObject, FemaleObject, MaleObject;
    private GameObject[] BodySlidersOBJ, FaceSlidersOBJ;
    //BLENDSHAPES
    private int StomachInc = 0;
    private int PelvisInc = 1;
    private int LegsInc = 2;
    private int ArmsInc = 3;
    private int NeckInc = 4;
    private int StomachDec = 5;
    private int PelvisDec = 6;
    private int LegsDec = 7;
    private int ArmsDec = 8;
    private int NeckDec = 9;
    private int JawInc = 10;
    private int JawDec = 11;
    private int MouthInc = 12;
    private int MouthDec = 13;
    private int NoseWidthInc = 14;
    private int NoseWidthDec = 15;
    private int NoseLengthInc = 16;
    private int NoseLengthDec = 17;
    private int EyesInc = 18;
    private int EyesDec = 19;
    private int EarsInc = 20;
    private int EarsDec = 21;
    private int ScalpDec = 23;
    //BLENDSHAPES END

    // Start is called before the first frame update
    void Start()
    {
        BaseMesh = BaseObject.GetComponent<SkinnedMeshRenderer>();
        BodySlidersOBJ = GameObject.FindGameObjectsWithTag("BodySlider");
        FaceSlidersOBJ = GameObject.FindGameObjectsWithTag("FaceSlider");
        foreach (GameObject sldr in BodySlidersOBJ)
        {
            sldr.SetActive(false);
            BodySliders.Add(sldr.GetComponent<Slider>());
        }
        foreach (GameObject sldr in FaceSlidersOBJ)
        {
            sldr.SetActive(false);
            FaceSliders.Add(sldr.GetComponent<Slider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        RandomNumber = Random.Range(0.0f, 100.0f);

        if (Input.GetKey(KeyCode.A))
        {
            BaseObject.transform.Rotate(new Vector3(0.0f, 2.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            BaseObject.transform.Rotate(new Vector3(0.0f, -2.0f, 0.0f));
        }
    }

    public void Male()
    {
        MaleObject.SetActive(true);
        FemaleObject.SetActive(false);
        BaseObject = MaleObject;
        BaseObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        BaseMesh = MaleObject.GetComponent<SkinnedMeshRenderer>();
    }
    public void Female()
    {
        FemaleObject.SetActive(true);
        MaleObject.SetActive(false);
        BaseObject = FemaleObject;
        BaseObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        BaseMesh = FemaleObject.GetComponent<SkinnedMeshRenderer>();
    }
    public void ActivateBodySliders()
    {
        foreach(GameObject bodySlider in BodySlidersOBJ)
        {
            if(bodySlider.activeSelf == true)
            {
                bodySlider.SetActive(false);
            }
            else
            {
                bodySlider.SetActive(true);
            }           
        }
    }
    public void ActivateFaceSliders()
    {
        foreach(GameObject faceSlider in FaceSlidersOBJ)
        {
            if(faceSlider.activeSelf == true)
            {
                faceSlider.SetActive(false);
            }
            else
            {
                faceSlider.SetActive(true);
            }           
        }
    }
    ///FUNCTIONS FOR RANDOM GENERATON
    ////////////////////////////////////
    ///////////////////////////////////
    ///
    private void GenerateRandomProportionalBody(float RandNum)
    {
        float IncORDec = Random.Range(0, 2);
        Debug.Log("Increase or decrease random number: " + IncORDec.ToString());
        if(IncORDec < 1)
        {
            //RESET TO ORIGINAL BODY
            BaseMesh.SetBlendShapeWeight(StomachDec, 0);
            BaseMesh.SetBlendShapeWeight(PelvisDec, 0);
            BaseMesh.SetBlendShapeWeight(LegsDec, 0);
            BaseMesh.SetBlendShapeWeight(ArmsDec, 0);
            BaseMesh.SetBlendShapeWeight(NeckDec, 0);

            //APPLY NEW CHANGES
            BaseMesh.SetBlendShapeWeight(StomachInc, RandNum);
            BaseMesh.SetBlendShapeWeight(PelvisInc, RandNum);
            BaseMesh.SetBlendShapeWeight(LegsInc, RandNum);
            BaseMesh.SetBlendShapeWeight(ArmsInc, RandNum);
            BaseMesh.SetBlendShapeWeight(NeckInc, RandNum);
            
            for(int i = 0; i < BodySliders.Count; i++)
            {
                BodySliders[i].value = RandNum;
            }

        }
        else
        {
            //RESET TO ORIGINAL BODY
            BaseMesh.SetBlendShapeWeight(StomachInc, 0);
            BaseMesh.SetBlendShapeWeight(PelvisInc, 0);
            BaseMesh.SetBlendShapeWeight(LegsInc, 0);
            BaseMesh.SetBlendShapeWeight(ArmsInc, 0);
            BaseMesh.SetBlendShapeWeight(NeckInc, 0);

            //APPLY NEW CHANGES
            BaseMesh.SetBlendShapeWeight(StomachDec, RandNum);
            BaseMesh.SetBlendShapeWeight(PelvisDec, RandNum);
            BaseMesh.SetBlendShapeWeight(LegsDec, RandNum);
            BaseMesh.SetBlendShapeWeight(ArmsDec, RandNum);
            BaseMesh.SetBlendShapeWeight(NeckDec, RandNum);

            for (int i = 0; i < BodySliders.Count; i++)
            {
                BodySliders[i].value = -RandNum;
            }
        }
    }

    public void ResetOriginal()
    {
        for(int i = 0; i < 23; i++)
        {
            BaseMesh.SetBlendShapeWeight(i, 0);
        }
        for(int j = 0; j < FaceSliders.Count; j++)
        {
            FaceSliders[j].value = 0;
        }
        for(int k = 0; k < BodySliders.Count; k++)
        {
            BodySliders[k].value = 0;
        }
    }
    public void ResetOriginalBody()
    {
        //RESET ONLY BODY
        BaseMesh.SetBlendShapeWeight(StomachInc, 0);
        BaseMesh.SetBlendShapeWeight(PelvisInc, 0);
        BaseMesh.SetBlendShapeWeight(LegsInc, 0);
        BaseMesh.SetBlendShapeWeight(ArmsInc, 0);
        BaseMesh.SetBlendShapeWeight(NeckInc, 0);
        BaseMesh.SetBlendShapeWeight(JawInc, 0);
        BaseMesh.SetBlendShapeWeight(StomachDec, 0);
        BaseMesh.SetBlendShapeWeight(PelvisDec, 0);
        BaseMesh.SetBlendShapeWeight(LegsDec, 0);
        BaseMesh.SetBlendShapeWeight(ArmsDec, 0);
        BaseMesh.SetBlendShapeWeight(NeckDec, 0);

        for(int i = 0; i < BodySliders.Count; i++)
        {
            BodySliders[i].value = 0;
        }
    }
    public void ResetOriginalFace()
    {
        //RESET ONLY FACE
        BaseMesh.SetBlendShapeWeight(MouthDec, 0);
        BaseMesh.SetBlendShapeWeight(NoseWidthDec, 0);
        BaseMesh.SetBlendShapeWeight(NoseLengthDec, 0);
        BaseMesh.SetBlendShapeWeight(EyesDec, 0);
        BaseMesh.SetBlendShapeWeight(EarsDec, 0);
        BaseMesh.SetBlendShapeWeight(ScalpDec, 0);
        BaseMesh.SetBlendShapeWeight(MouthInc, 0);
        BaseMesh.SetBlendShapeWeight(NoseWidthInc, 0);
        BaseMesh.SetBlendShapeWeight(NoseLengthInc, 0);
        BaseMesh.SetBlendShapeWeight(EyesInc, 0);
        BaseMesh.SetBlendShapeWeight(EarsInc, 0);
        BaseMesh.SetBlendShapeWeight(JawDec, 0);

        for (int i = 0; i < FaceSliders.Count; i++)
        {
            FaceSliders[i].value = 0;
        }
    }

    public void GenerateRandomProportionalFace(float RandNum)
    {
        float IncORDec = Random.Range(0, 2);

        if(IncORDec < 1)
        {
            //RESET ORIGINAL FACE
            BaseMesh.SetBlendShapeWeight(MouthDec, 0);
            BaseMesh.SetBlendShapeWeight(NoseWidthDec, 0);
            BaseMesh.SetBlendShapeWeight(NoseLengthDec, 0);
            BaseMesh.SetBlendShapeWeight(EyesDec, 0);
            BaseMesh.SetBlendShapeWeight(EarsDec, 0);
            BaseMesh.SetBlendShapeWeight(ScalpDec, 0);
            BaseMesh.SetBlendShapeWeight(JawDec, 0);

            //APPLY NEW CHANGES
            BaseMesh.SetBlendShapeWeight(MouthInc, RandNum);
            BaseMesh.SetBlendShapeWeight(NoseWidthInc, RandNum);
            BaseMesh.SetBlendShapeWeight(NoseLengthInc, RandNum);
            BaseMesh.SetBlendShapeWeight(EyesInc, RandNum);
            BaseMesh.SetBlendShapeWeight(EarsInc, RandNum);
            BaseMesh.SetBlendShapeWeight(ScalpDec, -RandNum);
            BaseMesh.SetBlendShapeWeight(JawDec, RandNum);

            for (int i = 0; i < FaceSliders.Count; i++)
            {
                FaceSliders[i].value = RandNum;
            }
        }
        else
        {
            //RESET ORIGINAL FACE
            BaseMesh.SetBlendShapeWeight(MouthInc, 0);
            BaseMesh.SetBlendShapeWeight(NoseWidthInc, 0);
            BaseMesh.SetBlendShapeWeight(NoseLengthInc, 0);
            BaseMesh.SetBlendShapeWeight(EyesInc, 0);
            BaseMesh.SetBlendShapeWeight(EarsInc, 0);
            BaseMesh.SetBlendShapeWeight(ScalpDec, 0);
            BaseMesh.SetBlendShapeWeight(JawDec, 0);

            //APPLY NEW CHANGES
            BaseMesh.SetBlendShapeWeight(MouthDec, RandNum);
            BaseMesh.SetBlendShapeWeight(NoseWidthDec, RandNum);
            BaseMesh.SetBlendShapeWeight(NoseLengthDec, RandNum);
            BaseMesh.SetBlendShapeWeight(EyesDec, RandNum);
            BaseMesh.SetBlendShapeWeight(EarsDec, RandNum);
            BaseMesh.SetBlendShapeWeight(ScalpDec, RandNum);
            BaseMesh.SetBlendShapeWeight(JawDec, RandNum);

            for (int i = 0; i < FaceSliders.Count; i++)
            {
                FaceSliders[i].value = -RandNum;
            }

        }
    }

    public void GenerateRandomProportionalBodyButton()
    {
        GenerateRandomProportionalBody(RandomNumber);
    }
    public void GenerateRandomProportionalFaceButton()
    {
        GenerateRandomProportionalFace(RandomNumber);
    }
    ///RANDOM GENERATION END
    //////////////////////////
    //////////////////////////
    ///
    
    ///FUNCTIONS FOR PERSONALIZED GENERATION
    ///////////////////////////////////////////
    //////////////////////////////////////////
    ///

    public void Stomach(Slider slider)
    {
        if(slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(StomachInc, slider.value);
            BaseMesh.SetBlendShapeWeight(StomachDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(StomachDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(StomachInc, 0);
        }        
    }
    public void Pelvis(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(PelvisInc, slider.value);
            BaseMesh.SetBlendShapeWeight(PelvisDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(PelvisDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(PelvisInc, 0);
        }
    }
    public void Arms(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(ArmsInc, slider.value);
            BaseMesh.SetBlendShapeWeight(ArmsDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(ArmsDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(ArmsInc, 0);
        }
    }
    public void Legs(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(LegsInc, slider.value);
            BaseMesh.SetBlendShapeWeight(LegsDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(LegsDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(LegsInc, 0);
        }
    }
    public void Neck(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(NeckInc, slider.value);
            BaseMesh.SetBlendShapeWeight(NeckDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(NeckDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(NeckInc, 0);
        }
    }
    public void Jaw(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(JawInc, slider.value);
            BaseMesh.SetBlendShapeWeight(JawDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(JawDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(JawInc, 0);
        }
    }
    public void Mouth(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(MouthInc, slider.value);
            BaseMesh.SetBlendShapeWeight(MouthDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(MouthDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(MouthInc, 0);
        }
    }
    public void NoseWidth(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(NoseWidthInc, slider.value);
            BaseMesh.SetBlendShapeWeight(NoseWidthDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(NoseWidthDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(NoseWidthInc, 0);
        }
    }
    public void NoseLength(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(NoseLengthInc, slider.value);
            BaseMesh.SetBlendShapeWeight(NoseLengthDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(NoseLengthDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(NoseLengthInc, 0);
        }
    }
    public void Eyes(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(EyesInc, slider.value);
            BaseMesh.SetBlendShapeWeight(EyesDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(EyesDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(EyesInc, 0);
        }
    }
    public void Ears(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(EarsInc, slider.value);
            BaseMesh.SetBlendShapeWeight(EarsDec, 0);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(EarsDec, -slider.value);
            BaseMesh.SetBlendShapeWeight(EarsInc, 0);
        }
    }
    public void Scalp(Slider slider)
    {
        if (slider.value > 0)
        {
            BaseMesh.SetBlendShapeWeight(ScalpDec, -slider.value);
        }
        else
        {
            BaseMesh.SetBlendShapeWeight(ScalpDec, -slider.value);
        }
    }

    ///PERSONALIZED GENERATION END
    /////////////////////////////////
    /////////////////////////////////
    ///
    public void BakeMesh()
    {
        GameObject newObj = new GameObject();
        
        Mesh mesh = new Mesh();
        BaseObject.GetComponent<SkinnedMeshRenderer>().BakeMesh(mesh);
        newObj.AddComponent<SkinnedMeshRenderer>();
        newObj.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
        newObj.GetComponent<SkinnedMeshRenderer>().materials = BaseObject.GetComponent<SkinnedMeshRenderer>().materials;
        if(BaseObject.tag == "BaseMale")
        {
            newObj.transform.localScale = new Vector3(37.59319f, 37.59319f, 37.59319f);
        }
        else if(BaseObject.tag == "BaseFemale")
        {
            newObj.transform.localScale = new Vector3(151.0862f, 151.0862f, 151.0862f);
        }
        ExportMesh(newObj.GetComponent<SkinnedMeshRenderer>());
        newObj.SetActive(false);
    }
    public void ExportMesh(SkinnedMeshRenderer meshToExport)
    {
        this.gameObject.GetComponent<ObjExporter>().currentTexture = meshToExport.material.mainTexture as Texture2D;
        this.gameObject.GetComponent<ObjExporter>().MeshToFile(meshToExport, "Body.obj");
        
    }
}
