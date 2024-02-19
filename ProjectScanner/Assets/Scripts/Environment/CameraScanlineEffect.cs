using System.Collections;
using UnityEngine;

public class CameraScanlineEffect : MonoBehaviour
{
    public Material mat;
	private Animator anim;
    private int effectSlider;
	public float f = 0;
	public float minF = 0;

    private void Start(){
        anim = this.GetComponent<Animator>();
        effectSlider = Shader.PropertyToID("_distSlider");
		mat.SetFloat(effectSlider, f);
    }

	public void EffectPlay(float targetVal = 0f){
		//this.GetComponent<AudioSource>().Play();
		//anim.PlayInFixedTime("ScanlineAnimation");
		//yield return new WaitForSeconds(3f);
		Debug.Log("yield over " + targetVal);
		minF = targetVal;
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest){
		if(f == 0 && minF == 0) {
			Graphics.Blit(src, dest);
			return;
		}
		else if (minF != 0)
		{
			f = minF;
			mat.SetFloat(effectSlider, f);
			Graphics.Blit(src, dest, mat);
		}
		else {
			mat.SetFloat(effectSlider, minF); 
			Graphics.Blit(src, dest, mat);
		}
		
	}
}
