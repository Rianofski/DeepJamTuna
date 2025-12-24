using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody MyRigidbdoy;
    bool canjump = true;
    bool canclimb= false;
    bool canDash = true;

    public bool isJumping = false;
    public bool isClimbing = false;
    public bool isWallJumping = false;

    float originalVeloMag;
    public float veloMag = 5;
    public float JumpMag = 25;
    public float DashConstant = 30;
    public float VeloGainConstant = 0.8f;
    public float JumpYatayVeloConstant = 20f;

    public float EkstraDusmeCoef = 0.1f;
    float originalDrag;
    public float Climbdrag;

    public GameObject StaminaMaviBar;

    public float stamina = 100;
    public float staminaConstant = 5;
    public float staminaDolmaConstant = 25;
    public float ZiplamaStaminaCost = 20;
    public float DashStaminaCost = 20;

   
    public float TirmanmaAcisi;
    bool DuvarYanalEksenTirmanilabilir;
    float nonClimbingTime = 0;

    Rigidbody MyRB;

    public Transform PlayerObject;
    void Start()
    {
        originalVeloMag = veloMag;
        stamina = 100;
        originalDrag = GetComponent<Rigidbody>().drag;
        MyRigidbdoy = GetComponent<Rigidbody>();
        StaminaMaviBar.transform.parent.gameObject.SetActive(false);

        MyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 tempV2 = new(0, 1);
        Vector2 tempV1 = new(MyRB.velocity.x, MyRB.velocity.z);

        float angle = Vector2.SignedAngle(tempV1, tempV2);


        if (Vector3.SqrMagnitude(MyRB.velocity) >= 0.2f&&!isClimbing)
        {
            PlayerObject.rotation = Quaternion.Euler(0, angle, 0);
            PlayerObject.gameObject.GetComponent<Animator>().SetBool("Walking", true);
        }
           

        else
            PlayerObject.gameObject.GetComponent<Animator>().SetBool("Walking", false);
        
        if(isClimbing)
        {

            PlayerObject.rotation = Quaternion.Euler(0, TirmanmaAcisi, 0);
        }
        else
            PlayerObject.GetComponent<Animator>().speed = 1;

        //if(isJumping||isWallJumping)
        //    PlayerObject.gameObject.GetComponent<Animator>().SetBool("Havadami", true);
        //else
        //    PlayerObject.gameObject.GetComponent<Animator>().SetBool("Havadami", false);



        if (MyRB.velocity.y < -1) 
        {
            PlayerObject.GetComponent<Animator>().SetBool("Dusmekte", true);
            veloMag = originalVeloMag / 4;
            Vector3 temp = MyRB.velocity;
            if (temp.y >= -5f)
            {
                temp.y -= EkstraDusmeCoef;
                MyRB.velocity = temp;
            }
             
        }
        else
        {
            PlayerObject.GetComponent<Animator>().SetBool("Dusmekte", false);
            veloMag = originalVeloMag;
        }

        //if(isJumping||isWallJumping)
        //    veloMag = originalVeloMag / 4;
        //else
        //    veloMag = originalVeloMag;
       
        if(stamina<=0)
        {
            canclimb = false;
        }
            

        Vector3 speed=new (0,0,0);
        Vector3 newSped;
        if ((Input.GetKey(KeyCode.LeftShift)) & (canclimb || isClimbing) && !isWallJumping)//TIRMANMAYA GÝRÝÞ
        {
            PlayerObject.GetComponent<Animator>().SetBool("Tirmanista", true);
            nonClimbingTime = 0;
            StaminaChange(-Time.deltaTime * staminaConstant);
            StaminaMaviBar.transform.parent.gameObject.SetActive(true);
      

           GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().drag = Climbdrag;//çýkýþta düzeltmeyi unutma
            isJumping = false;//zýplarsa stamina düþecek
            isClimbing = true;
            newSped = new(0, 0, 0);
            bool duruyomu = true;
            if (Input.GetKey(KeyCode.A)&& DuvarYanalEksenTirmanilabilir)
            {
                duruyomu = false;
                   speed = MyRigidbdoy.velocity;

             
                if (Mathf.Abs(TirmanmaAcisi - 0) < 0.1f)
                    newSped = new(-veloMag, speed.y, speed.z);
                else if (Mathf.Abs(TirmanmaAcisi - 90) < 0.1f)
                    newSped = new(speed.x, speed.y, veloMag);


                MyRigidbdoy.velocity = newSped;
            }
            if (Input.GetKey(KeyCode.D)&& DuvarYanalEksenTirmanilabilir)
            {
                duruyomu = false;
                speed = MyRigidbdoy.velocity;

            
                if (Mathf.Abs(TirmanmaAcisi - 0) < 0.1f)
                    newSped = new(veloMag, speed.y, speed.z);
                else if (Mathf.Abs(TirmanmaAcisi - 90) < 0.1f)
                    newSped = new(speed.x, speed.y, -veloMag);


                MyRigidbdoy.velocity = newSped;
            }
            if (Input.GetKey(KeyCode.W))
            {
                duruyomu = false;
                speed = MyRigidbdoy.velocity;

                newSped = new(speed.x, veloMag, speed.z);
                MyRigidbdoy.velocity = newSped;
            }
            if (Input.GetKey(KeyCode.S))
            {
                duruyomu = false;
                speed = MyRigidbdoy.velocity;

                newSped = new(speed.x, -veloMag, speed.z);
                MyRigidbdoy.velocity = newSped;
            }
            if (duruyomu)
                PlayerObject.GetComponent<Animator>().speed = 0;
            else
                PlayerObject.GetComponent<Animator>().speed =1;

            if (Input.GetKey(KeyCode.Space))//zýplýyor ve aslýnda çýktý týrmanmadan
            {
                PlayerObject.gameObject.GetComponent<Animator>().SetBool("Havadami", true);
                StaminaChange(-ZiplamaStaminaCost);
                isWallJumping = true;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().drag = originalDrag;//çýkýþta düzeltmeyi unutma
                isJumping = false;//zýplarsa stamina düþecek
                isClimbing = false;
                speed = MyRigidbdoy.velocity;
                newSped = new(speed.x, JumpMag, speed.z);
                MyRigidbdoy.velocity = newSped;

                canjump = false;  
            }
            StaminaMaviBar.GetComponent<Image>().fillAmount = stamina / 100f;
        }
        else if(isWallJumping)
        {
            PlayerObject.GetComponent<Animator>().SetBool("Tirmanista", false);
            if (Input.GetKeyUp(KeyCode.LeftShift))//zýplarken shifti býraktýðýnda tekrar shite basarsa týrmanmaya devam edecek
            {
                isWallJumping = false;
                canDash = true;
            }
        }
        else
        {
            PlayerObject.GetComponent<Animator>().SetBool("Tirmanista", false);
        }


        if(isClimbing&& (Input.GetKeyUp(KeyCode.LeftShift)||!canclimb))//týrmanmadan çýkýþ
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().drag = originalDrag;//çýkýþta düzeltmeyi unutma

            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity / 4;

            
            isJumping = false;//zýplarsa stamina düþecek
            isClimbing = false;
            canjump = false;
            canDash = true;
        }
        

        if(!isClimbing)
        {
            //if(canDash&& stamina > DashStaminaCost && (Input.GetKey(KeyCode.Q)))//DASH ATMA
            //{
            //    StaminaChange(-DashStaminaCost);
            //    Vector3 speedVec = GetComponent<Rigidbody>().velocity;
            //    speedVec.y = 0;
            //    Vector3 DashVec = speedVec.normalized*DashConstant;
            //    GetComponent<Rigidbody>().velocity=DashVec;
            //    canDash = false;
            //    if(!isJumping)
            //    {
            //        StartCoroutine(DashSayac());
            //    }
            //}

            if (stamina<100)
            {
                if(nonClimbingTime>=4)
                {
                    StaminaChange(Time.deltaTime * staminaDolmaConstant);            
                }
                else
                    nonClimbingTime += Time.deltaTime;
            }
          
        
            if ((Input.GetKey(KeyCode.Space)) & canjump)
            {
                //MyRigidbdoy.AddForce(new Vector3(0,500,0));
                PlayerObject.gameObject.GetComponent<Animator>().SetBool("Havadami", true);
                speed = MyRigidbdoy.velocity;
                Vector3 temp = Vector3.Normalize(new(speed.x, 0, speed.z))* JumpYatayVeloConstant;
                newSped = new(temp.x, JumpMag, temp.z);
                MyRigidbdoy.velocity = newSped;

                canjump = false;
                isJumping = true;
            }

            if(!isJumping &&(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S)))
            {
                Vector3 OriginalSpeed = MyRigidbdoy.velocity;
                Vector3 DeltaSpeed = new (0, 0, 0);
                if (Input.GetKey(KeyCode.A))
                    if(OriginalSpeed.x>-veloMag)
                    {
                        DeltaSpeed.x -= VeloGainConstant;
                        if (OriginalSpeed.x < -veloMag)
                            OriginalSpeed.x = -veloMag;
                    }
                if (Input.GetKey(KeyCode.D))
                    if (OriginalSpeed.x<veloMag)
                    {
                        DeltaSpeed.x += VeloGainConstant;
                        if (OriginalSpeed.x >veloMag)
                            OriginalSpeed.x = veloMag;
                    }
                if (Input.GetKey(KeyCode.W))
                    if (OriginalSpeed.z < veloMag)
                    {
                        DeltaSpeed.z += VeloGainConstant;
                        if (OriginalSpeed.z > veloMag)
                            OriginalSpeed.z = veloMag;
                    }                     
                if (Input.GetKey(KeyCode.S))             
                    if (OriginalSpeed.z > -veloMag)
                    {
                        DeltaSpeed.z -= VeloGainConstant;
                        if (OriginalSpeed.z < -veloMag)
                            OriginalSpeed.z = -veloMag;
                    }

                if (DeltaSpeed.magnitude > VeloGainConstant)
                    DeltaSpeed *= (VeloGainConstant / DeltaSpeed.magnitude);

                OriginalSpeed += DeltaSpeed;

                Vector3 temp = new (OriginalSpeed.x, 0, OriginalSpeed.z);
                if (temp.magnitude>veloMag)
                    temp *= (veloMag / temp.magnitude);     

                
                MyRigidbdoy.velocity = new (temp.x, OriginalSpeed.y,temp.z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("ZeminYan")&&isJumping&&(Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.A)))zxczxc
        //{
        //    canjump = true;
        //    isJumping = false;
        //    isClimbing = true;
        //}
        if (collision.gameObject.CompareTag("Zemin"))
        {
            PlayerObject.gameObject.GetComponent<Animator>().SetBool("Havadami", false);
            PlayerObject.GetComponent<Animator>().SetTrigger("Landing");
            StartCoroutine(ZiplamadanLandingBekleme());
            
          
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            TirmanmaAcisi = collision.transform.rotation.eulerAngles.y;
            DuvarYanalEksenTirmanilabilir = collision.gameObject.GetComponent<DuvarOzellik>().YanalEksenVarmi;
            canclimb = true;
        }

    }

    IEnumerator ZiplamadanLandingBekleme()
    {
        yield return new WaitForSeconds(0.65f);
        canDash = true;
        canjump = true;
        isJumping = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zemin"))
        {
            canjump = false ;
            //isJumping = true;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            canclimb = false;
            //isClimbing = false;
        }
    }


    public void StaminaChange(float deltaStamina)
    {
        if(deltaStamina<0)
            nonClimbingTime=0;

        stamina += deltaStamina;
        if (stamina < 0)
            stamina = 0;
        if (stamina >= 100)
        {
            stamina = 100;
            StaminaMaviBar.transform.parent.gameObject.SetActive(false);
        }

        if(stamina>=0 && stamina<100)
        {
            StaminaMaviBar.transform.parent.gameObject.SetActive(true);
        }

        StaminaMaviBar.GetComponent<Image>().fillAmount = stamina / 100f;


    }


    IEnumerator DashSayac()
    {
        float temptime = 0;
        while(!canDash)
        {
            temptime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            if (temptime >= 2)
                canDash = true;
        }
    }
}
