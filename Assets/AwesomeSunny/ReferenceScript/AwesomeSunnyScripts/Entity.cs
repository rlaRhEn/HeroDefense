using System.Collections;
using System.Collections.Generic;
using TMPro;
using UGS;
using UnityEngine;

 
public class Entity : MonoBehaviour
{
    public int id;


    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 2; 
    public float jumpPower = 5;
    public PlaceUIElementAtWorldPosition uiText;
    private string localizationName = null;

    /// <summary>
    /// UGS Data Load Code (Local Cached)
    /// </summary>
    public void UGSLoadData()
    {
        UnityGoogleSheet.LoadAllData();
        SunnyLand.UnitEntity entityData = SunnyLand.UnitEntity.UnitEntityMap[this.id];
        Reload();
    }
    public void Reload()
    {
        SunnyLand.UnitEntity data = SunnyLand.UnitEntity.UnitEntityMap[id];
        this.speed = data.moveSpeed;
        this.jumpPower = data.jumpPower; 
        this.transform.localScale = data.size;
        ReloadLocalization();
      
    }

    public void ReloadLocalization()
    {
        SunnyLand.UnitEntity data = SunnyLand.UnitEntity.UnitEntityMap[id];
        this.localizationName = LocalizationManager.Instance.GetSunnyName(data.localeID);
        if (uiText != null)
            uiText.GetComponent<TextMeshProUGUI>().text = this.localizationName;
    }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        var text = Resources.Load<GameObject>("FollowText");
        var obj = Instantiate<GameObject>(text); 
        obj.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
        var tmpro = obj.GetComponent<TextMeshProUGUI>();
        var moveText = obj.GetComponent<PlaceUIElementAtWorldPosition>();
        moveText.target = this.gameObject;
        moveText.offset = new Vector3(0, this.transform.localScale.y, 0);
        uiText = moveText;


        UGSLoadData(); 
 
    } 
    protected void Flip(bool value)
    {
        GetComponent<SpriteRenderer>().flipX = value;
    }

    public virtual void Update()
    {
         
    }
     

    public void MovementLogic(float moveVelocityX, bool jump)
    {        //Move
        float move = moveVelocityX; 
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if (Mathf.Abs(rb.velocity.x) >= 0.1f)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        //Flip
        bool flip = false;
        flip = (move > 0) ? false : true;

        if (move != 0)
        {
            Flip(flip);
        }

        if (jump && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }



    }
}
