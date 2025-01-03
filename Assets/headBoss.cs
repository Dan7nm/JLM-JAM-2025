using UnityEngine;

public class headBoss : MonoBehaviour
{
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talking()
    {
        anim.SetBool("isTalking", true);
        Invoke("StopTalking", 3f);
    }

    private void StopTalking()
    {
        anim.SetBool("isTalking", false);
    }
}
