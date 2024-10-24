using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputScript : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false; // Disabilita la gravità all'inizio
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos) + offset;
            transform.position = worldPos;

            // Controlla la rotella del mouse per traslare avanti e indietro
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                offset += Camera.main.transform.forward * scroll * 0.001f; // Modifica l'offset in base alla rotella del mouse
            }
        }
    }

    private void OnMouseDown()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        offset = transform.position - worldPos;
        isDragging = true;
        rb.useGravity = false; // Disabilita la gravità mentre si trascina
        rb.velocity = Vector3.zero; // Ferma il movimento dell'oggetto
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos) + offset;
            transform.position = worldPos;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.useGravity = true; // Abilita la gravità quando il mouse viene rilasciato
    }
}
