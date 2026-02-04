using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
 

public class movimientoPersonaje : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    //variable collider espada
    [SerializeField] private Collider coliderAtaque;



    [Header("MOVIMIENTO")]
    [SerializeField] private float velocidad = 5.0f;
    [SerializeField] private float velocidadGiro = 360.0f;
    [SerializeField] private float fuerzaSalto = 5.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        //activar movimiento salto
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
        //activar ataque
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }





    }


    private void FixedUpdate()
    {

        //Controles de teclado
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        //mocimiento en coordenadas locales
        //      (SUMA DE VECTORES).NORMALIZED

        Vector3 direccion = (transform.right * h  + transform.forward * v).normalized;
        Vector3 movimiento = direccion * velocidad * Time.fixedDeltaTime;

        // aplicar método MovePosition (donde que vector que posicion)
        rb.MovePosition(rb.position + movimiento);

       if (v!= 0.0f)
        {
            anim.SetBool("isMoving", true);

        }
       else
        {
            anim.SetBool("isMoving", false);

        }


        if (h != 0)
        {

            if (h < 0)
            {
                anim.SetBool("isMovingLeft", true);
                anim.SetBool("isMovingRight", false);
            }

            else
            {
                anim.SetBool("isMovingRight", true);
                anim.SetBool("isMovingLeft", false);
            }
        }

        else
        {
            anim.SetBool("isMovingLeft", false);
            anim.SetBool("isMovingRight", false);
        }


            //control de ratón
            float giro = Input.GetAxis("Mouse X");

        //giro en coords locales
        float giroY = giro * velocidadGiro * Time.fixedDeltaTime;
        Quaternion rotacion = Quaternion.Euler(0.0f, giroY, 0.0f);
        rb.MoveRotation(rb.rotation * rotacion);
    }

    //habilitar collider

    private void Habilitar()

    {
        coliderAtaque.enabled = true;

    }



    //deshabilitar collider
    private void Deshabilitar()
    {
        coliderAtaque.enabled = false;

    }


}
